using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class PurchaseBindingModel
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime DatePayment { get; set; }
        public Dictionary<int, (string,int, int)> MedicinesPurchases { get; set; }
        public List<string> AnimalsPurchases { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
