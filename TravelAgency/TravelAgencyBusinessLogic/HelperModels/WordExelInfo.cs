using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.HelperModels
{
    public class WordExelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportViewModel> Places { get; set; }
        public List<string> NeededPlaces { get; set; }
    }
}