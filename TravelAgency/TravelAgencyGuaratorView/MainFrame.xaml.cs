using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public MainFrame()
        {
            InitializeComponent();
        }

        private void TripItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<TripsFrame>();
            form.ShowDialog();
        }

        private void GuideItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<GuidesFrame>();
            form.ShowDialog();
        }

        private void PlaceItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<PlacesFrame>();
            form.ShowDialog();
        }

        private void GuideListItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<GuidesReportFrame>();
            form.ShowDialog();
        }

        private void GuidePlaceReportItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderReportFrame>();
            form.ShowDialog();
        }

        private void AddPlaceToGroupItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<AddPlaceToGroupFrame>();
            form.ShowDialog();
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<StatisticsFrame>();
            form.ShowDialog();
        }

        private void StatGuide_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<StatisticsGuideFrame>();
            form.ShowDialog();
        }
    }
}
