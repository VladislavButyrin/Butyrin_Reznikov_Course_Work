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
    public class GuideStorage : IGuideStorage
    {
        public void Delete(GuideBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Guide element = context.Guides.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Guides.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Лекарство не найдено");
                }
            }
        }
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var place = context.Guides.Include(rec => rec.TripsGuides).ThenInclude(rec => rec.Trip).FirstOrDefault(rec => rec.GuideName == model.GuideName || rec.Id == model.Id);
                return place != null ?
                new GuideViewModel
                {
                    Id = place.Id,
                    GuideName = place.GuideName,
                    Cost = place.Cost,
                    Trips = place.TripsGuides
                .ToDictionary(recPC => recPC.TripId, recPC =>
               (recPC.Trip?.TripName))
                } :
               null;
            }
        }

        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Guides
                .Include(rec => rec.TripsGuides)
               .ThenInclude(rec => rec.Trip)
               .Where(rec => rec.GuideName.Contains(model.GuideName))
               .ToList()
               .Select(rec => new GuideViewModel
               {
                   Id = rec.Id,
                   GuideName = rec.GuideName,
                   Cost = rec.Cost,
                   Trips = rec.TripsGuides
                .ToDictionary(recPC => recPC.TripId, recPC =>
                (recPC.Trip?.TripName))
               }).ToList();
            }
        }

        public List<GuideViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Guides
                .Include(rec => rec.TripsGuides)
               .ThenInclude(rec => rec.Trip)
               .ToList()
               .Select(rec => new GuideViewModel
               {
                   Id = rec.Id,
                   GuideName = rec.GuideName,
                   Cost = rec.Cost,
                   Trips = rec.TripsGuides
                .ToDictionary(recPC => recPC.TripId, recPC =>
                (recPC.Trip?.TripName))
               }).ToList();
            }
        }

        public void Insert(GuideBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Guide p = new Guide
                        {
                            GuideName = model.GuideName,
                            Cost = model.Cost
                        };
                        context.Guides.Add(p);
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

        public void Update(GuideBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Guides.FirstOrDefault(rec => rec.Id ==
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
        private Guide CreateModel(GuideBindingModel model, Guide guide, VetClinicDataBase context)
        {
            guide.GuideName = model.GuideName;
            guide.Cost = model.Cost;
            if (model.Id.HasValue)
            {
                var guideTrips = context.TripsGuides.Where(rec =>
               rec.GuideId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.TripsGuides.RemoveRange(guideTrips.Where(rec =>
               !model.Trips.ContainsKey(rec.TripId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTrip in guideTrips)
                {
                    model.Trips.Remove(updateTrip.TripId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.Trips)
            {
                context.TripsGuides.Add(new TripGuide
                {
                    GuideId = guide.Id,
                    TripId = pc.Key,
                });
                context.SaveChanges();
            }
            return guide;
        }
    }
}
