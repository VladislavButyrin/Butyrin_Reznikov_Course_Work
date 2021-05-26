using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public int UserId { get; set; }
        public string LoginCurrentUserInSystem { get; set; }
        public List<string> ToursName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}