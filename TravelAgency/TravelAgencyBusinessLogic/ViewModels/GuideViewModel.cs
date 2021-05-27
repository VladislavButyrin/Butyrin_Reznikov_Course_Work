using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО гида")]
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
        public override string ToString() {
            return GuideName;
        }
    }
}