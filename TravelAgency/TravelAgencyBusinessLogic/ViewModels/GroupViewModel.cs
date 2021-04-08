using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public Dictionary<int, (int, string)> Tours { get; set; }

    }
}
