﻿using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MedicinesFrame.xaml
    /// </summary>
    public partial class MedicinesFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly MedicineBusinessLogic logic;
        public MedicinesFrame(MedicineBusinessLogic logic)
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
            var form = Container.Resolve<MedicineFrame>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<MedicineFrame>();
                form.Id = ((MedicineViewModel)dataGridView.SelectedItem).Id;
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
                    int id = Convert.ToInt32(((MedicineViewModel)dataGridView.SelectedItem).Id);
                    try
                    {
                        logic.Delete(new MedicineBindingModel { Id = id });
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
