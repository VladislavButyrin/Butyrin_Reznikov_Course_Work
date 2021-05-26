using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class GuideStorage : IGuideStorage
    {
        public void Delete(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Guide element = context.Guides.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Guides.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Лекарство не найдено");
                }
            }
        }
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                var place = context.Guides.FirstOrDefault(rec => rec.GuideName == model.GuideName || rec.Id == model.Id);
                return place != null ?
                new GuideViewModel
                {
                    Id = place.Id,
                    GuideName = place.GuideName,
                    Cost = place.Cost
                } :
               null;
            }
        }
        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                return context.Guides.Where(rec => rec.GuideName.Contains(model.GuideName))
               .ToList()
               .Select(rec => new GuideViewModel
               {
                   Id = rec.Id,
                   GuideName = rec.GuideName,
                   Cost = rec.Cost
               }).ToList();
            }
        }
        public List<GuideViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Guides
               .ToList()
               .Select(rec => new GuideViewModel
               {
                   Id = rec.Id,
                   GuideName = rec.GuideName,
                   Cost = rec.Cost
               }).ToList();
            }
        }
        public void Insert(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Guides.Add(CreateModel(model, new Guide()));
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
            using (var context = new TravelAgencyDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Guides.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
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
        private Guide CreateModel(GuideBindingModel model, Guide guide)
        {
            guide.GuideName = model.GuideName;
            return guide;
        }
    }
}