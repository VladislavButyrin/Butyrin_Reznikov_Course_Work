using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для AddServiceToVisitFrame.xaml
    /// </summary>
    public partial class AddServiceToVisitFrame : Window
    {
        private readonly ServiceBusinessLogic servic_logic;
        private readonly VisitBusinessLogic visit_logic;
        public AddServiceToVisitFrame(ServiceBusinessLogic servic_logic, VisitBusinessLogic visit_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.visit_logic = visit_logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                ServiceComboBox.ItemsSource = servic_logic.Read(null);
                VisitComboBox.ItemsSource = visit_logic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (VisitComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите посещение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                visit_logic.AddService(new AddServiceToVisitBindingModel
                {
                    DoctorVisitId = (int)((VisitViewModel)VisitComboBox.SelectedItem).Id,
                    Service = new ServiceBindingModel { 
                        Id = (int)((ServiceViewModel)ServiceComboBox.SelectedItem).Id,
                        ServiceName = ((ServiceViewModel)ServiceComboBox.SelectedItem).ServiceName
                    }
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK,
               MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
