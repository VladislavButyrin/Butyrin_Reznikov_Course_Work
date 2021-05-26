using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using System;

namespace TravelAgencyBusinessLogic.HelperModels.Guarantor
{
    class WordExelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportExcursionTripViewModel> GuideTrips { get; set; }
        public List<String> NeededTrips { get; set; }
    }
}
