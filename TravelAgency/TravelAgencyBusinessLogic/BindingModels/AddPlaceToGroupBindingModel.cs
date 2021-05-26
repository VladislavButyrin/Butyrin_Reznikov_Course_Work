using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class AddPlaceToGroupBindingModel
    {
        public int OrganizatorGroupId { get; set; }
        public PlaceBindingModel Place { get; set; }
    }
}
