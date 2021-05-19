using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using VetClinikEntitiesImplements.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VetClinikEntitiesImplements;

namespace VetClinikEntitiesImplements.Implements
{
    public class PurchaseStorage : IPurchaseStorage
    {
        public List<PurchaseViewModel> GetFilteredList(PurchaseBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Purchases
                .Include(rec => rec.AnimalsPurchases)
                .ThenInclude(rec => rec.Animal)
                .Include(rec => rec.MedicinesPurchases)
                .ThenInclude(rec => rec.Medicine)
                .ToList().Where(rec => rec.DatePayment >= model.DateFrom && rec.DatePayment <= model.DateTo)
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    Sum = rec.Sum,
                    DatePayment = rec.DatePayment,
                    MedicinesPurchases = rec.MedicinesPurchases.ToDictionary(recTC => context.Medicines.FirstOrDefault(recMN => recMN.Id == recTC.MedicineId).Id,
                    recTC => (recTC.Medicine.MedicineName, recTC.Count, context.Medicines.FirstOrDefault(recMP => recMP.Id == recTC.MedicineId).Cost)),
                    AnimalsPurchases = rec.AnimalsPurchases.Select(recAP => recAP.Animal.AnimalName).ToList()
                })
                .ToList();
            }
        }

        public List<PurchaseViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Purchases
                .Include(rec => rec.AnimalsPurchases)
                .ThenInclude(rec => rec.Animal)
                .Include(rec => rec.MedicinesPurchases)
                .ThenInclude(rec => rec.Medicine)
                .ToList()
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    Sum = rec.Sum,
                    DatePayment = rec.DatePayment,
                    MedicinesPurchases = rec.MedicinesPurchases.ToDictionary(recTC => context.Medicines.FirstOrDefault(recMN => recMN.Id == recTC.MedicineId).Id,
                    recTC => (recTC.Medicine.MedicineName,recTC.Count, context.Medicines.FirstOrDefault(recMP => recMP.Id == recTC.MedicineId).Cost)),
                    AnimalsPurchases = rec.AnimalsPurchases.Select(recAP => recAP.Animal.AnimalName).ToList()
                })
                .ToList();
            }
        }
    }
}
