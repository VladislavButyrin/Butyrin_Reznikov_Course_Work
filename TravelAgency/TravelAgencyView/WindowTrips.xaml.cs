using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для WindowTrips.xaml
    /// </summary>
    public partial class WindowTrips : Window
    {
        public WindowTrips()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowTrip window = new WindowTrip();
            window.ShowDialog();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            WindowTrip window = new WindowTrip();
            window.ShowDialog();
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
