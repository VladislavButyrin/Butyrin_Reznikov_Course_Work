using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class TouristBindingModel
    {
        public int? Id { set; get; }
        public string TouristName { set; get; }
        public string Email { set; get; }
    }
}
