using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class ReportServiceMedicineViewModel
    {
        public DateTime Date { get; set; }
        public string ServiceName { get; set; }
        public string MedicineName { get; set; }
        public int MedicineCount { get; set; }
    }
}
