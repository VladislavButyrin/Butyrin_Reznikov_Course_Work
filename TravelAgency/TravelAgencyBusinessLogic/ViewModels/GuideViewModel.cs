using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО гида")]
        public string GuideName { get; set; }

        [DisplayName("Паспортные данные")]
        public string Passport { get; set; }

        public int TripId { get; set; }

    }
}
