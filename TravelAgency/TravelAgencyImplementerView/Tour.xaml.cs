using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace TravelAgencyImplementerView
{
    public partial class Tour : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TourLogic tour_logic;
        public int Id { set { id = value; } }
        private int? id;
        public Tour(TourLogic _tour_logic)
        {
            InitializeComponent();
            tour_logic = _tour_logic;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                tour_logic.CreateOrUpdate(new TourBindingModel
                {
                    Id = id,
                    TourName = NameTextBox.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Tour_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = tour_logic.Read(new TourBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameTextBox.Text = view.TourName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}