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
    /// Логика взаимодействия для WindowExcursion.xaml
    /// </summary>
    public partial class WindowExcursion : Window
    {
        public WindowExcursion()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowExcursionTour window = new WindowExcursionTour();
            window.ShowDialog();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            WindowExcursionTour window = new WindowExcursionTour();
            window.ShowDialog();
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
