using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class TourLogic
    {
        private readonly ITourStorage _tourStorage;
        public TourLogic(ITourStorage tourStorage)
        {
            _tourStorage = tourStorage;
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            if (model == null)
            {
                return _tourStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TourViewModel> { _tourStorage.GetElement(model) };
            }
            return _tourStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TourBindingModel model)
        {
            var element = _tourStorage.GetElement(new TourBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            if (model.Id.HasValue)
            {
                _tourStorage.Update(model);
            }
            else
            {
                _tourStorage.Insert(model);
            }
        }
        public void Delete(TourBindingModel model)
        {
            var element = _tourStorage.GetElement(new TourBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Тур не найден");
            }
            _tourStorage.Delete(model);
        }
    }
}