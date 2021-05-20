using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using VetClinikEntitiesImplements.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VetClinikEntitiesImplements;

namespace VetClinikEntitiesImplements.Implements
{
    public class ExcursionStorage : IExcursionStorage
    {
        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            using (var context = new VetClinicDataBase())
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
            using (var context = new VetClinicDataBase())
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
                    DatePayment = rec.DatePayment,
                    GuidesExcursions = rec.GuidesExcursions.ToDictionary(recTC => context.Guides.FirstOrDefault(recMN => recMN.Id == recTC.GuideId).Id,
                    recTC => (recTC.Guide.GuideName,recTC.Count, context.Guides.FirstOrDefault(recMP => recMP.Id == recTC.GuideId).Cost)),
                    ToursExcursions = rec.ToursExcursions.Select(recAP => recAP.Tour.TourName).ToList()
                })
                .ToList();
            }
        }
    }
}
