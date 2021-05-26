using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Organizator
    {
        public int Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Login { get; set; }
        [ForeignKey("OrganizatorId")]
        public virtual List<Place> Places { get; set; }
    }
}