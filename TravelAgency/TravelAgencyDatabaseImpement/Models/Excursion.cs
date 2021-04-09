using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Excursion
    {
        public int Id { get; set; }
        [Required]
        public string ExcursionName { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("ExcursionId")]
        public Guide Guides { get; set; }
        [ForeignKey("ExcursionId")]
        public List<Tour> Tours { get; set; }

    }
}
