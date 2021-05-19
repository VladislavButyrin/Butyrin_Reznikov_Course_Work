﻿using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для LoginFrame.xaml
    /// </summary>
    public partial class LoginFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly DoctorBusinessLogic logic;
        public LoginFrame(DoctorBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                return;
            }
            try
            {
                logic.Login(new DoctorBindingModel
                {
                    Id = null,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text,
                });
                var x = logic.Read(new DoctorBindingModel { Login = LoginTextBox.Text });
                App.DoctorId = (int)logic.Read(new DoctorBindingModel { Login = LoginTextBox.Text})[0].Id;
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
