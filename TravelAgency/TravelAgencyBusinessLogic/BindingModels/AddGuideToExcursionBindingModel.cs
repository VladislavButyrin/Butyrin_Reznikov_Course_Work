using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class AddGuideToExcursionBindingModel
    {
        public int ExcursionId { get; set; }
        public GuideViewModel Guide { get; set; }
    }
}
