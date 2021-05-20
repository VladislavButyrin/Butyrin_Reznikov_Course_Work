using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements;
using VetClinikEntitiesImplements.Modules;
using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinikEntitiesImplements.Implements
{
    public class GroupStorage : IGroupStorage
    {
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var group = context.Groups
                .Include(rec => rec.ToursGroups)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GroupPlaces)
                .ThenInclude(rec => rec.Place)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return group != null ?
                new GroupViewModel
                {
                    Id = group.Id,
                    DateGroup = group.DateGroup,
                    ToursGroups = group.ToursGroups.ToDictionary(recPC => recPC.TourId, recPC => (recPC.Tour?.TourName)),
                    PlacesGroups = group.GroupPlaces.ToDictionary(recPC => recPC.PlaceId, recPC => (recPC.Place?.PlaceName))
                } :
               null;
            }
        }

        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Groups
                .Include(rec => rec.ToursGroups)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GroupPlaces)
                .ThenInclude(rec => rec.Place)
                .ToList().Where(rec => rec.DateGroup >= model.DateFrom && rec.DateGroup <= model.DateTo)
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    DateGroup = rec.DateGroup,
                    ToursGroups = rec.ToursGroups.ToDictionary(recPC => recPC.TourId, recPC => (recPC.Tour?.TourName)),
                    PlacesGroups = rec.GroupPlaces.ToDictionary(recPC => recPC.PlaceId, recPC => (recPC.Place?.PlaceName))
                })
                .ToList();
            }
        }

        public List<GroupViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Groups
                .Include(rec => rec.ToursGroups)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GroupPlaces)
                .ThenInclude(rec => rec.Place)
                .ToList()
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    DateGroup = rec.DateGroup,
                    ToursGroups = rec.ToursGroups.ToDictionary(recPC => recPC.TourId, recPC => (recPC.Tour?.TourName)),
                    PlacesGroups = rec.GroupPlaces.ToDictionary(recPC => recPC.PlaceId, recPC => (recPC.Place?.PlaceName))
                })
                .ToList();
            }
        }

        public void Update(GroupBindingModel model)
        {
            using (var context = new VetClinicDataBase())
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
        private Group CreateModel(GroupBindingModel model, Group group, VetClinicDataBase context)
        {
            if (model.Id.HasValue)
            {
                var groupTours = context.ToursGroups.Where(rec => rec.GroupId == model.Id.Value).ToList();
                var groupPlaces = context.GroupsPlaces.Where(rec => rec.GroupId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ToursGroups.RemoveRange(groupTours.Where(rec => !model.ToursGroups.ContainsKey((int)context.Tours.FirstOrDefault(recAN => recAN.Id == rec.TourId).Id)).ToList());
                context.SaveChanges();
                // удалить услуги, которых нет в модели
                context.GroupsPlaces.RemoveRange(groupPlaces.Where(rec => !model.PlacesGroups.ContainsKey((int)context.Places.FirstOrDefault(recAN => recAN.Id == rec.PlaceId).Id)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTour in groupTours)
                {
                    model.ToursGroups.Remove((int)updateTour.Tour.Id);
                }
                foreach (var updatePlace in groupPlaces)
                {
                    model.PlacesGroups.Remove((int)updatePlace.Place.Id);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var av in model.ToursGroups)
            {
                context.ToursGroups.Add(new TourGroup
                {
                    TourId = (int)context.Tours.FirstOrDefault(rec => rec.Id == av.Key).Id,
                    GroupId = group.Id
                });
                context.SaveChanges();
            }
            foreach (var av in model.PlacesGroups)
            {
                context.GroupsPlaces.Add(new GroupPlace
                {
                    PlaceId = (int)context.Places.FirstOrDefault(rec => rec.Id == av.Key).Id,
                    GroupId = group.Id
                });
                context.SaveChanges();
            }
            return group;
        }
    }
}
