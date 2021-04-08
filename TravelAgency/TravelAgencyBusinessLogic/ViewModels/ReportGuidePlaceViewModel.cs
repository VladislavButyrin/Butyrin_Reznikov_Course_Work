using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportGuidePlaceViewModel
    {
        [DisplayName("Имя гида")]
        public string GuideName { get; set; }

        [DisplayName("Название места")]
        public string PlaceName { get; set; }

    }
}
