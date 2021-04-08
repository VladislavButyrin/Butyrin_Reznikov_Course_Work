using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class UserBindingModel
    {
        public int? Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
    }
}
