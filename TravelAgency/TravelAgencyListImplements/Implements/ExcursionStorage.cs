using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class ExcursionStorage
    {
        private readonly DataListSingleton source;
        public ExcursionStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ExcursionViewModel> GetFullList()
        {
            List<ExcursionViewModel> result = new List<ExcursionViewModel>();
            foreach (var excursion in source.Excursions)
            {
                result.Add(CreateModel(excursion));
            }
            return result;
        }
        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<ExcursionViewModel> result = new List<ExcursionViewModel>();
            foreach (var excursion in source.Excursions)
            {
                if (excursion.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(excursion));
                }
            }
            return result;
        }
        public ExcursionViewModel GetElement(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var excursion in source.Excursions)
            {
                if (excursion.Id == model.Id || excursion.Name ==
               model.Name)
                {
                    return CreateModel(excursion);
                }
            }
            return null;
        }
        public void Insert(ExcursionBindingModel model)
        {
            Excursion tempExcursion = new Excursion { Id = 1 };
            foreach (var excursion in source.Excursions)
            {
                if (excursion.Id >= tempExcursion.Id)
                {
                    tempExcursion.Id = excursion.Id + 1;
                }
            }
            source.Excursions.Add(CreateModel(model, tempExcursion));
        }
        public void Update(ExcursionBindingModel model)
        {

            Excursion tempExcursion = null;
            foreach (var excursion in source.Excursions)
            {
                if (excursion.Id == model.Id)
                {
                    tempExcursion = excursion;
                }
            }
            if (tempExcursion == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempExcursion);
        }
        public void Delete(ExcursionBindingModel model)
        {
            for (int i = 0; i < source.Excursions.Count; ++i)
            {
                if (source.Excursions[i].Id == model.Id.Value)
                {
                    source.Excursions.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Excursion CreateModel(ExcursionBindingModel model, Excursion excursion)
        {
            excursion.Name = model.Name;
            excursion.Date = model.Date;
            excursion.GuideId = model.GuideId;
            // удаляем убранные
            foreach (var key in excursion.Tours.Keys.ToList())
            {
                if (!model.Tours.ContainsKey(key))
                {
                    excursion.Tours.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var tour in model.Tours)
            {
                if (excursion.Tours.ContainsKey(tour.Key))
                {
                    excursion.Tours[tour.Key] =
                    model.Tours[tour.Key].Item2;
                }
                else
                {
                    excursion.Tours.Add(tour.Key,
                    model.Tours[tour.Key].Item2);
                }
            }
            return excursion;
        }
        private ExcursionViewModel CreateModel(Excursion excursion)
        {
            Dictionary<int, (string, int)> tours = new
        Dictionary<int, (string, int)>();
            foreach (var db in excursion.Tours)
            {
                string tourName = string.Empty;
                foreach (var tour in source.Tours)
                {
                    if (db.Key == tour.Id)
                    {
                        tourName = tour.Name;
                        break;
                    }
                }
                tours.Add(db.Key, (tourName, db.Value));
            }
            return new ExcursionViewModel
            {
                Id = excursion.Id,
                Name = excursion.Name,
                Date = excursion.Date,
                GuideId = excursion.GuideId,
                Tours = tours
            };
        }
    }
}
