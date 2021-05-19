using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для MedicationFrame.xaml
    /// </summary>
    public partial class MedicationFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly MedicationBusinessLogic logic;
        public int Id { set { id = value; } }
        private int? id;
        public MedicationFrame(MedicationBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void MedicationFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new MedicationBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameTextBox.Text = view.MedicationName;
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
                logic.CreateOrUpdate(new MedicationBindingModel
                {
                    Id = id,
                    MedicationName = NameTextBox.Text
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
