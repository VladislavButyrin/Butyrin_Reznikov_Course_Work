using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class ExcursionBindingModel
    {
        public int? Id { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }
        public DateTime DatePayment { get; set; }
        public Dictionary<string, (int, int)> GuidesExcursions { get; set; }
        public List<string> ToursExcursions { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}