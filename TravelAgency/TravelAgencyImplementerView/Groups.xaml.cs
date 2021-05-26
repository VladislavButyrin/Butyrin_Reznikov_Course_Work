using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using Unity;
using System;

namespace TravelAgencyImplementerView
{
    public partial class Groups : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly GroupLogic group_logic;
        public Groups(GroupLogic _group_logic)
        {
            InitializeComponent();
            group_logic = _group_logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = group_logic.Read(null);
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<Group>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<Group>();
                form.Id = ((GroupViewModel)dataGridView.SelectedItem).Id;
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
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = Convert.ToInt32(((GroupViewModel)dataGridView.SelectedItem).Id);
                    try
                    {
                        group_logic.Delete(new GroupBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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