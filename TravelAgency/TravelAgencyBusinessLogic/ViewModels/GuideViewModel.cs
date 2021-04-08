using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО гида")]
        public string Name { get; set; }

        public int TripId { get; set; }

    }
}
