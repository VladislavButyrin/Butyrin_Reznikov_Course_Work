using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
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

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для OrderReportFrame.xaml
    /// </summary>
    public partial class OrderReportFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        private readonly OrganizatorBusinessLogic _organizatorLogic;
        public OrderReportFrame(ReportLogic logic,OrganizatorBusinessLogic organizatorLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this._organizatorLogic = organizatorLogic;
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = logic.GetPlaceGuide(new ReportBindingModel { 
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
                msg.To.Add(_organizatorLogic.Read(new OrganizatorBindingModel { Id = App.OrganizatorId})[0].Login);
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
