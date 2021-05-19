using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using System;

namespace _VetCliniсBusinessLogic_.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportServiceMedicineViewModel> ServicesMedicines { get; set; }
    }
}
