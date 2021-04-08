using System;


namespace TravelAgencyBusinessLogic.BindingModels
{
    public class TripBindingModel
    {
        public int? Id { get; set; }

        public string TripName { get; set; }

        public DateTime Date { get; set; }
    }
}
