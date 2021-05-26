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
        public int PeopleQuantity { get; set; }
        public string GroupName { set; get; }
        public Dictionary<int, string> ToursGroups { get; set; }
        public Dictionary<int, string> GroupsPlaces { get; set; }
        public override string ToString()
        {
            return GroupName + " " + DateGroup;
        }
    }
}