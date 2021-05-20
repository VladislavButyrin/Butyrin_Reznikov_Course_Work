using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ITripStorage _tripStorage;
        private readonly IGuideStorage _guideStorage;
        private readonly IGroupStorage _groupStorage;
        private readonly IExcursionStorage _excursionStorage;
        public ReportLogic(ITripStorage tripStorage, IGuideStorage
       guideStorage, IGroupStorage groupStorage, IExcursionStorage excursionStorage)
        {
            _tripStorage = tripStorage;
            _guideStorage = guideStorage;
            _groupStorage = groupStorage;
            _excursionStorage = excursionStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportExcursionTripViewModel> GetExcursionsTrip(ReportBindingModel model)
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
                        if (!guides.FirstOrDefault(rec => rec.Id == guide.Key).Trips.ContainsKey(trip))
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
        public List<ReportPlaceGuideViewModel> GetPlaceGuide(ReportBindingModel model)
        {
            var excursions = _excursionStorage.GetFilteredList(new ExcursionBindingModel { 
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var groups = _groupStorage.GetFilteredList(new GroupBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var list = new List<ReportPlaceGuideViewModel>();
            foreach (var excursion in excursions)
            {
                foreach (var guide in excursion.GuidesExcursions)
                {
                    var selectedgroups = groups.Where(rec => rec.DateGroup.Date == excursion.DatePayment.Date).ToList();
                    foreach (var group in selectedgroups)
                    {
                        var report = group.PlacesGroups.Values.Select(s => new ReportPlaceGuideViewModel
                        {
                            Date = group.DateGroup.Date,
                            PlaceName = s,
                            GuideName = guide.Value.Item1,
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
        public void SaveExcursionsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                GuideTrips = GetExcursionsTrip(model),
                NeededTrips = _tripStorage.GetFullList().Where(rec => model.Trips.Contains(rec.Id)).Select(rec => rec.TripName).ToList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveExcursionsToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                GuideTrips = GetExcursionsTrip(model),
                NeededTrips = _tripStorage.GetFullList().Where(rec => model.Trips.Contains(rec.Id)).Select(rec => rec.TripName).ToList()
            });
        }
        public void SaveOrderToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
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
