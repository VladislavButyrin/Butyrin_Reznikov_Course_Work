using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ExcursionViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Sum { get; set; }
        public string Username { get; set; }
        public DateTime DatePayment { get; set; }
        public Dictionary<string, (int, int)> GuidesExcursions { get; set; }
        public List<string> ToursExcursions { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}