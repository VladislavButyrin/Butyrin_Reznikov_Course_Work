using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MedicineFrame.xaml
    /// </summary>
    public partial class MedicineFrame : Window
    {
        public int Id { set { id = value; } }
        private readonly MedicineBusinessLogic medicine_logic;
        private readonly MedicationBusinessLogic medication_logic;
        private int? id;
        private Dictionary<int, string> medicineMedications;
        public MedicineFrame(MedicineBusinessLogic medicine_logic, MedicationBusinessLogic medication_logic)
        {
            InitializeComponent();
            this.medicine_logic = medicine_logic;
            this.medication_logic = medication_logic;
        }
        private void LoadData()
        {
            try
            {
                if (medicineMedications != null)
                {
                    SelectedMedicationsListBox.Items.Clear();
                    foreach (var mm in medicineMedications)
                    {
                        SelectedMedicationsListBox.Items.Add(mm.Value);
                    }
                }
                CanSelectedMedicationsListBox.Items.Clear();
                foreach (var m in medication_logic.Read(null))
                {
                    if (medicineMedications.Values.Where(rec => rec == m.MedicationName).ToList().Count == 0)
                        CanSelectedMedicationsListBox.Items.Add(m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void MedicineFrame_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    MedicineViewModel view = medicine_logic.Read(new MedicineBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    medicineMedications = view.Medications;
                    NameTextBox.Text = view.MedicineName;
                    CostTextBox.Text = view.Cost.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                }
            }
            else
            {
                medicineMedications = new Dictionary<int, string>();
            }
            LoadData();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(CostTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (medicineMedications == null || medicineMedications.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                medicine_logic.CreateOrUpdate(new MedicineBindingModel
                {
                    Id = id,
                    MedicineName = NameTextBox.Text,
                    Cost = int.Parse(CostTextBox.Text),
                    Medications = medicineMedications
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK,
               MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMedicationsListBox.SelectedItems.Count == 1)
            {
                medicineMedications.Remove(medicineMedications.Where(rec => rec.Value == (string)SelectedMedicationsListBox.SelectedItem).ToList()[0].Key);
                LoadData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedMedicationsListBox.SelectedItems.Count == 1)
            {
                medicineMedications.Add(((MedicationViewModel)CanSelectedMedicationsListBox.SelectedItem).Id
                    , ((MedicationViewModel)CanSelectedMedicationsListBox.SelectedItem).MedicationName);
                LoadData();
            }
        }
    }
}
