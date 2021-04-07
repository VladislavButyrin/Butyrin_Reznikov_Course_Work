using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название места")]
        public string Name { get; set; }

        [DisplayName("Адрес")]
        public string Adress { get; set; }

        public List<TripViewModel> Trips { get; set; }

        public int GroupId { get; set; }
       
    }
}
