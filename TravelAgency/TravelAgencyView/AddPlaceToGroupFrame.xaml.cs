using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для AddPlaceToGroupFrame.xaml
    /// </summary>
    public partial class AddPlaceToGroupFrame : Window
    {
        private readonly PlaceBusinessLogic servic_logic;
        private readonly GroupBusinessLogic group_logic;
        public AddPlaceToGroupFrame(PlaceBusinessLogic servic_logic, GroupBusinessLogic group_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.group_logic = group_logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                PlaceComboBox.ItemsSource = servic_logic.Read(null);
                GroupComboBox.ItemsSource = group_logic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (PlaceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите посещение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                group_logic.AddPlace(new AddPlaceToGroupBindingModel
                {
                    OrganizatorGroupId = (int)((GroupViewModel)GroupComboBox.SelectedItem).Id,
                    Place = new PlaceBindingModel { 
                        Id = (int)((PlaceViewModel)PlaceComboBox.SelectedItem).Id,
                        PlaceName = ((PlaceViewModel)PlaceComboBox.SelectedItem).PlaceName
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
