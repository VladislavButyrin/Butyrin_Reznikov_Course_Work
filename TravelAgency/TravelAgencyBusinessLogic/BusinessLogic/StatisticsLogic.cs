using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class StatisticsLogic
    {
        private readonly IGroupStorage _groupStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly ITourStorage _tourStorage;
        public StatisticsLogic(IGroupStorage groupStorage, IExcursionStorage excursionStorage, ITourStorage tourStorage)
        {
            _groupStorage = groupStorage;
            _excursionStorage = excursionStorage;
            _tourStorage = tourStorage;
        }
        public List<StatisticsByToursViewModel> GetTours(StatisticsBindingModelImplementer model)
        {
            var groups = _groupStorage.GetFilteredList(new GroupBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var tours = _tourStorage.GetFullList();
            var list = new List<StatisticsByToursViewModel>();
            foreach (var tour in tours)
            {
                var numberOfGroups = new Dictionary<string, int>();
                var counter = 0;
                foreach (var group in groups)
                {
                    if (!group.ToursGroups.ContainsValue(tour.TourName))
                    {
                        continue;
                    }
                    counter++;
                }
                numberOfGroups.Add(tour.TourName, counter);
                var record = new StatisticsByToursViewModel
                {
                    TourName = tour.TourName,
                    NumberOfGroups = numberOfGroups[tour.TourName]
                };
                list.Add(record);
            }
            return list;
        }
        public List<StatisticsByGroupsAndExcursionsViewModel> GetGroups(StatisticsBindingModelImplementer model)
        {
            var groups = _groupStorage.GetFilteredList(new GroupBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var list = new List<StatisticsByGroupsAndExcursionsViewModel>();
            var numberOfGroups = new Dictionary<DateTime, int>();
            foreach (var group in groups)
            {
                if (numberOfGroups.Count == 0 || !numberOfGroups.ContainsKey(group.DateGroup))
                {
                    numberOfGroups.Add(group.DateGroup, 1);
                } 
                else if (numberOfGroups.ContainsKey(group.DateGroup))
                {
                    numberOfGroups[group.DateGroup] += 1;
                }
            }
            foreach (var item in numberOfGroups)
            {
                var record = new StatisticsByGroupsAndExcursionsViewModel
                {
                    Data = item.Key.ToShortDateString(),
                    AmountPerDay = item.Value
                };
                list.Add(record);
            }
            return list;
        }
        public List<StatisticsByGroupsAndExcursionsViewModel> GetExcursions(StatisticsBindingModelImplementer model)
        {
            var excursions = _excursionStorage.GetFilteredList(new ExcursionBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var list = new List<StatisticsByGroupsAndExcursionsViewModel>();
            var proceeds = new Dictionary<string, int>();
            foreach (var excursion in excursions)
            {
                if (proceeds.Count == 0 || !proceeds.ContainsKey(excursion.DatePayment.ToShortDateString()))
                {
                    proceeds.Add(excursion.DatePayment.ToShortDateString(), (int)excursion.Sum);
                }
                else if (proceeds.ContainsKey(excursion.DatePayment.ToShortDateString()))
                {
                    proceeds[excursion.DatePayment.ToShortDateString()] += (int)excursion.Sum;
                }
            }
            foreach (var item in proceeds)
            {
                var record = new StatisticsByGroupsAndExcursionsViewModel
                {
                    Data = item.Key,
                    AmountPerDay = item.Value
                };
                list.Add(record);
            }
            return list;
        }
    }
}