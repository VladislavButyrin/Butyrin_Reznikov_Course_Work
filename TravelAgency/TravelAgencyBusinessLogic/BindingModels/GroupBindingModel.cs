using System.Collections.Generic;


namespace TravelAgencyBusinessLogic.BindingModels
{
    public class GroupBindingModel
    {
        public int? Id { get; set; }

        public Dictionary<int, (string, int)> Tours { get; set; }

    }
}
