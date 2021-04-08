using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class ExcursionBindingModel
    {
        public int? Id { get; set; }

        public string ExcursionName { get; set; }

        public string Place { get; set; }

        public DateTime Date { get; set; }

        public int GuideId { get; set; }

        public Dictionary<int, (string,int)> Tours { get; set; }

    }
}
