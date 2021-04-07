using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Guide
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Passport { get; set; }

        public Trip Trip { get; set; }
       
    }
}
