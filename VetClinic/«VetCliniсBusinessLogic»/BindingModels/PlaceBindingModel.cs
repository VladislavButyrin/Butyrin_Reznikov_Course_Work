using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Услуга, оказываемая ветеринарной клиникой
    /// </summary>
    public class PlaceBindingModel
    {
        public int? Id { get; set; }
        public string PlaceName { get; set; }
        public int? OrganizatorId { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
    }
}
