using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class TourLogic
    {
        private readonly ITourStorage _TourStorage;
        public TourLogic(ITourStorage TourStorage)
        {
            _TourStorage = TourStorage;
        }

        public List<TourViewModel> Read(TourBindingModel model)
        {
            if (model == null)
            {
                return _TourStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TourViewModel> { _TourStorage.GetElement(model) };
            }
            return _TourStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TourBindingModel model)
        {
            var element = _TourStorage.GetElement(new TourBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть документ с таким названием");
            }
            if (model.Id.HasValue)
            {
                _TourStorage.Update(model);
            }
            else
            {
                _TourStorage.Insert(model);
            }
        }
        public void Delete(TourBindingModel model)

        {
            var element = _TourStorage.GetElement(new TourBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _TourStorage.Delete(model);
        }
    }
}
