using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportTourViewModel
    {
        [DisplayName("Название тура")]
        public string TourName { get; set; }

        [DisplayName("Экскурсии")]
        public List<string> Excursions { get; set; }

        [DisplayName("Номер группы")]
        public int GroupId { get; set; }

    }
}
