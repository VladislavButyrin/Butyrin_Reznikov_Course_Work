using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportViewModel
    {
        public string TourName { get; set; }
        public List<string> Places { get; set; }
        internal IEnumerable<string> ToArray()
        {
            return Places.ToArray();
        }
    }
}