using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название места")]
        public string PlaceName { get; set; }

        [DisplayName("Адрес")]
        public string Adress { get; set; }

        public Dictionary<int, string> Trips { get; set; }
        public override string ToString()
        {
            return PlaceName;
        }
    }
}