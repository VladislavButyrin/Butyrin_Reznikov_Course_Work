﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    public class Purchase
    {
        public int Id { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public DateTime DatePayment { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual List<MedicinePurchase> MedicinesPurchases { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual List<AnimalPurchase> AnimalsPurchases { get; set; }
    }
}
