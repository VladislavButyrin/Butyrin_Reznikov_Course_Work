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
    /// Логика взаимодействия для PlaceFrame.xaml
    /// </summary>
    public partial class PlaceFrame : Window
    {
        public int Id { set { id = value; } }
        private readonly PlaceLogic servic_logic;
        private readonly TripLogic trip_logic;
        private readonly OrganizatorLogic organizator_logic;
        private int? id;
        private Dictionary<int, string> placeTrips;
        public PlaceFrame(PlaceLogic servic_logic, TripLogic trip_logic, OrganizatorLogic organizator_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.trip_logic = trip_logic;
            this.organizator_logic = organizator_logic;
        }
        private void LoadData()
        {
            try
            {
                if (placeTrips != null)
                {
                    SelectedTripsListBox.Items.Clear();
                    foreach (var mm in placeTrips)
                    {
                        SelectedTripsListBox.Items.Add(mm.Value);
                    }
                }
                CanSelectedTripsListBox.Items.Clear();
                foreach (var m in trip_logic.Read(null))
                {
                    if (placeTrips.Values.Where(rec => rec == m.TripName).ToList().Count == 0)
                        CanSelectedTripsListBox.Items.Add(m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(AddresTextBox.Text))
            {
                MessageBox.Show("Укажите цену", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (placeTrips == null || placeTrips.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                servic_logic.CreateOrUpdate(new PlaceBindingModel
                {
                    Id = id,
                    PlaceName = NameTextBox.Text,
                    OrganizatorId = App.OrganizatorId,
                    Trips = placeTrips,
                    Adress=AddresTextBox.Text
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTripsListBox.SelectedItems.Count == 1)
            {
                placeTrips.Remove(placeTrips.Where(rec => rec.Value == (string)SelectedTripsListBox.SelectedItem).ToList()[0].Key);
                LoadData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedTripsListBox.SelectedItems.Count == 1)
            {
                placeTrips.Add(((TripViewModel)CanSelectedTripsListBox.SelectedItem).Id
                    , ((TripViewModel)CanSelectedTripsListBox.SelectedItem).TripName);
                LoadData();
            }
        }

        private void PlaceFrame_Load(object sender, RoutedEventArgs e)
        {
            var FIOsource = organizator_logic.Read(null);
            if (id.HasValue)
            {
                try
                {
                    PlaceViewModel view = servic_logic.Read(new PlaceBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    placeTrips = view.Trips;
                    NameTextBox.Text = view.PlaceName;
                    AddresTextBox.Text = view.Adress;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                }
            }
            else
            {
                placeTrips = new Dictionary<int, string>();
            }
            LoadData();
        }
    }
}
