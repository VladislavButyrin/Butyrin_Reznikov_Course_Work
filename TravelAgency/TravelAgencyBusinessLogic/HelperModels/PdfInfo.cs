using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace TravelAgencyBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
