using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название медикамента")]
        public string TripName { get; set; }
        public override string ToString()
        {
            return TripName;
        }
    }
}
