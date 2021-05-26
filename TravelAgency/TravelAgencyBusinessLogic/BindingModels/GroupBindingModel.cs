using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class GroupBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateGroup { get; set; }
        public int PeopleQuantity { set; get; }
        public Dictionary<int, string> ToursGroups { get; set; }
        public Dictionary<int, string> GroupsPlaces { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}