using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using Unity;

namespace TravelAgencyImplementerView
{
    public partial class GuideBinding : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ExcursionLogic excursion_logic;
        private readonly GuideLogic guide_logic;
        private readonly Dictionary<string, (int, int)> GuidesExcursions = new Dictionary<string, (int, int)>();
        public GuideBinding(ExcursionLogic _excursion_logic, GuideLogic _guide_logic)
        {
            InitializeComponent();
            excursion_logic = _excursion_logic;
            guide_logic = _guide_logic;
            FillData();
        }
        private void FillData()
        {
            SelectedExcursionComboBox.ItemsSource = excursion_logic.Read(new ExcursionBindingModel
            {
                UserId = (int)App.User.UserId
            });
            SelectedGuideComboBox.ItemsSource = guide_logic.Read(null);
        }
        private void SaveData()
        {
            try
            {
                GuidesExcursions.Add(((GuideViewModel)SelectedGuideComboBox.SelectedItem).GuideName, 
                    (Convert.ToInt32(CountTextBox.Text), ((GuideViewModel)SelectedGuideComboBox.SelectedItem).Cost));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedExcursionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите ID покупки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedGuideComboBox.SelectedItem == null)
            {
                MessageBox.Show("Укажите лекарство", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CountTextBox.Text == null)
            {
                MessageBox.Show("Укажите количество лекарства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                SaveData();
                int sum = Convert.ToInt32(SumTextBox.Text);
                int count = Convert.ToInt32(CountTextBox.Text);
                excursion_logic.GuideBinding(new AddingGuideBindingModel
                {
                    ExcursionId = ((ExcursionViewModel)SelectedExcursionComboBox.SelectedItem).Id,
                    GuideName = SelectedGuideComboBox.SelectedItem.ToString(),
                    Sum = sum,
                    Count = count,
                    Cost = sum / count
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void Count_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (SelectedExcursionComboBox.SelectedItem != null && SelectedGuideComboBox.SelectedItem != null)
                {
                    SumTextBox.Text = (Convert.ToInt32(CountTextBox.Text) * ((GuideViewModel)SelectedGuideComboBox.SelectedItem).Cost).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SelectedExcursionComboBox_SelectionChangeCommitted(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var form = Container.Resolve<Excursion>();
            form.Id = ((ExcursionViewModel)SelectedExcursionComboBox.SelectedItem).Id;
            form.ReportWindow();
            form.ShowDialog();
        }
    }
}