using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using VetClinikEntitiesImplements;
using _VetCliniсBusinessLogic_.ViewModels;

namespace VetClinikEntitiesImplements.Implements
{
    public class MedicationStorage : IMedicationStorage
    {
        public void Delete(MedicationBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Medication element = context.Medications.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Medications.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public MedicationViewModel GetElement(MedicationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var medication = context.Medications
                .FirstOrDefault(rec => rec.MedicationName == model.MedicationName ||
               rec.Id == model.Id);
                return medication != null ?
                new MedicationViewModel
                {
                    Id = medication.Id,
                    MedicationName = medication.MedicationName
                } :
               null;
            }
        }

        public List<MedicationViewModel> GetFilteredList(MedicationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Medications
                .Where(rec => rec.MedicationName.Contains(model.MedicationName))
               .Select(rec => new MedicationViewModel
               {
                   Id = rec.Id,
                   MedicationName = rec.MedicationName
               })
                .ToList();
            }
        }

        public List<MedicationViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Medications
                .Select(rec => new MedicationViewModel
                {
                    Id = rec.Id,
                    MedicationName = rec.MedicationName
                })
.ToList();
            }
        }

        public void Insert(MedicationBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                context.Medications.Add(CreateModel(model, new Medication()));
                context.SaveChanges();
            }
        }

        public void Update(MedicationBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                var element = context.Medications.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private Medication CreateModel(MedicationBindingModel model, Medication medication)
        {
            medication.MedicationName = model.MedicationName;
            return medication;
        }
    }
}
