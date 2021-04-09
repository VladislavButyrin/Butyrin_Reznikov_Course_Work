using System.Windows;

namespace TravelAgencyView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
        }

        private void Tour_Click(object sender, RoutedEventArgs e)
        {
            WindowTours window = new WindowTours();
            window.ShowDialog();
        }

        private void Excursion_Click(object sender, RoutedEventArgs e)
        {
            WindowExcursions window = new WindowExcursions();
            window.ShowDialog();
        }

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            WindowGroups window = new WindowGroups();
            window.ShowDialog();
        }

        private void Trip_Click(object sender, RoutedEventArgs e)
        {
            WindowTrips window = new WindowTrips();
            window.ShowDialog();
        }

        private void Guide_Click(object sender, RoutedEventArgs e)
        {
            WindowGuides window = new WindowGuides();
            window.ShowDialog();
        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {
            WindowPlaces window = new WindowPlaces();
            window.ShowDialog();
        }

        private void GuidePlaceReport_Click(object sender, RoutedEventArgs e)
        {
            WindowGuidePlaceReport window = new WindowGuidePlaceReport();
            window.ShowDialog();
        }

        private void TourReport_Click(object sender, RoutedEventArgs e)
        {
            WindowTourReport window = new WindowTourReport();
            window.ShowDialog();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            WindowTourReport window = new WindowTourReport();
            window.ShowDialog();
        }

        private void RegOrAut_Click(object sender, RoutedEventArgs e)
        {
            WindowRegOrAut window = new WindowRegOrAut();
            window.ShowDialog();
        }
    }
}
