using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ExcursionViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название экскурсии")]
        public string Name { get; set; }

        [DisplayName("Дата проведения")]
        public DateTime Date { get; set; }

        public int GuideId { get; set; }

        public Dictionary<int, (string,int)> Tours { get; set; }
       
    }
}
