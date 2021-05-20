using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class OrganizatorBindingModel
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
    }
}
