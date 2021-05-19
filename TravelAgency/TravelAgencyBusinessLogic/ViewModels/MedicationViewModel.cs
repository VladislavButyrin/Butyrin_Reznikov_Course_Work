using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class MedicationViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название медикамента")]
        public string MedicationName { get; set; }
        public override string ToString()
        {
            return MedicationName;
        }
    }
}
