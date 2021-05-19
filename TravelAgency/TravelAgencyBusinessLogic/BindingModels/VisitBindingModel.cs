using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class VisitBindingModel
    {
        public int? Id { get; set; }
        public DateTime DateVisit { get; set; }
        public Dictionary<int, string> AnimalsVisits { get; set; }
        public Dictionary<int, string> ServicesVisits { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
