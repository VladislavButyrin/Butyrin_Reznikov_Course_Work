using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [Required]
        public string GuideName { get; set; }
        [Required]
        public string Passport { get; set; }
        [ForeignKey("GuideId")]
        public Trip Trip { get; set; }

        public int ExcursionId { set; get; }

    }
}
