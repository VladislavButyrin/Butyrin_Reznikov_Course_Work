using TravelAgencyExecutorBusinessLogic.BindingModels;
using TravelAgencyExecutorBusinessLogic.BusinessLogic;
using TravelAgencyExecutorBusinessLogic.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyExecutorView
{
    public partial class Excursion : Window
    {
        public int Id { set { id = value; } }
        private readonly ExcursionLogic purchase_logic;
        private readonly TourLogic tour_logic;
        private int? id;
        private string Username;
        private decimal Sum;
        private List<string> ToursExcursions;
        private Dictionary<string, (int, int)> GuidesExcursions;
        public Excursion(ExcursionLogic _purchase_logic, TourLogic _tour_logic)
        {
            InitializeComponent();
            purchase_logic = _purchase_logic;
            tour_logic = _tour_logic;
        }
        private void LoadData()
        {
            try
            {
                SelectedToursListBox.Items.Clear();
                foreach (var tour in ToursExcursions)
                {
                    SelectedToursListBox.Items.Add(tour);
                }

                CanSelectedToursListBox.Items.Clear();
                foreach (var tour in tour_logic.Read(null))
                {
                    if (ToursExcursions.Where(rec => rec.ToString() == tour.TourName).ToList().Count == 0)
                    {
                        CanSelectedToursListBox.Items.Add(tour);
                    }
                }

                SelectedGuidesListBox.Items.Clear();
                foreach (var guide in GuidesExcursions)
                {
                    SelectedGuidesListBox.Items.Add(guide.Key + "   —   " + guide.Value.Item1);
                }

                UserLabel.Content = Username;
                SumLabel.Content = Sum;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedToursListBox.SelectedItems.Count == 1)
            {
                ToursExcursions.Remove(ToursExcursions.Where(rec => rec.ToString() == (string)SelectedToursListBox.SelectedItem).ToList()[0]);
                LoadData();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedToursListBox.SelectedItems.Count == 1)
            {
                ToursExcursions.Add(((TourViewModel)CanSelectedToursListBox.SelectedItem).TourName);
                LoadData();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursExcursions == null || ToursExcursions.Count == 0)
            {
                MessageBox.Show("Укажите животное", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                purchase_logic.CreateOrUpdate(new ExcursionBindingModel
                {
                    Id = id,
                    Sum = (decimal)SumLabel.Content,
                    DatePayment = DateTime.Now,
                    GuidesExcursions = GuidesExcursions,
                    ToursExcursions = ToursExcursions,
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
        private void CheckCurrentUser()
        {
            if (Username == App.User.Fullname)
            {
                SaveButton.IsEnabled = true;
                RefundButton.IsEnabled = true;
                AddButton.IsEnabled = true;
            }
            else if (Username != App.User.Fullname && UserLabel.Content == null)
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Excursion_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ExcursionViewModel view = purchase_logic.Read(new ExcursionBindingModel { Id = id.Value })?[0];
                    ToursExcursions = view.ToursExcursions;
                    GuidesExcursions = view.GuidesExcursions;
                    Username = view.Username;
                    Sum = view.Sum;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ToursExcursions = new List<string>();
                GuidesExcursions = new Dictionary<string, (int, int)>();
            }
            LoadData();
            CheckCurrentUser();
        }
    }
}