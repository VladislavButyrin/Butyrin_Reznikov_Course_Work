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
    class GuideStorage : IGuideStorage
    {
        public List<GuideViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Guides
                .Select(rec => new GuideViewModel
                {
                    Id = rec.Id,
                    GuideName = rec.GuideName,
                    Passport = rec.Passport,
                    TripId = rec.Trip.Id
                })
               .ToList();
            }
        }
        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Guides
                .Where(rec => (rec.Id == model.Id))
               .Select(rec => new GuideViewModel
               {
                   Id = rec.Id,
                   GuideName = rec.GuideName,
                   Passport = rec.Passport,
                   TripId = rec.Trip.Id
               })
                .ToList();
            }
        }
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var guide = context.Guides
                .FirstOrDefault(rec => (rec.Id == model.Id));
                return guide != null ?
                new GuideViewModel
                {
                    Id = guide.Id,
                    GuideName = guide.GuideName,
                    Passport = guide.Passport,
                    TripId = guide.Trip.Id
                } :
               null;
            }
        }
        public void Insert(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Guide(), context);
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
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Guide = context.Guides.FirstOrDefault(rec => rec.Id == model.Id);

                        if (Guide == null)
                        {
                            throw new Exception("Гид не найден");
                        }

                        CreateModel(model, Guide, context);
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
        public void Delete(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var Tour = context.Guides.FirstOrDefault(rec => rec.Id == model.Id);

                if (Tour == null)
                {
                    throw new Exception("Поездка не найдена");
                }

                context.Guides.Remove(Tour);
                context.SaveChanges();
            }
        }
        private Guide CreateModel(GuideBindingModel model, Guide guide, TravelAgencyDatabase context)
        {
            guide.GuideName = model.GuideName;
            guide.Passport = model.Passport;
            guide.Trip = context.Trips.FirstOrDefault(rec => rec.Id == model.TripId);
            return guide;
        }
    }
}
