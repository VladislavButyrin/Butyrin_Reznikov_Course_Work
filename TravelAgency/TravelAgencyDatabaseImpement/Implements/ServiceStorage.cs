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
    public class ServiceStorage : IServiceStorage
    {
        public void Delete(ServiceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Service element = context.Services.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Services.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Услуга не найдена");
                }
            }
        }

        public ServiceViewModel GetElement(ServiceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var service = context.Services.Include(rec => rec.MedicationsServices).ThenInclude(rec => rec.Medication).FirstOrDefault(rec => rec.ServiceName == model.ServiceName || rec.Id == model.Id);
                return service != null ?
                new ServiceViewModel
                {
                    Id = service.Id,
                    ServiceName = service.ServiceName,
                    Cost = service.Cost,
                    FIO = context.Doctors.FirstOrDefault(doc => doc.Id == service.DoctorId).FIO,
                    Medications = service.MedicationsServices
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
               (recPC.Medication?.MedicationName))
                } :
               null;
            }
        }

        public List<ServiceViewModel> GetFilteredList(ServiceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Services
                .Include(rec => rec.MedicationsServices)
               .ThenInclude(rec => rec.Medication)
               .Where(rec => rec.ServiceName.Contains(model.ServiceName) || model.DoctorId == rec.DoctorId)
               .ToList()
               .Select(rec => new ServiceViewModel
               {
                   Id = rec.Id,
                   ServiceName = rec.ServiceName,
                   Cost = rec.Cost,
                   FIO = context.Doctors.FirstOrDefault(doc => doc.Id == rec.DoctorId).FIO,
                   Medications = rec.MedicationsServices
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
                (recPC.Medication?.MedicationName))
               }).ToList();
            }
        }

        public List<ServiceViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Services
                .Include(rec => rec.MedicationsServices)
               .ThenInclude(rec => rec.Medication)
               .ToList()
               .Select(rec => new ServiceViewModel
               {
                   Id = rec.Id,
                   ServiceName = rec.ServiceName,
                   Cost = rec.Cost,
                   FIO = context.Doctors.FirstOrDefault(doc => doc.Id == rec.DoctorId).FIO,
                   Medications = rec.MedicationsServices
                .ToDictionary(recPC => recPC.MedicationId, recPC =>
                (recPC.Medication?.MedicationName))
               }).ToList();
            }
        }

        public void Insert(ServiceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Service p = new Service
                        {
                            ServiceName = model.ServiceName,
                            Cost = model.Cost,
                            DoctorId = (int)model.DoctorId
                        };
                        context.Services.Add(p);
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

        public void Update(ServiceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Услуга не найдена");
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

        private Service CreateModel(ServiceBindingModel model, Service service, VetClinicDataBase context)
        {
            service.ServiceName = model.ServiceName;
            service.DoctorId = (int)model.DoctorId;
            service.Cost = model.Cost;
            if (model.Id.HasValue)
            {
                var serviceMedications = context.MedicationsServices.Where(rec =>
               rec.ServiceId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.MedicationsServices.RemoveRange(serviceMedications.Where(rec =>
               !model.Medications.ContainsKey(rec.MedicationId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateMedication in serviceMedications)
                {
                    model.Medications.Remove(updateMedication.MedicationId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.Medications)
            {
                context.MedicationsServices.Add(new MedicationService
                {
                    ServiceId = service.Id,
                    MedicationId = pc.Key
                });
                context.SaveChanges();
            }
            return service;
        }
    }
}
