using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImpement.Implements
{
    public class TouristStorage: ITouristStorage
    {
        public List<TouristViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tourists
                .Select(rec => new TouristViewModel
                {
                    Id = rec.Id,
                    TouristName = rec.TouristName
                })
               .ToList();
            }
        }
        public List<TouristViewModel> GetFilteredList(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tourists
                .Where(rec => rec.TouristName.Contains(model.TouristName))
               .Select(rec => new TouristViewModel
               {
                   Id = rec.Id,
                   TouristName = rec.TouristName
               })
                .ToList();
            }
        }
        public TouristViewModel GetElement(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var tourist = context.Tourists
                .FirstOrDefault(rec => rec.TouristName == model.TouristName ||
               rec.Id == model.Id);
                return tourist != null ?
                new TouristViewModel
                {
                    Id = tourist.Id,
                    TouristName = tourist.TouristName
                } :
               null;
            }
        }
        public void Insert(TouristBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                context.Tourists.Add(CreateModel(model, new Tourist()));
                context.SaveChanges();
            }
        }
        public void Update(TouristBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var element = context.Tourists.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(TouristBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Tourist element = context.Tourists.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Tourists.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Tourist CreateModel(TouristBindingModel model, Tourist tourist)
        {
            tourist.TouristName = model.TouristName;
            return tourist;
        }
    }
}
