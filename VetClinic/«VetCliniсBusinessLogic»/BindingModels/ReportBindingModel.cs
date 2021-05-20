using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public List<int> Trips { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
