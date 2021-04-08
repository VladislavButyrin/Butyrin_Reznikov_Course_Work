using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplement.Implements
{
    public class TourStorage : ITourStorage
    {
        private readonly DataListSingleton source;
        public TourStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<TourViewModel> GetFullList()
        {
            List<TourViewModel> result = new List<TourViewModel>();
            foreach (var tour in source.Tours)
            {
                result.Add(CreateModel(tour));
            }
            return result;
        }
        public List<TourViewModel> GetFilteredList(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<TourViewModel> result = new List<TourViewModel>();
            foreach (var tour in source.Tours)
            {
                if (tour.TourName.Contains(model.TourName))
                {
                    result.Add(CreateModel(tour));
                }
            }
            return result;
        }
        public TourViewModel GetElement(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var tour in source.Tours)
            {
                if (tour.Id == model.Id || tour.TourName ==
               model.TourName)
                {
                    return CreateModel(tour);
                }
            }
            return null;
        }
        public void Insert(TourBindingModel model)
        {
            Tour temptour = new Tour { Id = 1 };
            foreach (var tour in source.Tours)
            {
                if (tour.Id >= temptour.Id)
                {
                    temptour.Id = tour.Id + 1;
                }
            }
            source.Tours.Add(CreateModel(model, temptour));
        }
        public void Update(TourBindingModel model)
        {

            Tour tempTour = null;
            foreach (var tour in source.Tours)
            {
                if (tour.Id == model.Id)
                {
                    tempTour = tour;
                }
            }
            if (tempTour == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempTour);
        }
        public void Delete(TourBindingModel model)
        {
            for (int i = 0; i < source.Tours.Count; ++i)
            {
                if (source.Tours[i].Id == model.Id.Value)
                {
                    source.Tours.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Tour CreateModel(TourBindingModel model, Tour tour)
        {
            tour.TourName = model.TourName;
            tour.PlaceOfDeparture = model.PlaceOfDeparture;
            tour.PlaceOfResidence = model.PlaceOfResidence;
            tour.DateOfDeparture = model.DateOfDeparture;
            return tour;
        }
        private TourViewModel CreateModel(Tour tour)
        {
            return new TourViewModel
            {
                Id = tour.Id,
                TourName = tour.TourName,
                DateOfDeparture = tour.DateOfDeparture,
                PlaceOfDeparture = tour.PlaceOfDeparture,
                PlaceOfResidence = tour.PlaceOfResidence
            };
        }
    }
}
