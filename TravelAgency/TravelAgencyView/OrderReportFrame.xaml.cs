﻿using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для OrderReportFrame.xaml
    /// </summary>
    public partial class OrderReportFrame : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        private readonly DoctorBusinessLogic _doctorLogic;
        public OrderReportFrame(ReportLogic logic,DoctorBusinessLogic doctorLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this._doctorLogic = doctorLogic;
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = logic.GetServiceMedicine(new ReportBindingModel { 
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                if (order != null)
                {
                    DataGridView.ItemsSource = null;
                    DataGridView.ItemsSource = order;
                }
                System.Windows.MessageBox.Show("Получилось", "Информация", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                System.Windows.MessageBox.Show("Неверное выставление даты начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                string basis = "Отчет по болезням";
                msg.Subject = basis;
                msg.Body = basis + " c " + DatePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + DatePickerTo.SelectedDate.Value.ToShortDateString();

                msg.From = new MailAddress("emailforlab1@gmail.com");
                msg.To.Add(_doctorLogic.Read(new DoctorBindingModel { Id = App.DoctorId})[0].Login);
                msg.IsBodyHtml = true;

                string file = "X:\\Otchet.pdf";
                logic.SaveOrderToPdfFile(new ReportBindingModel
                {
                    FileName = file,
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attach.ContentDisposition;

                //meta inf for mail
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                //conn
                msg.Attachments.Add(attach);
                client.Host = "smtp.gmail.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("emailforlab1@gmail.com", "Jujhjl34");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);

                //success
                System.Windows.MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
