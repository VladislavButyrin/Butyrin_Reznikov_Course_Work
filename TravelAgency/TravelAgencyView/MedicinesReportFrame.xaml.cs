using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MedicinesReportFrame.xaml
    /// </summary>
    public partial class MedicinesReportFrame : Window
    {
        private readonly ReportLogic report_logic;
        private readonly MedicationBusinessLogic medication_logic;
        private Dictionary<int, string> Medications;
        public MedicinesReportFrame(ReportLogic report_logic, MedicationBusinessLogic medication_logic)
        {
            InitializeComponent();
            this.report_logic = report_logic;
            this.medication_logic = medication_logic;
        }
        private void LoadData()
        {
            try
            {
                if (Medications != null)
                {
                    CanSelectedMedicationsListBox.Items.Clear();
                    foreach (var m in medication_logic.Read(null))
                    {
                        CanSelectedMedicationsListBox.Items.Add(m);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<int> selected_medications = new List<int>();
                    foreach (var item in CanSelectedMedicationsListBox.SelectedItems)
                    {
                        selected_medications.Add(((MedicationViewModel)item).Id);
                    }
                    report_logic.SavePurchasesToWordFile(new ReportBindingModel { FileName = dialog.FileName, Medications = selected_medications });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
               MessageBoxImage.Information);
                }
            }
        }

        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<int> selected_medications = new List<int>();
                    foreach (var item in CanSelectedMedicationsListBox.SelectedItems)
                    {
                        selected_medications.Add(((MedicationViewModel)item).Id);
                    }
                    report_logic.SavePurchasesToExcelFile(new ReportBindingModel { FileName = dialog.FileName, Medications = selected_medications });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
               MessageBoxImage.Information);
                }
            }
        }

        private void CancelButton__Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void MedicinesReportFrame_Load(object sender, RoutedEventArgs e)
        {
            Medications = new Dictionary<int, string>();
            LoadData();
        }
    }
}
