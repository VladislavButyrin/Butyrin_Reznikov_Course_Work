using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using System;
using System.Collections.Generic;

namespace TravelAgencyImplementerView
{
    public partial class ListPlaces : Window
    {
        private readonly ReportLogicImplementer report_logic;
        private readonly TourLogic tour_logic;
        private List<string> Places;
        public ListPlaces(ReportLogicImplementer _report_logic, TourLogic _tour_logic)
        {
            InitializeComponent();
            report_logic = _report_logic;
            tour_logic = _tour_logic;
        }
        private void LoadData()
        {
            try
            {
                if (Places != null)
                {
                    CanSelectedToursListBox.Items.Clear();
                    foreach (var place in tour_logic.Read(null))
                    {
                        CanSelectedToursListBox.Items.Add(place.TourName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<string> ToursName = new List<string>();
                    foreach (var tourName in CanSelectedToursListBox.SelectedItems)
                    {
                        ToursName.Add(tourName.ToString());
                    }
                    report_logic.SavePlacesToWordFile(new ReportBindingModelImplementer { FileName = dialog.FileName, ToursName = ToursName });
                    MessageBox.Show("Файл со списком мест успешно создан", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<string> ToursName = new List<string>();
                    foreach (var tourName in CanSelectedToursListBox.SelectedItems)
                    {
                        ToursName.Add(tourName.ToString());
                    }
                    report_logic.SaveToursToExcelFile(new ReportBindingModelImplementer { FileName = dialog.FileName, ToursName = ToursName });
                    MessageBox.Show("Файл со списком мест успешно создан", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void CancelButton__Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ListPlaces_Load(object sender, RoutedEventArgs e)
        {
            Places = new List<string>();
            LoadData();
        }
    }
}