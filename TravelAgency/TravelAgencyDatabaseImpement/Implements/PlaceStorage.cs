using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImpement;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImpement.Implements
{
    class PlaceStorage : IPlaceStorage
    {
        public List<PlaceViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Places
                .Select(rec => new PlaceViewModel
                {
                    Id = rec.Id,
                    PlaceName = rec.PlaceName,
                    Adress = rec.Adress,
                    GroupId = (int)rec.Group.Id,
                    Trips = rec.Trips
                            .ToDictionary(recTrips => recTrips.Id,
                            recTrips => (recTrips.TripName))
                })
               .ToList();
            }
        }
        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Places
                .Where(rec => (rec.Id == model.Id) || (rec.PlaceName == model.PlaceName))
               .Select(rec => new PlaceViewModel
               {
                   Id = rec.Id,
                   PlaceName = rec.PlaceName,
                   Adress = rec.Adress,
                   GroupId = (int)rec.Group.Id,
                   Trips = rec.Trips
                            .ToDictionary(recTrips => recTrips.Id,
                            recTrips => (recTrips.TripName))
               })
                .ToList();
            }
        }
        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var place = context.Places
                .FirstOrDefault(rec => (rec.Id == model.Id));
                return place != null ?
                new PlaceViewModel
                {
                    Id = place.Id,
                    PlaceName = place.PlaceName,
                    Adress = place.Adress,
                    GroupId = (int)place.Group.Id,
                    Trips = place.Trips
                            .ToDictionary(recTrips => recTrips.Id,
                            recTrips => (recTrips.TripName))

                } :
               null;
            }
        }
        public void Insert(PlaceBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Place(), context);
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
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Place = context.Places.FirstOrDefault(rec => rec.Id == model.Id);

                        if (Place == null)
                        {
                            throw new Exception("Место не найдено");
                        }

                        CreateModel(model, Place, context);
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
        public void Delete(PlaceBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var Tour = context.Places.FirstOrDefault(rec => rec.Id == model.Id);

                if (Tour == null)
                {
                    throw new Exception("Поездка не найдена");
                }

                context.Places.Remove(Tour);
                context.SaveChanges();
            }
        }
        private Place CreateModel(PlaceBindingModel model, Place place, TravelAgencyDatabase context)
        {
            place.PlaceName = model.PlaceName;
            if (place.Id == 0)
            {
                context.Places.Add(place);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var Trip = context.Trips
                    .Where(rec => rec.Id == model.Id.Value)
                    .ToList();

                context.Trips.RemoveRange(Trip
                    .Where(rec => !model.Trips.ContainsKey(rec.Id))
                    .ToList());
                context.SaveChanges();

                foreach (var updateTrip in Trip)
                {
                    model.Trips.Remove(updateTrip.Id);
                }
                context.SaveChanges();
            }
            foreach (var trip in model.Trips)
            {
                context.Trips.Add(new Trip
                {
                    TripName = trip.Value
                });
                context.SaveChanges();
            }
            return place;
        }
    }
}
