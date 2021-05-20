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
    public class PlaceStorage : IPlaceStorage
    {
        public void Delete(PlaceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Place element = context.Places.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Places.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Услуга не найдена");
                }
            }
        }

        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var place = context.Places.Include(rec => rec.TripsPlaces).ThenInclude(rec => rec.Trip).FirstOrDefault(rec => rec.PlaceName == model.PlaceName || rec.Id == model.Id);
                return place != null ?
                new PlaceViewModel
                {
                    Id = place.Id,
                    PlaceName = place.PlaceName,
                    Cost = place.Cost,
                    FIO = context.Organizators.FirstOrDefault(doc => doc.Id == place.OrganizatorId).FIO,
                    Trips = place.TripsPlaces
                .ToDictionary(recPC => recPC.TripId, recPC =>
               (recPC.Trip?.TripName))
                } :
               null;
            }
        }

        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Places
                .Include(rec => rec.TripsPlaces)
               .ThenInclude(rec => rec.Trip)
               .Where(rec => rec.PlaceName.Contains(model.PlaceName) || model.OrganizatorId == rec.OrganizatorId)
               .ToList()
               .Select(rec => new PlaceViewModel
               {
                   Id = rec.Id,
                   PlaceName = rec.PlaceName,
                   Cost = rec.Cost,
                   FIO = context.Organizators.FirstOrDefault(doc => doc.Id == rec.OrganizatorId).FIO,
                   Trips = rec.TripsPlaces
                .ToDictionary(recPC => recPC.TripId, recPC =>
                (recPC.Trip?.TripName))
               }).ToList();
            }
        }

        public List<PlaceViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Places
                .Include(rec => rec.TripsPlaces)
               .ThenInclude(rec => rec.Trip)
               .ToList()
               .Select(rec => new PlaceViewModel
               {
                   Id = rec.Id,
                   PlaceName = rec.PlaceName,
                   Cost = rec.Cost,
                   FIO = context.Organizators.FirstOrDefault(doc => doc.Id == rec.OrganizatorId).FIO,
                   Trips = rec.TripsPlaces
                .ToDictionary(recPC => recPC.TripId, recPC =>
                (recPC.Trip?.TripName))
               }).ToList();
            }
        }

        public void Insert(PlaceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Place p = new Place
                        {
                            PlaceName = model.PlaceName,
                            Cost = model.Cost,
                            OrganizatorId = (int)model.OrganizatorId
                        };
                        context.Places.Add(p);
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

        public void Update(PlaceBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Places.FirstOrDefault(rec => rec.Id == model.Id);
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

        private Place CreateModel(PlaceBindingModel model, Place place, VetClinicDataBase context)
        {
            place.PlaceName = model.PlaceName;
            place.OrganizatorId = (int)model.OrganizatorId;
            place.Cost = model.Cost;
            if (model.Id.HasValue)
            {
                var placeTrips = context.TripsPlaces.Where(rec =>
               rec.PlaceId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.TripsPlaces.RemoveRange(placeTrips.Where(rec =>
               !model.Trips.ContainsKey(rec.TripId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTrip in placeTrips)
                {
                    model.Trips.Remove(updateTrip.TripId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.Trips)
            {
                context.TripsPlaces.Add(new TripPlace
                {
                    PlaceId = place.Id,
                    TripId = pc.Key
                });
                context.SaveChanges();
            }
            return place;
        }
    }
}
