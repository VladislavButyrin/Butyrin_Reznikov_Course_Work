using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ExcursionLogic
    {
        private readonly IExcursionStorage _excursionStorage;
        public ExcursionLogic(IExcursionStorage excursionStorage)
        {
            _excursionStorage = excursionStorage;
        }
        public List<ExcursionViewModel> Read(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return _excursionStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ExcursionViewModel> { _excursionStorage.GetElement(model) };
            }
            return _excursionStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ExcursionBindingModel model)
        {
            var element = _excursionStorage.GetElement(new ExcursionBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть покупка с таким ID");
            }
            if (model.Id.HasValue)
            {
                _excursionStorage.Update(model);
            }
            else
            {
                _excursionStorage.Insert(model);
            }
        }
        public void Delete(ExcursionBindingModel model)
        {
            var element = _excursionStorage.GetElement(new ExcursionBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Покупка не найдена");
            }
            _excursionStorage.Delete(model);
        }
        public void GuideBinding(AddingGuideBindingModel model)
        {
            ExcursionViewModel excursion = _excursionStorage.GetElement(new ExcursionBindingModel
            {
                Id = model.ExcursionId
            });
            if (excursion.GuidesExcursions.Count == 0)
            {
                excursion.GuidesExcursions = new Dictionary<string, (int, int)>
                {
                    { model.GuideName, (model.Count, model.Cost) }
                };
            }
            else
            {
                if(!excursion.GuidesExcursions.ContainsKey(model.GuideName))
                {
                    excursion.GuidesExcursions.Add(model.GuideName, (model.Count, model.Cost));
                }
                else
                {
                    excursion.GuidesExcursions[model.GuideName] = (excursion.GuidesExcursions[model.GuideName].Item1 + model.Count, model.Cost);
                }
            }
            decimal sum = excursion.Sum + model.Sum;
            _excursionStorage.Update(new ExcursionBindingModel
            {
                Id = excursion.Id,
                UserId = excursion.UserId,
                Sum = sum,
                DatePayment = excursion.DatePayment,
                GuidesExcursions = excursion.GuidesExcursions,
                ToursExcursions = excursion.ToursExcursions
            });
        }
    }
}