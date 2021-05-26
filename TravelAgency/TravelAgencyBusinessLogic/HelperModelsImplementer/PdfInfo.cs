using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using System;

namespace TravelAgencyBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportToursGroupsExcursionsViewModel> ToursGroupsExcursions { get; set; }
    }
}