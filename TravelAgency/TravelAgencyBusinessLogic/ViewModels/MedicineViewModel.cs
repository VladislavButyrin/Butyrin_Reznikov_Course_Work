using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лекарства")]
        public string MedicineName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Medications { get; set; }
    }
}
