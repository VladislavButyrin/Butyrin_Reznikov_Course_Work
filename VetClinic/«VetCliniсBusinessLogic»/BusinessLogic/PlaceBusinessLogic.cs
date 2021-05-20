using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class PlaceBusinessLogic
    {
        private readonly IPlaceStorage _placeStorage;
        public PlaceBusinessLogic(IPlaceStorage placeStorage)
        {
            _placeStorage = placeStorage;
        }
        public List<PlaceViewModel> Read(PlaceBindingModel model)
        {
            if (model == null)
            {
                return _placeStorage.GetFullList();
            }
            if (model.OrganizatorId.HasValue)
            {
                return _placeStorage.GetFilteredList(model);
            }
            if (model.Id.HasValue)
            {
                return new List<PlaceViewModel> { _placeStorage.GetElement(model) };
            }
            return _placeStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(PlaceBindingModel model)
        {
            var element = _placeStorage.GetElement(new PlaceBindingModel
            {
                PlaceName = model.PlaceName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть лекарство с таким названием");
            }
            if (model.Id.HasValue)
            {
                _placeStorage.Update(model);
            }
            else
            {
                _placeStorage.Insert(model);
            }
        }
        public void Delete(PlaceBindingModel model)
        {
            var element = _placeStorage.GetElement(new PlaceBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _placeStorage.Delete(model);
        }
    }
}
