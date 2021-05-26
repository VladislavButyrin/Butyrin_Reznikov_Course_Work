using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyBusinessLogic.BusinessLogic.Guarantor
{
    public class StatisticsLogic
    {
        private readonly IGroupStorage _GroupStorage;
        private readonly IExcursionStorage _ExcursionStorage;
        public StatisticsLogic(IGroupStorage GroupStorage, IExcursionStorage ExcursionStorage)
        {
            _GroupStorage = GroupStorage;
            _ExcursionStorage = ExcursionStorage;
        }
        public List<StatisticsByPlacesViewModel> GetStatisticsByPlaces(StatisticsBindingModelGuarantor model)
        {
            var Groups = _GroupStorage.GetFilteredList(new GroupBindingModel { DateFrom = model.DateFrom,DateTo = model.DateTo});
            var list = Groups.Where(Group => Group.GroupsPlaces.ContainsKey(model.ElementId)).GroupBy(Group => Group.DateGroup).Select(Group => new StatisticsByPlacesViewModel { date = Group.Key, count=Group.Count()}).ToList();
            return list;
        }
        public List<StatisticsByGuideViewModel> GetStatisticsByGuide(StatisticsBindingModelGuarantor model)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var Excursions = _ExcursionStorage.GetFilteredList(new ExcursionBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            foreach (var Excursion in Excursions)
            {
                foreach (var guide_Excursion in Excursion.GuidesExcursions)
                {
                    if (!list.ContainsValue(guide_Excursion.Value.Item1))
                        list.Add(guide_Excursion.Key,guide_Excursion.Value.Item2);
                }
            }
            return list.Select(l => new StatisticsByGuideViewModel { guideName = l.Key,cost=l.Value}).ToList();
        }
    }
}
