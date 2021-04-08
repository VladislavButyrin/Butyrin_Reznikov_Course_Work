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
    class ExcursionStorage : IExcursionStorage
    {
        public List<ExcursionViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Excursions
                .Select(rec => new ExcursionViewModel
                {
                    Id = rec.Id,
                    ExcursionName = rec.ExcursionName,
                    Date = rec.Date,
                    GuideId = rec.Guide.Id,
                    Tours = rec.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))

                })
               .ToList();
            }
        }
        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Excursions
                .Where(rec => (rec.Id == model.Id))
               .Select(rec => new ExcursionViewModel
               {
                   Id = rec.Id,
                   ExcursionName = rec.ExcursionName,
                   Date = rec.Date,
                   GuideId = rec.Guide.Id,
                   Tours = rec.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))
               })
                .ToList();
            }
        }
        public ExcursionViewModel GetElement(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var excursion = context.Excursions
                .FirstOrDefault(rec => (rec.Id == model.Id));
                return excursion != null ?
                new ExcursionViewModel
                {
                    Id = excursion.Id,
                    ExcursionName = excursion.ExcursionName,
                    Date = excursion.Date,
                    GuideId = excursion.Guide.Id,
                    Tours = excursion.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))
                } :
               null;
            }
        }
        public void Insert(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Excursion(), context);
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
        public void Update(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Excursion = context.Excursions.FirstOrDefault(rec => rec.Id == model.Id);

                        if (Excursion == null)
                        {
                            throw new Exception("Экскурсия не найдена");
                        }

                        CreateModel(model, Excursion, context);
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
        public void Delete(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var Tour = context.Excursions.FirstOrDefault(rec => rec.Id == model.Id);

                if (Tour == null)
                {
                    throw new Exception("Тур не найден");
                }

                context.Excursions.Remove(Tour);
                context.SaveChanges();
            }
        }
        private Excursion CreateModel(ExcursionBindingModel model, Excursion excursion, TravelAgencyDatabase context)
        {
            excursion.ExcursionName = model.ExcursionName;
            excursion.Place = model.Place;
            excursion.Date = model.Date;
            if (excursion.Id == 0)
            {
                context.Excursions.Add(excursion);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var Tour = context.Tours
                    .Where(rec => rec.Id == model.Id.Value)
                    .ToList();

                context.Tours.RemoveRange(Tour
                    .Where(rec => !model.Tours.ContainsKey(rec.Id))
                    .ToList());
                context.SaveChanges();

                foreach (var updateTour in Tour)
                {
                    model.Tours.Remove(updateTour.Id);
                }
                context.SaveChanges();
            }
            foreach (var tour in model.Tours)
            {
                context.Tours.Add(new Tour
                {
                    TourName = tour.Value
                });
                context.SaveChanges();
            }
            return excursion;
        }
    }
}
