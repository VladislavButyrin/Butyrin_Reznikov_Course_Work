using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class ReportBindingModelGuarantor
    {
        public string FileName { get; set; }
        public List<int> Trips { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int OrganizatorId { get; set; }
    }
}
