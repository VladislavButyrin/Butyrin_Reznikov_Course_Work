using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic.Guarantor;
using TravelAgencyBusinessLogic.ViewModels;
using Unity;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace TravelAgencyView
{
    public partial class StatisticsGuideFrame : Window
    {
        private readonly StatisticsLogic statistics_logic;
        public StatisticsGuideFrame(StatisticsLogic statistics_logic)
        {
            InitializeComponent();
            this.statistics_logic = statistics_logic;
        }
        private void LoadData()
        {
            try
            {
                var list = statistics_logic.GetStatisticsByGuide(new StatisticsBindingModelGuarantor
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
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
            var list = statistics_logic.GetStatisticsByGuide(new StatisticsBindingModelGuarantor
            {
                DateFrom = DatePickerFrom.SelectedDate,
                DateTo = DatePickerTo.SelectedDate,
            });
            SeriesCollection series = new SeriesCollection();

            ChartValues<int> numberOfVisits = new ChartValues<int>();

            List<string> medicinesName = new List<string>();

            foreach (var item in list)

            {
                numberOfVisits.Add(item.cost);
                medicinesName.Add(item.guideName);
            }
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\nЛекартва",
                Labels = medicinesName
            });
            LineSeries animalLine = new LineSeries
            {
                Title = "Статистика по Гидам: ",
                Values = numberOfVisits
            };

            series.Add(animalLine);
            cartesianChart.Series = series;
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ChartCreation();
        }
    }
}
