using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class PlaceBindingModel
    {
        public int? Id { get; set; }
        public string PlaceName { get; set; }
        public int? OrganizatorId { get; set; }
        public string Adress { get; set; }
        public Dictionary<int, string> Trips { get; set; }
    }
}