﻿using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.HelperModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using MailKit.Net.Pop3;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class MailLogic
    {
        private static string smtpClientHost;
        private static int smtpClientPort;
        private static string mailLogin;
        private static string mailPassword;
        private readonly IMessageInfoStorage _messageInfoStorage;
        private readonly IDoctorStorage _doctorStorage;
        public MailLogic(IMessageInfoStorage messageInfoStorage, IDoctorStorage
       doctorStorage)
        {
            _messageInfoStorage = messageInfoStorage;
            _doctorStorage = doctorStorage;
        }
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return _messageInfoStorage.GetFullList();
            }
            return _messageInfoStorage.GetFilteredList(model);
        }
        public void CreateOrder(MessageInfoBindingModel model)
        {
            var client = _doctorStorage.GetElement(new DoctorBindingModel
            {
                Login = model.FromMailAddress
            });
            model.DoctorId = client?.Id;
            _messageInfoStorage.Insert(model);
        }
        public static void MailConfig(MailConfig config)
        {
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
        }
        public static async void MailSendAsync(MailSendInfo info)
        {
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) ||
           string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            using (var objMailMessage = new MailMessage())
            {
                using (var objSmtpClient = new SmtpClient(smtpClientHost,
               smtpClientPort))
                {
                    try
                    {
                        objMailMessage.From = new MailAddress(mailLogin);
                        objMailMessage.To.Add(new MailAddress(info.MailAddress));
                        objMailMessage.Subject = info.Subject;
                        objMailMessage.Body = info.Text;
                        objMailMessage.SubjectEncoding = Encoding.UTF8;
                        objMailMessage.BodyEncoding = Encoding.UTF8;
                        objSmtpClient.UseDefaultCredentials = false;
                        objSmtpClient.EnableSsl = true;
                        objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        objSmtpClient.Credentials = new NetworkCredential(mailLogin,
                        mailPassword);
                        await Task.Run(() => objSmtpClient.Send(objMailMessage));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        public static async void MailCheck(MailCheckInfo info)
        {
            if (string.IsNullOrEmpty(info.PopHost) || info.PopPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (info.MessageStorage == null)
            {
                return;
            }
            using (var client = new Pop3Client())
            {
                await Task.Run(() =>
                {
                    try
                    {
                        client.Connect(info.PopHost, info.PopPort,
                       SecureSocketOptions.SslOnConnect);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.AuthenticationMechanisms.Remove("NTLM");
                        client.Authenticate(mailLogin, mailPassword);
                        for (int i = 0; i < client.Count; i++)
                        {
                            var message = client.GetMessage(i);
                            foreach (var mail in message.From.Mailboxes)
                            {
                                int? x = info.DoctorStorage.GetElement(new DoctorBindingModel { Login = mail.Address })?.Id;
                                info.MessageStorage.Insert(new MessageInfoBindingModel
                                {
                                    DoctorId = info.DoctorStorage.GetElement(new DoctorBindingModel { Login = mail.Address })?.Id,
                                    DateDelivery = message.Date.DateTime,
                                    MessageId = message.MessageId,
                                    FromMailAddress = mail.Address,
                                    Subject = message.Subject,
                                    Body = message.TextBody
                                });
                            }
                        }
                    }
                    finally
                    {
                        client.Disconnect(true);
                    }
                });
            }
        }
    }
}
