using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Models;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class GroupStorage : IGroupStorage
    {
        public List<GroupViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Groups
                .Include(rec => rec.ToursGroups)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GroupPlaces)
                .ThenInclude(rec => rec.Place)
                .Include(rec => rec.User)
                .ToList()
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Username = rec.User.Fullname,
                    DateGroup = rec.DateGroup,
                    ToursGroups=rec.ToursGroups.ToDictionary(recrec=>recrec.TourId, recrec=>recrec.Tour.TourName),
                    GroupsPlaces = rec.GroupPlaces.ToDictionary(recrec=>recrec.PlaceId,recrec=>recrec.Place.PlaceName)
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
            using (var context = new TravelAgencyDataBase())
            {
                return context.Groups
               .Include(rec => rec.ToursGroups)
               .ThenInclude(rec => rec.Tour)
               .Include(rec => rec.GroupPlaces)
               .ThenInclude(rec => rec.Place)
               .Include(rec => rec.User)
               .Where(rec => rec.Id.Equals(model.Id) || (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.UserId == model.UserId) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && model.UserId == 0) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateGroup >= model.DateFrom && rec.DateGroup <= model.DateTo && rec.UserId == model.UserId))
               .ToList()
               .Select(rec => new GroupViewModel
               {
                   Id = rec.Id,
                   UserId = rec.UserId,
                   Username = rec.User.Fullname,
                   DateGroup = rec.DateGroup,
                   ToursGroups = rec.ToursGroups.ToDictionary(recrec => recrec.TourId, recrec => recrec.Tour.TourName),
                   GroupsPlaces = rec.GroupPlaces.ToDictionary(recrec => recrec.PlaceId, recrec => recrec.Place.PlaceName)
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
            using (var context = new TravelAgencyDataBase())
            {
                var group = context.Groups
                .Include(rec => rec.ToursGroups)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GroupPlaces)
                .ThenInclude(rec => rec.Place)
                .Include(rec => rec.User)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return group != null ?
                new GroupViewModel
                {
                    Id = group.Id,
                    UserId = group.UserId,
                    Username = group.User.Fullname,
                    DateGroup = group.DateGroup,
                    ToursGroups = group.ToursGroups.ToDictionary(recrec => recrec.TourId, recrec => recrec.Tour.TourName),
                    GroupsPlaces = group.GroupPlaces.ToDictionary(recrec => recrec.PlaceId, recrec => recrec.Place.PlaceName)
                } :
               null;
            }
        }
        public void Insert(GroupBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = new Group
                        {
                            DateGroup = model.DateGroup,
                            UserId = model.UserId
                        };
                        context.Groups.Add(group);
                        context.SaveChanges();
                        CreateModel(model, group, context);
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
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Посещение не найдено");
                        }
                        element.DateGroup = model.DateGroup;
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
        public void Delete(GroupBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Group element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Groups.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Посещение не найдено");
                }
            }
        }
        private Group CreateModel(GroupBindingModel model, Group group, TravelAgencyDataBase context)
        {
            if (model.Id.HasValue)
            {
                var groupTours = context.ToursGroups.Where(rec => rec.GroupId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ToursGroups.RemoveRange(groupTours.Where(rec => !model.ToursGroups
                .ContainsValue(context.Tours.FirstOrDefault(recAN => recAN.Id == rec.TourId).TourName)));
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTour in groupTours)
                {
                    model.ToursGroups.Remove((int)updateTour.Tour.Id);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var av in model.ToursGroups)
            {
                context.ToursGroups.Add(new TourGroup
                {
                    TourId = (int)context.Tours.FirstOrDefault(rec => rec.TourName == av.Value).Id,
                    GroupId = group.Id
                });
                context.SaveChanges();
            }
            return group;
        }
    }
}