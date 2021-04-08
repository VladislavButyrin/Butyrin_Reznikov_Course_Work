using System;
using System.ComponentModel;



namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название тура")]
        public string TourName { get; set; }

        [DisplayName("Дата отправления")]
        public DateTime DateOfDeparture { get; set; }

        [DisplayName("Место проживания")]
        public string PlaceOfResidence { get; set; }

        [DisplayName("Место назначения")]
        public string PlaceOfDeparture { get; set; }
    }
}
