using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Unity;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MedicationsFrame.xaml
    /// </summary>
    public partial class MedicationsFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly MedicationBusinessLogic logic;
        public MedicationsFrame(MedicationBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.ColumnWidth = DataGridLength.Auto;
                    var columns = dataGridView.Columns;
                    //dataGridView.Columns[1].Visibility = Visibility.Collapsed;
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
            var form = Container.Resolve<MedicationFrame>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<MedicationFrame>();
                form.Id = ((MedicationViewModel)dataGridView.SelectedItem).Id;
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
                    int id = Convert.ToInt32(((MedicationViewModel)dataGridView.SelectedItem).Id);
                    try
                    {
                        logic.Delete(new MedicationBindingModel { Id = id });
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
