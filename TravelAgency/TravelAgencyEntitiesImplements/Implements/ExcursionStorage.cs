using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyEntitiesImplements.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyEntitiesImplements;

namespace TravelAgencyEntitiesImplements.Implements
{
    public class ExcursionStorage : IExcursionStorage
    {
        public ExcursionViewModel GetElement(ExcursionBindingModel groupBindingModel)
        {
            throw new NotImplementedException();
        }

        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Excursions
                .Include(rec => rec.ToursExcursions)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GuidesExcursions)
                .ThenInclude(rec => rec.Guide)
                .ToList().Where(rec => rec.DatePayment >= model.DateFrom && rec.DatePayment <= model.DateTo)
                .Select(rec => new ExcursionViewModel
                {
                    Id = rec.Id,
                    Sum = rec.Sum, 
                    ExcursionName = rec.ExcursionName,
                    DatePayment = rec.DatePayment,
                    GuidesExcursions = rec.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).Id,
                    recTC => (recTC.Guide.GuideName, recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                    ToursExcursions = rec.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
                })
                .ToList();
            }
        }

        public List<ExcursionViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Excursions
                .Include(rec => rec.ToursExcursions)
                .ThenInclude(rec => rec.Tour)
                .Include(rec => rec.GuidesExcursions)
                .ThenInclude(rec => rec.Guide)
                .ToList()
                .Select(rec => new ExcursionViewModel
                {
                    Id = rec.Id,
                    Sum = rec.Sum,
                    ExcursionName = rec.ExcursionName,
                    DatePayment = rec.DatePayment,
                    GuidesExcursions = rec.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).Id,
                    recTC => (recTC.Guide.GuideName,recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                    ToursExcursions = rec.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
                })
                .ToList();
            }
        }

        void IExcursionStorage.Update(ExcursionBindingModel groupBindingModel)
        {
            throw new NotImplementedException();
        }
    }
}
