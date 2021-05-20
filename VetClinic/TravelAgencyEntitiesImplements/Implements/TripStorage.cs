using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyEntitiesImplements.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyEntitiesImplements;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyEntitiesImplements.Implements
{
    public class TripStorage : ITripStorage
    {
        public void Delete(TripBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
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
                    throw new Exception("Поездка не найдена");
                }
            }
        }

        public TripViewModel GetElement(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                var trip = context.Trips
                .FirstOrDefault(rec => rec.TripName == model.TripName ||
               rec.Id == model.Id);
                return trip != null ?
                new TripViewModel
                {
                    Id = trip.Id,
                    TripName = trip.TripName
                } :
               null;
            }
        }

        public List<TripViewModel> GetFilteredList(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                return context.Trips
                .Where(rec => rec.TripName.Contains(model.TripName))
               .Select(rec => new TripViewModel
               {
                   Id = rec.Id,
                   TripName = rec.TripName
               })
                .ToList();
            }
        }

        public List<TripViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Trips
                .Select(rec => new TripViewModel
                {
                    Id = rec.Id,
                    TripName = rec.TripName
                })
.ToList();
            }
        }

        public void Insert(TripBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                context.Trips.Add(CreateModel(model, new Trip()));
                context.SaveChanges();
            }
        }

        public void Update(TripBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                var element = context.Trips.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Поездка не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private Trip CreateModel(TripBindingModel model, Trip trip)
        {
            trip.TripName = model.TripName;
            return trip;
        }
    }
}
