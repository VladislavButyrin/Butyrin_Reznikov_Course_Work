using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата посещения")]
        public DateTime DateVisit { get; set; }
        public Dictionary<int, string> AnimalsVisits { get; set; }
        public Dictionary<int, string> ServicesVisits { get; set; }
        public override string ToString()
        {
            return DateVisit.ToString();
        }
    }
}
