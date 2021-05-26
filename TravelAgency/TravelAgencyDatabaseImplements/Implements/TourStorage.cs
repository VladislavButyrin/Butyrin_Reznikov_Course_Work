using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Models;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class TourStorage : ITourStorage
    {
        public List<TourViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
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
            using (var context = new TravelAgencyDataBase())
            {
                return context.Tours
               .Where(rec => rec.TourName.Contains(model.TourName))
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
            using (var context = new TravelAgencyDataBase())
            {
                var tour = context.Tours.FirstOrDefault(rec => rec.TourName == model.TourName || rec.Id == model.Id);
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
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Tours.Add(CreateModel(model, new Tour()));
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
        public void Update(TourBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Животное не найдено");
                        }
                        CreateModel(model, element);
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
        public void Delete(TourBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Tour element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Tours.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Животное не найдено");
                }
            }
        }
        private Tour CreateModel(TourBindingModel model, Tour tour)
        {
            tour.TourName = model.TourName;
            return tour;
        }
    }
}