using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using VetClinikEntitiesImplements;
using _VetCliniсBusinessLogic_.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace VetClinikEntitiesImplements.Implements
{
    public class MedicineStorage : IMedicineStorage
    {
        public void Delete(MedicineBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Medicine element = context.Medicines.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Medicines.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Лекарство не найдено");
                }
            }
        }
        public MedicineViewModel GetElement(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var service = context.Medicines.Include(rec => rec.MedicationsMedicines).ThenInclude(rec => rec.Medication).FirstOrDefault(rec => rec.MedicineName == model.MedicineName || rec.Id == model.Id);
                return service != null ?
                new MedicineViewModel
                {
                    Id = service.Id,
                    MedicineName = service.MedicineName,
                    Cost = service.Cost,
                    Medications = service.MedicationsMedicines
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
               (recPC.Medication?.MedicationName))
                } :
               null;
            }
        }

        public List<MedicineViewModel> GetFilteredList(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Medicines
                .Include(rec => rec.MedicationsMedicines)
               .ThenInclude(rec => rec.Medication)
               .Where(rec => rec.MedicineName.Contains(model.MedicineName))
               .ToList()
               .Select(rec => new MedicineViewModel
               {
                   Id = rec.Id,
                   MedicineName = rec.MedicineName,
                   Cost = rec.Cost,
                   Medications = rec.MedicationsMedicines
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
                (recPC.Medication?.MedicationName))
               }).ToList();
            }
        }

        public List<MedicineViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Medicines
                .Include(rec => rec.MedicationsMedicines)
               .ThenInclude(rec => rec.Medication)
               .ToList()
               .Select(rec => new MedicineViewModel
               {
                   Id = rec.Id,
                   MedicineName = rec.MedicineName,
                   Cost = rec.Cost,
                   Medications = rec.MedicationsMedicines
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
                (recPC.Medication?.MedicationName))
               }).ToList();
            }
        }

        public void Insert(MedicineBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Medicine p = new Medicine
                        {
                            MedicineName = model.MedicineName,
                            Cost = model.Cost
                        };
                        context.Medicines.Add(p);
                        context.SaveChanges();
                        CreateModel(model, p, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(MedicineBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Medicines.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        private Medicine CreateModel(MedicineBindingModel model, Medicine medicine, VetClinicDataBase context)
        {
            medicine.MedicineName = model.MedicineName;
            medicine.Cost = model.Cost;
            if (model.Id.HasValue)
            {
                var medicineMedications = context.MedicationsMedicines.Where(rec =>
               rec.MedicineId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.MedicationsMedicines.RemoveRange(medicineMedications.Where(rec =>
               !model.Medications.ContainsKey(rec.MedicationId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateMedication in medicineMedications)
                {
                    model.Medications.Remove(updateMedication.MedicationId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.Medications)
            {
                context.MedicationsMedicines.Add(new MedicationMedicine
                {
                    MedicineId = medicine.Id,
                    MedicationId = pc.Key,
                });
                context.SaveChanges();
            }
            return medicine;
        }
    }
}
