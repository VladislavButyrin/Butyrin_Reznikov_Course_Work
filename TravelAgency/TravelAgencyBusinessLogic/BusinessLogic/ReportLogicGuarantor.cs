using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels.Guarantor;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.BusinessLogic.Guarantor;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogicGuarantor
    {

        private readonly ITripStorage _tripStorage;
        private readonly IGuideStorage _guideStorage;
        private readonly IGroupStorage _groupStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly IPlaceStorage _placeStorage;

        public ReportLogicGuarantor(ITripStorage tripStorage, IGuideStorage
       guideStorage, IGroupStorage groupStorage, IExcursionStorage excursionStorage, IPlaceStorage placeStorage)
        {
            _tripStorage = tripStorage;
            _guideStorage = guideStorage;
            _groupStorage = groupStorage;
            _excursionStorage = excursionStorage;
            _placeStorage = placeStorage;
        }

        public List<ReportExcursionTripViewModel> GetExcursionsTrip(ReportBindingModelGuarantor model)
        {
            var trips = _tripStorage.GetFullList();
            var excursions = _excursionStorage.GetFullList();
            var guides = _guideStorage.GetFullList();
            var list = new List<ReportExcursionTripViewModel>();
            foreach (var excursion in excursions)
            {
                bool have_trips = true;
                foreach (int trip in model.Trips) 
                {
                    foreach (var guide in excursion.GuidesExcursions)
                    {
                        if (!guides.FirstOrDefault(rec => rec.Id == guide.Value.Item1).Trips.ContainsKey(trip))
                        {
                            have_trips = false;
                        }
                    }
                }
                if (!have_trips)
                    continue;
                var record = new ReportExcursionTripViewModel
                {
                    ExcursionId = excursion.Id,
                    Date = excursion.DatePayment,
                    Sum = excursion.Sum,
                };
                list.Add(record);
            }
            return list;
        }
        public List<ReportPlaceGuideViewModel> GetPlaceGuide(ReportBindingModelGuarantor model)
        {
            var excursions = _excursionStorage.GetFilteredList(new ExcursionBindingModel { 
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var places = _placeStorage.GetFilteredList(new PlaceBindingModel
            {
                OrganizatorId = model.OrganizatorId
            }).Select(service => service.Id).ToList();
            var list = new List<ReportPlaceGuideViewModel>();
            foreach (var excursion in excursions)
            {
                foreach (var guide in excursion.GuidesExcursions)
                {
                    var selectedgroups = _groupStorage.GetFilteredList(new GroupBindingModel
                    {
                        DateGroup = excursion.DatePayment
                    });
                    foreach (var group in selectedgroups)
                    {
                        var report = group.GroupsPlaces.Where(sv => places.Contains(sv.Key)).Select(s => new ReportPlaceGuideViewModel
                        {
                            Date = group.DateGroup.Date,
                            PlaceName = s.Value,
                            GuideName = guide.Key,
                            GuideCount = guide.Value.Item2
                        }).ToList();
                        report.ForEach(rep => list.Add(rep));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveExcursionsToWordFile(ReportBindingModelGuarantor model)
        {
            SaveToWordGuarantor.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий по поездкам",
                GuideTrips = GetExcursionsTrip(model),
                NeededTrips = _tripStorage.GetFullList().Where(rec => model.Trips.Contains(rec.Id)).Select(rec => rec.TripName).ToList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveExcursionsToExcelFile(ReportBindingModelGuarantor model)
        {
            SaveToExcelGuarantor.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий на основе выбранных поездок",
                GuideTrips = GetExcursionsTrip(model),
                NeededTrips = _tripStorage.GetFullList().Where(rec => model.Trips.Contains(rec.Id)).Select(rec => rec.TripName).ToList()
            });
        }

        [Obsolete]
        public void SaveOrderToPdfFile(ReportBindingModelGuarantor model)
        {
            SaveToPdfGuarantor.CreateDoc(new PdfInfo
            { 
                FileName = model.FileName,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
                Title = "Отчёт",
                PlacesGuides = GetPlaceGuide(model)
            });
        }
    }
}
