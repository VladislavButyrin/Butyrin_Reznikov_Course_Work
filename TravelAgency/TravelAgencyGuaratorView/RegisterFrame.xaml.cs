using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using Unity;
using System;
using System.Text.RegularExpressions;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для RegisterFrame.xaml
    /// </summary>
    public partial class RegisterFrame : Window
    {
        const int _passwordMaxLength = 3;
        const int _passwordMinLength = 40;
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OrganizatorLogic logic;
        public RegisterFrame(OrganizatorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                return;
            }
            if (!Regex.IsMatch(LoginTextBox.Text, @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$"))
            {
                MessageBox.Show("Логином должна быть электронная почта", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка");
                return;
            }
            if (PasswordTextBox.Text.Length < _passwordMaxLength || PasswordTextBox.Text.Length >
_passwordMinLength /*|| !Regex.IsMatch(PasswordTextBox.Text,
@"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))*/)
            {
                MessageBox.Show("Пароль должен быть сложнее", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(FIOTextBox.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка");
                return;
            }
            try
            {
                logic.CreateOrUpdate(new OrganizatorBindingModel
                {
                    Id = null,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text,
                    FIO = FIOTextBox.Text
                });
                App.OrganizatorId = (int)logic.Read(new OrganizatorBindingModel { Login = LoginTextBox.Text })[0].Id;
                MessageBox.Show("Регистрация прошла успешно", "Сообщение");
                var form = Container.Resolve<MainFrame>();
                Close();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<RegisterLoginFrame>();
            Close();
            form.ShowDialog();
        }
    }
}
