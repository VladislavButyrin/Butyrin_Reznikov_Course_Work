using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using System;

namespace _VetCliniсBusinessLogic_.HelperModels
{
    class WordExelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportPurchaseMedicationViewModel> MedicineMedications { get; set; }
        public List<String> NeededMedications { get; set; }
    }
}
