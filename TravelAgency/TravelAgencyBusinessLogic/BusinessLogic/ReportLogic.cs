using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TravelAgencyBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly ITourStorage _tourStorage;
        private readonly ITripStorage _tripStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly IGroupStorage _groupStorage;
        private readonly IPlaceStorage _placeStorage;
        private readonly IGuideStorage _guideStorage;
        public ReportLogic(ITourStorage tourStorage, ITripStorage tripStorage, IExcursionStorage excursionStorage, IGroupStorage groupStorage,
            IPlaceStorage placeStorage, IGuideStorage guideStorage)
        {
            _tourStorage = tourStorage;
            _tripStorage = tripStorage;
            _excursionStorage = excursionStorage;
            _groupStorage = groupStorage;
            _placeStorage = placeStorage;
            _guideStorage = guideStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
         /// <returns></returns>
        public List<ReportTourViewModel> GetTour()
        {
            var tours = _tourStorage.GetFullList();
            var excursions = _excursionStorage.GetFullList();
            var groups = _groupStorage.GetFullList();
            var list = new List<ReportTourViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportTourViewModel
                {
                    TourName = tour.TourName  
                };
                foreach (var excursion in excursions)
                {
                    foreach (var excursionTour in excursion.Tours)
                    {
                        if (excursionTour.Value == tour.TourName)
                        {
                            record.Excursions.Add(excursion.ExcursionName);
                        }
                    }
                }
                foreach (var group in groups)
                {
                    foreach (var groupTour in group.Tours)
                    {
                        if (groupTour.Value == tour.TourName)
                        {
                            record.GroupId = (int)group.Id;
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportGuidePlaceViewModel> GetGuidePlace(ReportBindingModel model)
        {
            var trips = _tripStorage.GetFullList();
            var guides = _guideStorage.GetFullList();
            var places = _placeStorage.GetFullList();
            var list = new List<ReportGuidePlaceViewModel>();
            foreach (var guide in guides)
            {
                var record = new ReportGuidePlaceViewModel
                {
                    GuideName = guide.GuideName
                };

                foreach(var trip in trips)
                {
                    foreach (var place in places)
                    {
                        if (trip.Id == guide.TripId && place.Trips.ContainsValue(trip.TripName))
                        {
                            record.PlaceName = place.PlaceName;
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }
    }
}
