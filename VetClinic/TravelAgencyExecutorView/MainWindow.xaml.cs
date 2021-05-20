using System.Windows;
using Unity;

namespace TravelAgencyExecutorView
{
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ToursItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Tours>();
            form.ShowDialog();
        }
        private void ExcursionsItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Excursions>();
            form.ShowDialog();
        }
        private void GroupsItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Groups>();
            form.ShowDialog();
        }
        private void GuidesBindingItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<GuideBinding>();
            form.ShowDialog();
        }
        private void GetListPlacesItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ListPlaces>();
            form.ShowDialog();
        }
        private void ReportItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Report>();
            form.ShowDialog();
        }
        private void PersonalData_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<PersonalData>();
            form.ShowDialog();
        }
        private void StatisticsItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Statistics>();
            form.ShowDialog();
        }
    }
}