using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class GroupPlace
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int GroupId { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        public virtual Place Place { get; set; }
    }
}