namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Добавление услуги к посещению врача
    /// </summary>
    public class AddPlaceToGroupBindingModel
    {
        public int OrganizatorGroupId { get; set; }
        public PlaceBindingModel Place { get; set; }
    }
}
