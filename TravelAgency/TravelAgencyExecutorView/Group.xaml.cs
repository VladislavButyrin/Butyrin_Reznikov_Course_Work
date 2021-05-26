using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyExecutorView
{
    public partial class Group : Window
    {
        public int Id { set { id = value; } }
        private readonly GroupBusinessLogic group_logic;
        private readonly TourBusinessLogic tour_logic;
        private int? id;
        private string Username;
        private List<string> ToursGroups;
        private List<string> GroupsPlaces;
        public Group(GroupBusinessLogic _group_logic, TourBusinessLogic _tour_logic)
        {
            InitializeComponent();
            group_logic = _group_logic;
            tour_logic = _tour_logic;
        }
        private void LoadData()
        {
            try
            {
                SelectedToursListBox.Items.Clear();
                foreach (var currentTours in ToursGroups)
                {
                    SelectedToursListBox.Items.Add(currentTours);
                }

                CanSelectedToursListBox.Items.Clear();
                foreach (var tour in tour_logic.Read(null))
                {
                    if (ToursGroups.Where(rec => rec == tour.TourName).ToList().Count == 0)
                    {
                        CanSelectedToursListBox.Items.Add(tour);
                    }
                }

                SelectedPlacesListBox.Items.Clear();
                foreach (var service in GroupsPlaces)
                {
                    SelectedPlacesListBox.Items.Add(service);
                }

                UserLabel.Content = Username;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CheckCurrentUser()
        {
            if (Username == App.User.Fullname)
            {
                SaveButton.IsEnabled = true;
                RefundButton.IsEnabled = true;
                AddButton.IsEnabled = true;
            }
            else if(Username != App.User.Fullname && UserLabel.Content == null)
            {
                SaveButton.IsEnabled = true;
                RefundButton.IsEnabled = true;
                AddButton.IsEnabled = true;
            } 
            else
            {
                SaveButton.IsEnabled = false;
                RefundButton.IsEnabled = false;
                AddButton.IsEnabled = false;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedToursListBox.SelectedItems.Count == 1)
            {
                ToursGroups.Add(((TourViewModel)CanSelectedToursListBox.SelectedItem).TourName);
                LoadData();
            }
        }
        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedToursListBox.SelectedItems.Count == 1)
            {
                ToursGroups.Remove(ToursGroups.Where(rec => rec == (string)SelectedToursListBox.SelectedItem).ToList()[0]);
                LoadData();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DateGroupDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Укажите дату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ToursGroups == null || ToursGroups.Count == 0)
            {
                MessageBox.Show("Укажите животных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                group_logic.CreateOrUpdate(new GroupBindingModel
                {
                    Id = id,
                    DateGroup = DateGroupDatePicker.SelectedDate.Value,
                    ToursGroups = ToursGroups,
                    UserId = (int)App.User.UserId
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Group_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try 
                {
                    GroupViewModel view = group_logic.Read(new GroupBindingModel { Id = id.Value })?[0];
                    DateGroupDatePicker.SelectedDate = view.DateGroup;
                    ToursGroups = view.ToursGroups;
                    GroupsPlaces = view.GroupsPlaces;
                    Username = view.Username;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ToursGroups = new List<string>();
                GroupsPlaces = new List<string>();
            }
            LoadData();
            CheckCurrentUser();
        }
    }
}