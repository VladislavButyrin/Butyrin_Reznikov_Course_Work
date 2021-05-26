using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для GuidesReportFrame.xaml
    /// </summary>
    public partial class GuidesReportFrame : Window
    {
        private readonly ReportLogicGuarantor report_logic;
        private readonly TripLogic trip_logic;
        private Dictionary<int, string> Trips;
        public GuidesReportFrame(ReportLogicGuarantor report_logic, TripLogic trip_logic)
        {
            InitializeComponent();
            this.report_logic = report_logic;
            this.trip_logic = trip_logic;
        }
        private void LoadData()
        {
            try
            {
                if (Trips != null)
                {
                    CanSelectedTripsListBox.Items.Clear();
                    foreach (var m in trip_logic.Read(null))
                    {
                        CanSelectedTripsListBox.Items.Add(m);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<int> selected_trips = new List<int>();
                    foreach (var item in CanSelectedTripsListBox.SelectedItems)
                    {
                        selected_trips.Add(((TripViewModel)item).Id);
                    }
                    report_logic.SaveExcursionsToWordFile(new ReportBindingModelGuarantor { FileName = dialog.FileName, Trips = selected_trips });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
               MessageBoxImage.Information);
                }
            }
        }

        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<int> selected_trips = new List<int>();
                    foreach (var item in CanSelectedTripsListBox.SelectedItems)
                    {
                        selected_trips.Add(((TripViewModel)item).Id);
                    }
                    report_logic.SaveExcursionsToExcelFile(new ReportBindingModelGuarantor { FileName = dialog.FileName, Trips = selected_trips });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
               MessageBoxImage.Information);
                }
            }
        }

        private void CancelButton__Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void GuidesReportFrame_Load(object sender, RoutedEventArgs e)
        {
            Trips = new Dictionary<int, string>();
            LoadData();
        }
    }
}
