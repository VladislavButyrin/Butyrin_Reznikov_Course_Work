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
    /// Логика взаимодействия для WindowGroup.xaml
    /// </summary>
    public partial class WindowGroup : Window
    {
        public WindowGroup()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowGroupTourist window = new WindowGroupTourist();
            window.ShowDialog();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
