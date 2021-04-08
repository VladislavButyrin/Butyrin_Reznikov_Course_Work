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
    class GroupStorage : IGroupStorage
    {
        public List<GroupViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Groups
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    PeopleQuantity = rec.PeopleQuantity,
                    Tours = rec.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))

                })
               .ToList();
            }
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Groups
                .Where(rec => (rec.Id == model.Id))
               .Select(rec => new GroupViewModel
               {
                   Id = rec.Id,
                   PeopleQuantity = rec.PeopleQuantity,
                   Tours = rec.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))
               })
                .ToList();
            }
        }
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var group = context.Groups
                .FirstOrDefault(rec => (rec.Id == model.Id));
                return group != null ?
                new GroupViewModel
                {
                    Id = group.Id,
                    PeopleQuantity = group.PeopleQuantity,
                    Tours = group.Tours
                            .ToDictionary(recTours => recTours.Id,
                            recTours => (recTours.TourName))
                } :
               null;
            }
        }
        public void Insert(GroupBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Group(), context);
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
        public void Update(GroupBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Group = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);

                        if (Group == null)
                        {
                            throw new Exception("Группа не найдена");
                        }

                        CreateModel(model, Group, context);
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
        public void Delete(GroupBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var Tour = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);

                if (Tour == null)
                {
                    throw new Exception("Тур не найден");
                }

                context.Groups.Remove(Tour);
                context.SaveChanges();
            }
        }
        private Group CreateModel(GroupBindingModel model, Group group, TravelAgencyDatabase context)
        {
            group.PeopleQuantity = model.PeopleQuantity;
            if (group.Id == 0)
            {
                context.Groups.Add(group);
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
                }) ;
                context.SaveChanges();
            }
            return group;
        }
    }
}
