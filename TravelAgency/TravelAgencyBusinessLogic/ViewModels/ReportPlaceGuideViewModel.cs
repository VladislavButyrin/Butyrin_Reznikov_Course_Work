﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportPlaceGuideViewModel
    {
        public DateTime Date { get; set; }
        public string PlaceName { get; set; }
        public string GuideName { get; set; }
        public int GuideCount { get; set; }
    }
}
