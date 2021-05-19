namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Добавление услуги к посещению врача
    /// </summary>
    public class AddServiceToVisitBindingModel
    {
        public int DoctorVisitId { get; set; }
        public ServiceBindingModel Service { get; set; }
    }
}
