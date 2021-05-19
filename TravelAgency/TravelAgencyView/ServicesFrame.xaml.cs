using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для ServicesFrame.xaml
    /// </summary>
    public partial class ServicesFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ServiceBusinessLogic logic;
        public ServicesFrame(ServiceBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                int x = App.DoctorId;
                var list = logic.Read(new ServiceBindingModel { DoctorId = App.DoctorId });
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.ColumnWidth = DataGridLength.Auto;
                    var columns = dataGridView.Columns;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ServiceFrame>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<ServiceFrame>();
                form.Id = ((ServiceViewModel)dataGridView.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = Convert.ToInt32(((ServiceViewModel)dataGridView.SelectedItem).Id);
                    try
                    {
                        logic.Delete(new ServiceBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
