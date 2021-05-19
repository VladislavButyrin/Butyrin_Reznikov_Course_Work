using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements;
using VetClinikEntitiesImplements.Modules;
using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinikEntitiesImplements.Implements
{
    public class VisitStorage : IVisitStorage
    {
        public VisitViewModel GetElement(VisitBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var visit = context.Visits
                .Include(rec => rec.AnimalsVisits)
                .ThenInclude(rec => rec.Animal)
                .Include(rec => rec.VisitServices)
                .ThenInclude(rec => rec.Service)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return visit != null ?
                new VisitViewModel
                {
                    Id = visit.Id,
                    DateVisit = visit.DateVisit,
                    AnimalsVisits = visit.AnimalsVisits.ToDictionary(recPC => recPC.AnimalId, recPC => (recPC.Animal?.AnimalName)),
                    ServicesVisits = visit.VisitServices.ToDictionary(recPC => recPC.ServiceId, recPC => (recPC.Service?.ServiceName))
                } :
               null;
            }
        }

        public List<VisitViewModel> GetFilteredList(VisitBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Visits
                .Include(rec => rec.AnimalsVisits)
                .ThenInclude(rec => rec.Animal)
                .Include(rec => rec.VisitServices)
                .ThenInclude(rec => rec.Service)
                .ToList().Where(rec => rec.DateVisit >= model.DateFrom && rec.DateVisit <= model.DateTo)
                .Select(rec => new VisitViewModel
                {
                    Id = rec.Id,
                    DateVisit = rec.DateVisit,
                    AnimalsVisits = rec.AnimalsVisits.ToDictionary(recPC => recPC.AnimalId, recPC => (recPC.Animal?.AnimalName)),
                    ServicesVisits = rec.VisitServices.ToDictionary(recPC => recPC.ServiceId, recPC => (recPC.Service?.ServiceName))
                })
                .ToList();
            }
        }

        public List<VisitViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Visits
                .Include(rec => rec.AnimalsVisits)
                .ThenInclude(rec => rec.Animal)
                .Include(rec => rec.VisitServices)
                .ThenInclude(rec => rec.Service)
                .ToList()
                .Select(rec => new VisitViewModel
                {
                    Id = rec.Id,
                    DateVisit = rec.DateVisit,
                    AnimalsVisits = rec.AnimalsVisits.ToDictionary(recPC => recPC.AnimalId, recPC => (recPC.Animal?.AnimalName)),
                    ServicesVisits = rec.VisitServices.ToDictionary(recPC => recPC.ServiceId, recPC => (recPC.Service?.ServiceName))
                })
                .ToList();
            }
        }

        public void Update(VisitBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Visits.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Посещение не найдено");
                        }
                        element.DateVisit = model.DateVisit;
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
        private Visit CreateModel(VisitBindingModel model, Visit visit, VetClinicDataBase context)
        {
            if (model.Id.HasValue)
            {
                var visitAnimals = context.AnimalsVisits.Where(rec => rec.VisitId == model.Id.Value).ToList();
                var visitServices = context.VisitsServices.Where(rec => rec.VisitId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.AnimalsVisits.RemoveRange(visitAnimals.Where(rec => !model.AnimalsVisits.ContainsKey((int)context.Animals.FirstOrDefault(recAN => recAN.Id == rec.AnimalId).Id)).ToList());
                context.SaveChanges();
                // удалить услуги, которых нет в модели
                context.VisitsServices.RemoveRange(visitServices.Where(rec => !model.ServicesVisits.ContainsKey((int)context.Services.FirstOrDefault(recAN => recAN.Id == rec.ServiceId).Id)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateAnimal in visitAnimals)
                {
                    model.AnimalsVisits.Remove((int)updateAnimal.Animal.Id);
                }
                foreach (var updateService in visitServices)
                {
                    model.ServicesVisits.Remove((int)updateService.Service.Id);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var av in model.AnimalsVisits)
            {
                context.AnimalsVisits.Add(new AnimalVisit
                {
                    AnimalId = (int)context.Animals.FirstOrDefault(rec => rec.Id == av.Key).Id,
                    VisitId = visit.Id
                });
                context.SaveChanges();
            }
            foreach (var av in model.ServicesVisits)
            {
                context.VisitsServices.Add(new VisitService
                {
                    ServiceId = (int)context.Services.FirstOrDefault(rec => rec.Id == av.Key).Id,
                    VisitId = visit.Id
                });
                context.SaveChanges();
            }
            return visit;
        }
    }
}
