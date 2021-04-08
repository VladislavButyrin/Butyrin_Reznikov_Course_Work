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
    class TripStorage : ITripStorage
    {
        public List<TripViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Trips
                .Select(rec => new TripViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date
                })
               .ToList();
            }
        }
        public List<TripViewModel> GetFilteredList(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Trips
                .Where(rec => rec.Id == model.Id)
               .Select(rec => new TripViewModel
               {
                   Id = rec.Id,
                   Date = rec.Date
               })
                .ToList();
            }
        }
        public TripViewModel GetElement(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var trip = context.Trips
                .FirstOrDefault(rec => rec.Id == model.Id);
                return trip != null ?
                new TripViewModel
                {
                    Id = trip.Id,
                    Date = trip.Date
                } :
               null;
            }
        }
        public void Insert(TripBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                context.Trips.Add(CreateModel(model, new Trip()));
                context.SaveChanges();
            }
        }
        public void Update(TripBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var element = context.Trips.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(TripBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Trip element = context.Trips.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Trips.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Trip CreateModel(TripBindingModel model, Trip trip)
        {
            trip.Date = model.Date;
            return trip;
        }
    }
}
