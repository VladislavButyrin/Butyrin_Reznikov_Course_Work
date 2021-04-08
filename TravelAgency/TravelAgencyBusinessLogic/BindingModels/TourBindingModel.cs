using System;



namespace TravelAgencyBusinessLogic.BindingModels
{
    public class TourBindingModel
    {
        public int? Id { get; set; }

        public string TourName { get; set; }

        public string PlaceOfResidence { get; set; }

        public string PlaceOfDeparture { get; set; }

        public DateTime DateOfDeparture { get; set; }

    }
}
