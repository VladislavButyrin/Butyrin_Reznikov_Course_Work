using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class TripLogic
    {
        private readonly ITripStorage _TripStorage;
        public TripLogic(ITripStorage TripStorage)
        {
            _TripStorage = TripStorage;
        }

        public List<TripViewModel> Read(TripBindingModel model)
        {
            if (model == null)
            {
                return _TripStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TripViewModel> { _TripStorage.GetElement(model) };
            }
            return _TripStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TripBindingModel model)
        {

            if (model.Id.HasValue)
            {
                _TripStorage.Update(model);
            }
            else
            {
                _TripStorage.Insert(model);
            }

        }
        public void Delete(TripBindingModel model)

        {
            var element = _TripStorage.GetElement(new TripBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _TripStorage.Delete(model);
        }

    }
}
