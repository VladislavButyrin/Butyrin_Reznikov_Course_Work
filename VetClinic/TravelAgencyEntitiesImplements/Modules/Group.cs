using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность группы
    /// </summary>
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public int PeopleQuantity { get; set; }
        [Required]
        public DateTime DateGroup { get; set; }
        [ForeignKey("GroupId")]
        public virtual List<TourGroup> ToursGroups { get; set; }
        [ForeignKey("GroupId")]
        public virtual List<GroupPlace> GroupPlaces { get; set; }
    }
}
