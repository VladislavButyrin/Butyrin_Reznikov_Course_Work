using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Group
    {
        public int Id { get; set; }
        public DateTime DateGroup { get; set; }
        public int UserId { get; set; }
        [ForeignKey("GroupId")]
        public virtual List<TourGroup> ToursGroups { get; set; }
        [ForeignKey("GroupId")]
        public virtual List<GroupPlace> GroupPlaces { get; set; }
        public virtual User User { get; set; }
    }
}