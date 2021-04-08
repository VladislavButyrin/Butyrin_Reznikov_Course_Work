using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ExcursionLogic
    {
        private readonly IExcursionStorage _ExcursionStorage;
        public ExcursionLogic(IExcursionStorage ExcursionStorage)
        {
            _ExcursionStorage = ExcursionStorage;
        }

        public List<ExcursionViewModel> Read(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return _ExcursionStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ExcursionViewModel> { _ExcursionStorage.GetElement(model) };
            }
            return _ExcursionStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ExcursionBindingModel model)
        {
            var element = _ExcursionStorage.GetElement(new ExcursionBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть документ с таким названием");
            }
            if (model.Id.HasValue)
            {
                _ExcursionStorage.Update(model);
            }
            else
            {
                _ExcursionStorage.Insert(model);
            }
        }
        public void Delete(ExcursionBindingModel model)

        {
            var element = _ExcursionStorage.GetElement(new ExcursionBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _ExcursionStorage.Delete(model);
        }
    }
}
