using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using System;

namespace TravelAgencyBusinessLogic.HelperModels.Guarantor
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportPlaceGuideViewModel> PlacesGuides { get; set; }
    }
}
