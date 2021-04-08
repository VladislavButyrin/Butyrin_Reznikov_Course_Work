using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class GuideStorage
    {
        private readonly DataListSingleton source;
        public GuideStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<GuideViewModel> GetFullList()
        {
            List<GuideViewModel> result = new List<GuideViewModel>();
            foreach (var guide in source.Guides)
            {
                result.Add(CreateModel(guide));
            }
            return result;
        }
        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<GuideViewModel> result = new List<GuideViewModel>();
            foreach (var guide in source.Guides)
            {
                if (guide.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(guide));
                }
            }
            return result;
        }
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var guide in source.Guides)
            {
                if (guide.Id == model.Id || guide.Name ==
               model.Name)
                {
                    return CreateModel(guide);
                }
            }
            return null;
        }
        public void Insert(GuideBindingModel model)
        {
            Guide tempGuide = new Guide { Id = 1 };
            foreach (var guide in source.Guides)
            {
                if (guide.Id >= tempGuide.Id)
                {
                    tempGuide.Id = guide.Id + 1;
                }
            }
            source.Guides.Add(CreateModel(model, tempGuide));
        }
        public void Update(GuideBindingModel model)
        {

            Guide tempGuide = null;
            foreach (var component in source.Guides)
            {
                if (component.Id == model.Id)
                {
                    tempGuide = component;
                }
            }
            if (tempGuide == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempGuide);
        }
        public void Delete(GuideBindingModel model)
        {
            for (int i = 0; i < source.Guides.Count; ++i)
            {
                if (source.Guides[i].Id == model.Id.Value)
                {
                    source.Guides.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Guide CreateModel(GuideBindingModel model, Guide Guide)
        {
            Guide.Name = model.Name;
            Guide.TripId = model.TripId;
            return Guide;
        }
        private GuideViewModel CreateModel(Guide guide)
        {
            return new GuideViewModel
            {
                Id = guide.Id,
                Name = guide.Name,
                TripId = guide.TripId
            };
        }
    }
}
