using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class TouristStorage: ITouristStorage
    {
        private readonly DataListSingleton source;
        public TouristStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<TouristViewModel> GetFullList()
        {
            List<TouristViewModel> result = new List<TouristViewModel>();
            foreach (var tourist in source.Tourists)
            {
                result.Add(CreateModel(tourist));
            }
            return result;
        }
        public List<TouristViewModel> GetFilteredList(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<TouristViewModel> result = new List<TouristViewModel>();
            foreach (var Tourist in source.Tourists)
            {
                if (Tourist.TouristName.Contains(model.TouristName))
                {
                    result.Add(CreateModel(Tourist));
                }
            }
            return result;
        }
        public TouristViewModel GetElement(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var tourist in source.Tourists)
            {
                if (tourist.Id == model.Id || tourist.TouristName ==
               model.TouristName)
                {
                    return CreateModel(tourist);
                }
            }
            return null;
        }
        public void Insert(TouristBindingModel model)
        {
            Tourist tempTourist = new Tourist { Id = 1 };
            foreach (var Tourist in source.Tourists)
            {
                if (Tourist.Id >= tempTourist.Id)
                {
                    tempTourist.Id = Tourist.Id + 1;
                }
            }
            source.Tourists.Add(CreateModel(model, tempTourist));
        }
        public void Update(TouristBindingModel model)
        {

            Tourist tempTourist = null;
            foreach (var Tourist in source.Tourists)
            {
                if (Tourist.Id == model.Id)
                {
                    tempTourist = Tourist;
                }
            }
            if (tempTourist == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempTourist);
        }
        public void Delete(TouristBindingModel model)
        {
            for (int i = 0; i < source.Tourists.Count; ++i)
            {
                if (source.Tourists[i].Id == model.Id.Value)
                {
                    source.Tourists.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Tourist CreateModel(TouristBindingModel model, Tourist tourist)
        {
            tourist.TouristName = model.TouristName;
            tourist.Email = model.Email;
            return tourist;
        }
        private TouristViewModel CreateModel(Tourist tourist)
        {
            return new TouristViewModel
            {
                Id = tourist.Id,
                TouristName = tourist.TouristName,
                Email=tourist.Email
            };
        }
    }
}
