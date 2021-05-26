using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название поездки")]
        public string TripName { get; set; }
        public override string ToString()
        {
            return TripName;
        }
    }
}
