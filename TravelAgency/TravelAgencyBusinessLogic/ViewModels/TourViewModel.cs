using System;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название тура")]
        public string TourName { get; set; }
    }
}
