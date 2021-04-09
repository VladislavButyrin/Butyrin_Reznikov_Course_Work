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
    /// Логика взаимодействия для WindowRegOrAut.xaml
    /// </summary>
    public partial class WindowRegOrAut : Window
    {
        public WindowRegOrAut()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration window = new WindowRegistration();
            window.ShowDialog();
        }

        private void buttonAut_Click(object sender, RoutedEventArgs e)
        {
            WindowAutorization window = new WindowAutorization();
            window.ShowDialog();
        }
    }
}
