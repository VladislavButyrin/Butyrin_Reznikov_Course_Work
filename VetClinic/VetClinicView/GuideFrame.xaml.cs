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
    /// Логика взаимодействия для GuideFrame.xaml
    /// </summary>
    public partial class GuideFrame : Window
    {
        public int Id { set { id = value; } }
        private readonly GuideBusinessLogic guide_logic;
        private readonly TripBusinessLogic trip_logic;
        private int? id;
        private Dictionary<int, string> guideTrips;
        public GuideFrame(GuideBusinessLogic guide_logic, TripBusinessLogic trip_logic)
        {
            InitializeComponent();
            this.guide_logic = guide_logic;
            this.trip_logic = trip_logic;
        }
        private void LoadData()
        {
            try
            {
                if (guideTrips != null)
                {
                    SelectedTripsListBox.Items.Clear();
                    foreach (var mm in guideTrips)
                    {
                        SelectedTripsListBox.Items.Add(mm.Value);
                    }
                }
                CanSelectedTripsListBox.Items.Clear();
                foreach (var m in trip_logic.Read(null))
                {
                    if (guideTrips.Values.Where(rec => rec == m.TripName).ToList().Count == 0)
                        CanSelectedTripsListBox.Items.Add(m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void GuideFrame_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    GuideViewModel view = guide_logic.Read(new GuideBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    guideTrips = view.Trips;
                    NameTextBox.Text = view.GuideName;
                    CostTextBox.Text = view.Cost.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                }
            }
            else
            {
                guideTrips = new Dictionary<int, string>();
            }
            LoadData();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(CostTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (guideTrips == null || guideTrips.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                guide_logic.CreateOrUpdate(new GuideBindingModel
                {
                    Id = id,
                    GuideName = NameTextBox.Text,
                    Cost = int.Parse(CostTextBox.Text),
                    Trips = guideTrips
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

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTripsListBox.SelectedItems.Count == 1)
            {
                guideTrips.Remove(guideTrips.Where(rec => rec.Value == (string)SelectedTripsListBox.SelectedItem).ToList()[0].Key);
                LoadData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedTripsListBox.SelectedItems.Count == 1)
            {
                guideTrips.Add(((TripViewModel)CanSelectedTripsListBox.SelectedItem).Id
                    , ((TripViewModel)CanSelectedTripsListBox.SelectedItem).TripName);
                LoadData();
            }
        }
    }
}
