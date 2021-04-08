using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyDatabaseImpement;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class TourStorage : ITourStorage
    {
        public List<TourViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours
                .Select(rec => new TourViewModel
                {
                    Id = rec.Id,
                    TourName = rec.TourName
                })
               .ToList();
            }
        }
        public List<TourViewModel> GetFilteredList(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours
                .Where(rec => rec.TourName.Contains(model.Name))
               .Select(rec => new TourViewModel
               {
                   Id = rec.Id,
                   TourName = rec.TourName
               })
                .ToList();
            }
        }
        public TourViewModel GetElement(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var tour = context.Tours
                .FirstOrDefault(rec => rec.TourName == model.Name ||
               rec.Id == model.Id);
                return tour != null ?
                new TourViewModel
                {
                    Id = tour.Id,
                    TourName = tour.TourName
                } :
               null;
            }
        }
        public void Insert(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                context.Tours.Add(CreateModel(model, new Tour()));
                context.SaveChanges();
            }
        }
        public void Update(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var element = context.Tours.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Tour element = context.Tours.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Tours.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Tour CreateModel(TourBindingModel model, Tour tour)
        {
            tour.TourName = model.Name;
            return tour;
        }
    }
}
