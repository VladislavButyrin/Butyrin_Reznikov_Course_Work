using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyListImplement.Models
{
    public class User
    {
        public int? Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
    }
}
