using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для RegisterLoginFrame.xaml
    /// </summary>
    public partial class RegisterLoginFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public RegisterLoginFrame()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<RegisterFrame>();
            Close();
            form.ShowDialog();
        }

        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<LoginFrame>();
            Close();
            form.ShowDialog();
        }
    }
}
