using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class TripStorage
    {
        private readonly DataListSingleton source;
        public TripStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<TripViewModel> GetFullList()
        {
            List<TripViewModel> result = new List<TripViewModel>();
            foreach (var trip in source.Trips)
            {
                result.Add(CreateModel(trip));
            }
            return result;
        }
        public List<TripViewModel> GetFilteredList(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<TripViewModel> result = new List<TripViewModel>();
            foreach (var trip in source.Trips)
            {
                if (trip.Id==(model.Id))
                {
                    result.Add(CreateModel(trip));
                }
            }
            return result;
        }
        public TripViewModel GetElement(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var trip in source.Trips)
            {
                if (trip.Id == model.Id)
                {
                    return CreateModel(trip);
                }
            }
            return null;
        }
        public void Insert(TripBindingModel model)
        {
            Trip tempComponent = new Trip { Id = 1 };
            foreach (var component in source.Trips)
            {
                if (component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
            }
            source.Trips.Add(CreateModel(model, tempComponent));
        }
        public void Update(TripBindingModel model)
        {

            Trip tempTrip = null;
            foreach (var component in source.Trips)
            {
                if (component.Id == model.Id)
                {
                    tempTrip = component;
                }
            }
            if (tempTrip == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempTrip);
        }
        public void Delete(TripBindingModel model)
        {
            for (int i = 0; i < source.Trips.Count; ++i)
            {
                if (source.Trips[i].Id == model.Id.Value)
                {
                    source.Trips.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Trip CreateModel(TripBindingModel model, Trip trip)
        {
            trip.Date = model.Date;
            return trip;
        }
        private TripViewModel CreateModel(Trip component)
        {
            return new TripViewModel
            {
                Id = component.Id,
                Date = component.Date
            };
        }
    }
}
