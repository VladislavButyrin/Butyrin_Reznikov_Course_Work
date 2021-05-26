using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class GuideLogic
    {
        private readonly IGuideStorage _guideStorage;
        public GuideLogic(IGuideStorage guideStorage)
        {
            _guideStorage = guideStorage;
        }
        public List<GuideViewModel> Read(GuideBindingModel model)
        {
            if (model == null)
            {
                return _guideStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GuideViewModel> { _guideStorage.GetElement(model) };
            }
            return _guideStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(GuideBindingModel model)
        {
            var element = _guideStorage.GetElement(new GuideBindingModel
            {
                GuideName = model.GuideName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть гид с таким названием");
            }
            if (model.Id.HasValue)
            {
                _guideStorage.Update(model);
            }
            else
            {
                _guideStorage.Insert(model);
            }
        }
        public void Delete(GuideBindingModel model)
        {
            var element = _guideStorage.GetElement(new GuideBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("гид не найдено");
            }
            _guideStorage.Delete(model);
        }
    }
}