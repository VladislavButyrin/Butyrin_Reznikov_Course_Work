using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class GroupBindingModel
    {
        public int? Id { get; set; }
        public DateTime DateGroup { get; set; }
        public Dictionary<int, string> ToursGroups { get; set; }
        public Dictionary<int, string> PlacesGroups { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
