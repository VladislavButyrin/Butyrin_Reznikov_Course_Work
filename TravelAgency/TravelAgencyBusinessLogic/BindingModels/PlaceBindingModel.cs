using System.Collections.Generic;


namespace TravelAgencyBusinessLogic.BindingModels
{
    public class PlaceBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public Dictionary<int, int> Trips { get; set; }

        public int GroupId { get; set; }

    }
}
