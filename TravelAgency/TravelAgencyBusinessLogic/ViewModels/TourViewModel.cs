using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int? Id { get; set; }
        public string TourName { get; set; }
        public List<string> Places { get; set; } = new List<string>();
        public override string ToString()
        {
            return TourName;
        }
    }
}