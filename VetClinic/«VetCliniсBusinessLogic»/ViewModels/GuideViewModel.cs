using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лекарства")]
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
    }
}
