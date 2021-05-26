using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class StatisticsBindingModelGuarantor
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int ElementId { get; set; }
    }
}
