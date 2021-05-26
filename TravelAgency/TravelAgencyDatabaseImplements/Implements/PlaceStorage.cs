using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyDatabaseImplements;
using TravelAgencyBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class PlaceStorage : IPlaceStorage
    {
        public void Delete(PlaceBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Place element = context.Places.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Places.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Услуга не найдена");
                }
            }
        }
        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                var place = context.Places.FirstOrDefault(rec => rec.PlaceName == model.PlaceName || rec.Id == model.Id);
                return place != null ?
                new PlaceViewModel
                {
                    Id = place.Id,
                    PlaceName = place.PlaceName
                } :
               null;
            }
        }
        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                return context.Places
               .Where(rec => rec.PlaceName.Contains(model.PlaceName))
               .ToList()
               .Select(rec => new PlaceViewModel
               {
                   Id = rec.Id,
                   PlaceName = rec.PlaceName
               }).ToList();
            }
        }
        public List<PlaceViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Places
               .ToList()
               .Select(rec => new PlaceViewModel
               {
                   Id = rec.Id,
                   PlaceName = rec.PlaceName
               }).ToList();
            }
        }
        public void Insert(PlaceBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Places.Add(CreateModel(model, new Place()));
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
        public void Update(PlaceBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Places.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Услуга не найдена");
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
        private Place CreateModel(PlaceBindingModel model, Place place)
        {
            place.PlaceName = model.PlaceName;
            return place;
        }
    }
}