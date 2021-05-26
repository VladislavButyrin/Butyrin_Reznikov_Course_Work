using TravelAgencyBusinessLogic.BusinessLogic;
using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows.Media;

namespace TravelAgencyExecutorView
{
    public partial class DiagramByTours : Window
    {
        private readonly ReportLogic reportLogic;
        public DiagramByTours(ReportLogic _reportLogic)
        {
            InitializeComponent();
            reportLogic = _reportLogic;
        }
        private void ChartCreation(List<StatisticsByToursViewModel> report)
        {
            SeriesCollection series = new SeriesCollection();
            ChartValues<int> numberOfGroups = new ChartValues<int>();
            List<string> toursName = new List<string>();

            foreach (var item in report)
            {
                numberOfGroups.Add(item.NumberOfGroups);
                toursName.Add(item.TourName);
            }

            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#000000");

            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\nЖивотные",
                FontSize = 20,
                Foreground = brush,
                Labels = toursName
            });

            LineSeries tourLine = new LineSeries
            {
                Title = "Кол-во посещений: ",
                FontSize = 20,
                Values = numberOfGroups
            };

            series.Add(tourLine);
            cartesianChart.Series = series;
            cartesianChart.LegendLocation = LegendLocation.Top;
            cartesianChart.Visibility = Visibility.Visible;
        }
        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            cartesianChart.Visibility = Visibility.Hidden;
        }
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            dataGridView.ItemsSource = null;
            if (dataFromDataPicker.SelectedDate == null)
            {
                MessageBox.Show("Укажите дату начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dataToDataPicker.SelectedDate == null)
            {
                MessageBox.Show("Укажите дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dataFromDataPicker.SelectedDate >= dataToDataPicker.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var report = reportLogic.GetToursWithNumberOfGroups(new StatisticsBindingModel
                {
                    DateFrom = dataFromDataPicker.SelectedDate,
                    DateTo = dataToDataPicker.SelectedDate
                });
                if (report != null)
                {
                    dataGridView.Items.Clear();
                    dataGridView.ItemsSource = report;
                }

                ChartCreation(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}