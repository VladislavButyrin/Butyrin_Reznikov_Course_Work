using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    class TouristLogic
    {
        private readonly ITouristStorage _TouristStorage;
        public TouristLogic(ITouristStorage TouristStorage)
        {
            _TouristStorage = TouristStorage;
        }

        public List<TouristViewModel> Read(TouristBindingModel model)
        {
            if (model == null)
            {
                return _TouristStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TouristViewModel> { _TouristStorage.GetElement(model) };
            }
            return _TouristStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TouristBindingModel model)
        {
            var element = _TouristStorage.GetElement(new TouristBindingModel
            {
                TouristName = model.TouristName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть документ с таким названием");
            }
            if (model.Id.HasValue)
            {
                _TouristStorage.Update(model);
            }
            else
            {
                _TouristStorage.Insert(model);
            }
        }
        public void Delete(TouristBindingModel model)

        {
            var element = _TouristStorage.GetElement(new TouristBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _TouristStorage.Delete(model);
        }
    }
}
