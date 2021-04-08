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
    /// Логика взаимодействия для WindowGroups.xaml
    /// </summary>
    public partial class WindowGroups : Window
    {
        public WindowGroups()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowGroup window = new WindowGroup();
            window.ShowDialog();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            WindowGroup window = new WindowGroup();
            window.ShowDialog();
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
