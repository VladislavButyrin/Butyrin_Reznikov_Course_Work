using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TouristViewModel
    {
        public int? Id { set; get; }
        [DisplayName("Имя туриста")]
        public string TouristName { set; get; }
        [DisplayName("Почта туриста")]
        public string Email { set; get; }
    }
}
