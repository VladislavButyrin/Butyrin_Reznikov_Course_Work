using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateGroup { get; set; }
        public Dictionary<int, string> ToursGroups { get; set; }
        public Dictionary<int, string> GroupsPlaces { get; set; }
    }
}