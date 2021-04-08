using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class GuideLogic
    {
        private readonly IGuideStorage _GuideStorage;
        public GuideLogic(IGuideStorage GuideStorage)
        {
            _GuideStorage = GuideStorage;
        }

        public List<GuideViewModel> Read(GuideBindingModel model)
        {
            if (model == null)
            {
                return _GuideStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GuideViewModel> { _GuideStorage.GetElement(model) };
            }
            return _GuideStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(GuideBindingModel model)
        {
            var element = _GuideStorage.GetElement(new GuideBindingModel
            {
                GuideName = model.GuideName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть документ с таким названием");
            }
            if (model.Id.HasValue)
            {
                _GuideStorage.Update(model);
            }
            else
            {
                _GuideStorage.Insert(model);
            }
        }
        public void Delete(GuideBindingModel model)

        {
            var element = _GuideStorage.GetElement(new GuideBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _GuideStorage.Delete(model);
        }
    }
}
