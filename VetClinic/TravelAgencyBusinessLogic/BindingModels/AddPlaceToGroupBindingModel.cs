namespace TravelAgencyBusinessLogic.BindingModels
{
    /// <summary>
    /// Добавление места к группе
    /// </summary>
    public class AddPlaceToGroupBindingModel
    {
        public int OrganizatorGroupId { get; set; }
        public PlaceBindingModel Place { get; set; }
    }
}
