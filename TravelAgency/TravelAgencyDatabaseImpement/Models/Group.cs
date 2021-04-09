using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Group
    {
        public int Id { get; set; }
        [ForeignKey("GroupId")]
        public List<Tour> Tours { get; set; }
        [Required]
        public int PeopleQuantity { get; set; }
        [ForeignKey("GroupId")]
        public List<Tourist> Tourists { set; get; }

        public int PlaceId { set; get; }
    }
}
