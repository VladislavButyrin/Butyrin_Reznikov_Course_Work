using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public MainFrame()
        {
            InitializeComponent();
        }

        private void MedicationItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<MedicationsFrame>();
            form.ShowDialog();
        }

        private void MedicineItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<MedicinesFrame>();
            form.ShowDialog();
        }

        private void ServiceItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ServicesFrame>();
            form.ShowDialog();
        }

        private void MedicineListItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<MedicinesReportFrame>();
            form.ShowDialog();
        }

        private void MedicineServiceReportItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderReportFrame>();
            form.ShowDialog();
        }

        private void AddServiceToVisitItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<AddServiceToVisitFrame>();
            form.ShowDialog();
        }
    }
}
