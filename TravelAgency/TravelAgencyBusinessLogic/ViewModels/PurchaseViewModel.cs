using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Дата оплаты")]
        public DateTime DatePayment { get; set; }
        public Dictionary<int, (string, int, int)> MedicinesPurchases { get; set; }
        public List<string> AnimalsPurchases { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
