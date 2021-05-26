using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    /// <summary>
    /// Гид
    /// </summary>
    public class GuideBindingModel
    {
        public int? Id { get; set; }
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
    }
}
