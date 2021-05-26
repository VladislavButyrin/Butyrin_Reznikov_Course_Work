using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyImplementerView
{
    public partial class Excursion : Window
    {
        public int Id { get { return (int)id; }  set { id = value; } }
        private readonly ExcursionLogic excursion_logic;
        private readonly TourLogic tour_logic;
        private int? id;
        private string Username;
        private int UserId;
        private decimal Sum;
        private List<string> ToursExcursions;
        private bool reportWindow = false;
        private Dictionary<string, (int, int)> GuidesExcursions;
        public Excursion(ExcursionLogic _excursion_logic, TourLogic _tour_logic)
        {
            InitializeComponent();
            excursion_logic = _excursion_logic;
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
        public void ReportWindow()
        {
            SaveButton.IsEnabled = false;
            RefundButton.IsEnabled = false;
            AddButton.IsEnabled = false;
            reportWindow = true;
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
                excursion_logic.CreateOrUpdate(new ExcursionBindingModel
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
            if (Username == App.User.Fullname && UserId == App.User.UserId || UserLabel.Content == null)
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
                    ExcursionViewModel view = excursion_logic.Read(new ExcursionBindingModel { Id = id.Value })?[0];
                    ToursExcursions = view.ToursExcursions;
                    GuidesExcursions = view.GuidesExcursions;
                    Sum = view.Sum;
                    UserId = view.UserId;
                    Username = view.Username;
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
            if (!reportWindow)
            {
                CheckCurrentUser();
            }
        }
    }
}