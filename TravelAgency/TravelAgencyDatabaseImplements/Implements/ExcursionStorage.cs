using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplements.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class ExcursionStorage : IExcursionStorage
    {
        public List<ExcursionViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Excursions
                .Include(rec => rec.ToursExcursions)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GuidesExcursions)
                .ThenInclude(rec => rec.Guide)
                .Include(rec => rec.User)
                .ToList()
                .Select(rec => new ExcursionViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Username = rec.User.Fullname,
                    Sum = rec.Sum,
                    DatePayment = rec.DatePayment,
                    GuidesExcursions = rec.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).GuideName, 
                    recTC => (recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                    ToursExcursions = rec.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
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
            using (var context = new TravelAgencyDataBase())
            {
                return context.Excursions
               .Include(rec => rec.ToursExcursions)
               .ThenInclude(rec => rec.Tour)
               .Include(rec => rec.GuidesExcursions)
               .ThenInclude(rec => rec.Guide)
               .Include(rec => rec.User)
               .Where(rec => rec.Id.Equals(model.Id) || (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.UserId == model.UserId) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && model.UserId == 0) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DatePayment >= model.DateFrom && rec.DatePayment <= model.DateTo && rec.UserId == model.UserId))
               .ToList()
               .Select(rec => new ExcursionViewModel
               {
                   Id = rec.Id,
                   UserId = rec.UserId,
                   Username = rec.User.Fullname,
                   Sum = rec.Sum,
                   DatePayment = rec.DatePayment,
                   GuidesExcursions = rec.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).GuideName,
                    recTC => (recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                   ToursExcursions = rec.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
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
            using (var context = new TravelAgencyDataBase())
            {
                var excursion = context.Excursions
                .Include(rec => rec.ToursExcursions)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GuidesExcursions)
                .ThenInclude(rec => rec.Guide)
                .Include(rec => rec.User)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return excursion != null ?
                new ExcursionViewModel
                {
                    Id = excursion.Id,
                    UserId = excursion.UserId,
                    Username = excursion.User.Fullname,
                    Sum = excursion.Sum,
                    DatePayment = excursion.DatePayment,
                    GuidesExcursions = excursion.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).GuideName,
                    recTC => (recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                    ToursExcursions = excursion.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
                } :
               null;
            }
        }
        public void Insert(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Excursion excursion = new Excursion
                        {
                            Sum = model.Sum,
                            DatePayment = model.DatePayment,
                            UserId = model.UserId
                        };
                        context.Excursions.Add(excursion);
                        context.SaveChanges();
                        CreateModel(model, excursion, context);
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
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Excursions.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Покупка не найдена");
                        }
                        element.Sum = model.Sum;
                        element.UserId = model.UserId;
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
        public void Delete(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Excursion element = context.Excursions.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Excursions.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Покупка не найдена");
                }
            }
        }
        private Excursion CreateModel(ExcursionBindingModel model, Excursion excursion, TravelAgencyDataBase context)
        {
            if (model.Id.HasValue)
            {
                // Лекарства в покупке
                var guidesExcursions = context.GuidesExcursions.Where(rec => rec.ExcursionId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.GuidesExcursions.RemoveRange(guidesExcursions.Where(rec => !model.GuidesExcursions.ContainsKey(context.Guides.FirstOrDefault(recMN => recMN.Id == rec.GuideId).GuideName)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateGuide in guidesExcursions)
                {
                    updateGuide.Count = model.GuidesExcursions[updateGuide.Guide.GuideName].Item1;
                    model.GuidesExcursions.Remove(updateGuide.Guide.GuideName);
                }
                context.SaveChanges();

                // Животные в покупке
                var toursExcursions = context.ToursExcursions.Where(rec => rec.ExcursionId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ToursExcursions.RemoveRange(toursExcursions.Where(rec => !model.ToursExcursions.Contains(context.Tours.FirstOrDefault(recAN => recAN.Id == rec.TourId).TourName)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTour in toursExcursions)
                {
                    model.ToursExcursions.Remove(updateTour.Tour.TourName);
                }
                context.SaveChanges();
            }

            // добавили новые записи лекарств
            foreach (var mp in model.GuidesExcursions)
            {
                context.GuidesExcursions.Add(new GuideExcursion
                {
                    GuideId = context.Guides.FirstOrDefault(rec => rec.GuideName == mp.Key).Id,
                    ExcursionId = excursion.Id,
                    Count = mp.Value.Item1
                });
                context.SaveChanges();
            }

            // добавили новые записи животных
            foreach (var ap in model.ToursExcursions)
            {
                context.ToursExcursions.Add(new TourExcursion
                {
                    TourId = (int)context.Tours.FirstOrDefault(rec => rec.TourName == ap).Id,
                    ExcursionId = excursion.Id
                });
                context.SaveChanges();
            }
            return excursion;
        }
    }
}