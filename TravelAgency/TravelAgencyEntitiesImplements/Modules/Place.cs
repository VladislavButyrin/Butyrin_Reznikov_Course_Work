using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность места
    /// </summary>
    public class Place
    {
        public int Id { get; set; }
        [Required]
        public int OrganizatorId { get; set; }
        [Required]
        public string PlaceName { get; set; }
        [Required]
        public string Adress { get; set; }

        [ForeignKey("PlaceId")]
        public virtual List<TripPlace> TripsPlaces { get; set; }
        [ForeignKey("PlaceId")]
        public virtual List<GroupPlace> GroupsPlaces { get; set; }
        public virtual Organizator Organizator { get; set; }
    }
}
