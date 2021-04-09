using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Tourist
    {
        public int Id { set; get; }
        [Required]
        public string TouristName { set; get; }
        [Required]
        public string Email { set; get; }

        public int GroupId { set; get; }
    }
}
