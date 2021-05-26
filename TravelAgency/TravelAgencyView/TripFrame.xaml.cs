using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using System;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для TripFrame.xaml
    /// </summary>
    public partial class TripFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TripBusinessLogic logic;
        public int Id { set { id = value; } }
        private int? id;
        public TripFrame(TripBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void TripFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new TripBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameTextBox.Text = view.TripName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка");
                return;
            }
            try
            {
                logic.CreateOrUpdate(new TripBindingModel
                {
                    Id = id,
                    TripName = NameTextBox.Text
                });
                this.DialogResult = true;
                MessageBox.Show("Сохранение прошло успешно", "Сообщение");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
