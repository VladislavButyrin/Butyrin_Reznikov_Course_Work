using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic.Guarantor;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Unity;
using LiveCharts;
using LiveCharts.Wpf;
using TravelAgencyBusinessLogic.BusinessLogic;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для StatisticsFrame.xaml
    /// </summary>
    public partial class StatisticsFrame : Window
    {
        private readonly PlaceLogic servic_logic;
        private readonly TravelAgencyBusinessLogic.BusinessLogic.Guarantor.StatisticsLogic statistics_logic;
        public StatisticsFrame(PlaceLogic servic_logic, TravelAgencyBusinessLogic.BusinessLogic.Guarantor.StatisticsLogic statistics_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.statistics_logic = statistics_logic;
            PlaceComboBox.ItemsSource = servic_logic.Read(new PlaceBindingModel { OrganizatorId = App.OrganizatorId });
        }
        private void LoadData()
        {
            try
            {
                var list = statistics_logic.GetStatisticsByPlaces(new StatisticsBindingModelGuarantor
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
                    ElementId = ((PlaceViewModel)PlaceComboBox.SelectedItem).Id
                });
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.ColumnWidth = DataGridLength.Auto;
                    var columns = dataGridView.Columns;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void ChartCreation()

        {
            var list = statistics_logic.GetStatisticsByPlaces(new StatisticsBindingModelGuarantor
            {
                DateFrom = DatePickerFrom.SelectedDate,
                DateTo = DatePickerTo.SelectedDate,
                ElementId = ((PlaceViewModel)PlaceComboBox.SelectedItem).Id
            });
            SeriesCollection series = new SeriesCollection();

            ChartValues<int> numberOfVisits = new ChartValues<int>();

            List<string> toursname = new List<string>();

            foreach (var item in list)

            {
                numberOfVisits.Add(item.count);
                toursname.Add(item.date.ToShortDateString());
            }
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\n" + ((PlaceViewModel)PlaceComboBox.SelectedItem).PlaceName,
                Labels = toursname
            });
            LineSeries animalLine = new LineSeries
            {
                Title = "Статистика по месту: ",
                Values = numberOfVisits
            };

            series.Add(animalLine);
            cartesianChart.Series = series;
        }

        private void PlaceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception)
            {

            }
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            ChartCreation();
        }
    }
}