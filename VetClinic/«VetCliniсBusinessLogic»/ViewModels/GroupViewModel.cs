using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата посещения")]
        public DateTime DateGroup { get; set; }
        public Dictionary<int, string> ToursGroups { get; set; }
        public Dictionary<int, string> PlacesGroups { get; set; }
        public override string ToString()
        {
            return DateGroup.ToString();
        }
    }
}
