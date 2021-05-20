using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class TripBusinessLogic
    {
        private readonly ITripStorage _tripStorage;
        public TripBusinessLogic(ITripStorage tripStorage)
        {
            _tripStorage = tripStorage;
        }
        public List<TripViewModel> Read(TripBindingModel model)
        {
            if (model == null)
            {
                return _tripStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TripViewModel> { _tripStorage.GetElement(model) };
            }
            return _tripStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TripBindingModel model)
        {
            var element = _tripStorage.GetElement(new TripBindingModel
            {
                TripName = model.TripName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть медикамент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _tripStorage.Update(model);
            }
            else
            {
                _tripStorage.Insert(model);
            }
        }
        public void Delete(TripBindingModel model)
        {
            var element = _tripStorage.GetElement(new TripBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Медикамент не найден");
            }
            _tripStorage.Delete(model);
        }
    }
}