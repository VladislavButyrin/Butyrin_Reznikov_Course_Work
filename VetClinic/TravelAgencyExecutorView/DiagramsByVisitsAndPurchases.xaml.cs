using TravelAgencyBusinessLogic.BusinessLogic;
using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using System.Windows.Media;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyExecutorView
{
    public partial class DiagramsByGroupsAndExcursions : Window
    {
        private readonly StatisticsLogic reportLogic;
        public DiagramsByGroupsAndExcursions(StatisticsLogic _reportLogic)
        {
            InitializeComponent();
            reportLogic = _reportLogic;
        }
        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            cartesianChart.Visibility = Visibility.Hidden;
        }
        private void ChartCreation(List<StatisticsByGroupsAndExcursionsViewModel> report)
        {
            SeriesCollection series = new SeriesCollection();
            ChartValues<int> numberOfGroups = new ChartValues<int>();
            List<string> dates = new List<string>();

            foreach (var item in report)
            {
                numberOfGroups.Add(item.AmountPerDay);
                dates.Add(item.Data);
            }

            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#000000");

            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\nДаты",
                FontSize = 20,
                Foreground = brush,
                Labels = dates
            });

            LineSeries eventLine = new LineSeries
            {
                Title = "Кол-во событий: ",
                FontSize = 20,
                Values = numberOfGroups
            };

            series.Add(eventLine);
            cartesianChart.Series = series;
            cartesianChart.LegendLocation = LegendLocation.Top;
            cartesianChart.Visibility = Visibility.Visible;
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
            if (SelectedDiagramComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите диаграмму", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (SelectedDiagramComboBox.Text == "Покупки")
                {
                    var report = reportLogic.GetExcursions(new StatisticsBindingModel
                    {
                        DateFrom = dataFromDataPicker.SelectedDate,
                        DateTo = dataToDataPicker.SelectedDate
                    });
                    if (report != null)
                    {
                        dataGridView.Items.Clear();
                        dataGridView.ItemsSource = report;
                    }
                    DataEventDataGridTextColumn.Header = "Дата покупки";
                    AmountPerDayDataGridTextColumn.Header = "Кол-во покупок за день";
                    ChartCreation(report);
                }
                else
                {
                    var report = reportLogic.GetGroups(new StatisticsBindingModel
                    {
                        DateFrom = dataFromDataPicker.SelectedDate,
                        DateTo = dataToDataPicker.SelectedDate
                    });
                    if (report != null)
                    {
                        dataGridView.Items.Clear();
                        dataGridView.ItemsSource = report;
                    }
                    DataEventDataGridTextColumn.Header = "Дата посещения";
                    AmountPerDayDataGridTextColumn.Header = "Кол-во посещений за день";
                    ChartCreation(report);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}