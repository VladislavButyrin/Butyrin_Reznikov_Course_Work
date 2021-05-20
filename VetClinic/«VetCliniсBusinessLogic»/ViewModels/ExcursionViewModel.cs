using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class ExcursionViewModel
    {
        public int Id { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Дата оплаты")]
        public DateTime DatePayment { get; set; }
        public Dictionary<int, (string, int, int)> GuidesExcursions { get; set; }
        public List<string> ToursExcursions { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
