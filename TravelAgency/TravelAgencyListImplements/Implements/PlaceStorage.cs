using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class PlaceStorage
    {
        private readonly DataListSingleton source;
        public PlaceStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<PlaceViewModel> GetFullList()
        {
            List<PlaceViewModel> result = new List<PlaceViewModel>();
            foreach (var place in source.Places)
            {
                result.Add(CreateModel(place));
            }
            return result;
        }
        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<PlaceViewModel> result = new List<PlaceViewModel>();
            foreach (var place in source.Places)
            {
                if (place.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(place));
                }
            }
            return result;
        }
        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var place in source.Places)
            {
                if (place.Id == model.Id || place.Name ==
               model.Name)
                {
                    return CreateModel(place);
                }
            }
            return null;
        }
        public void Insert(PlaceBindingModel model)
        {
            Place tempPlace = new Place { Id = 1 };
            foreach (var Place in source.Places)
            {
                if (Place.Id >= tempPlace.Id)
                {
                    tempPlace.Id = Place.Id + 1;
                }
            }
            source.Places.Add(CreateModel(model, tempPlace));
        }
        public void Update(PlaceBindingModel model)
        {

            Place tempPlace = null;
            foreach (var Place in source.Places)
            {
                if (Place.Id == model.Id)
                {
                    tempPlace = Place;
                }
            }
            if (tempPlace == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempPlace);
        }
        public void Delete(PlaceBindingModel model)
        {
            for (int i = 0; i < source.Places.Count; ++i)
            {
                if (source.Places[i].Id == model.Id)
                {
                    source.Places.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Place CreateModel(PlaceBindingModel model, Place place)
        {
            place.Name = model.Name;
            place.GroupId = model.GroupId;
            place.Adress = model.Adress;
            // удаляем убранные
            foreach (var key in place.Trips.Keys.ToList())
            {
                if (!model.Trips.ContainsKey(key))
                {
                    place.Trips.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var tour in model.Trips)
            {
                if (place.Trips.ContainsKey(tour.Key))
                {
                    place.Trips[tour.Key] =
                    model.Trips[tour.Key];
                }
                else
                {
                    place.Trips.Add(tour.Key,
                    model.Trips[tour.Key]);
                }
            }
            return place;
        }
        private PlaceViewModel CreateModel(Place place)
        {
            Dictionary<int, (string, int)> trip = new
        Dictionary<int, (string, int)>();
            foreach (var db in place.Trips)
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
                trip.Add(db.Key, (tourName, db.Value));
            }
            return new PlaceViewModel
            {
                Id = place.Id,
                Name = place.Name,
                Adress = place.Adress,
                GroupId = place.GroupId,
                Trips = place.Trips
            };
        }
    }
}
