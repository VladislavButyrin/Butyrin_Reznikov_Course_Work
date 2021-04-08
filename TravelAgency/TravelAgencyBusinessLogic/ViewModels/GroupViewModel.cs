using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GroupViewModel
    {
        public int? Id { get; set; }

        public int PeopleQuantity { get; set; }

        public Dictionary<int, string> Tours { get; set; }

        public Dictionary<int, string> Tourists { set; get; }
    }
}
