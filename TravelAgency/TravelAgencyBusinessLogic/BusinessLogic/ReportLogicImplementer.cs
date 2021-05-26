using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogicImplementer
    {
        private readonly IGroupStorage _groupStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly ITourStorage _tourStorage;
        private readonly IUserStorage _userStorage;
        public ReportLogicImplementer(IGroupStorage groupStorage, IExcursionStorage excursionStorage, ITourStorage tourStorage, IUserStorage userStorage)
        {
            _groupStorage = groupStorage;
            _excursionStorage = excursionStorage;
            _tourStorage = tourStorage;
            _userStorage = userStorage;
        }
        public List<ReportViewModel> GetPlaces(ReportBindingModelImplementer model)
        {
            var groups = _groupStorage.GetFullList();
            var list = new List<ReportViewModel>();
            foreach (var tour in model.ToursName)
            {
                var places = new List<string>();
                foreach(var group in groups)
                {
                    if (!group.ToursGroups.ContainsValue(tour))
                    {
                        continue;
                    }
                    places.AddRange(group.GroupsPlaces.Values.ToList());
                }
                var readyListPlaces = new List<string>();
                var g = places.GroupBy(x => x);
                foreach (var grp in g)
                {
                    readyListPlaces.Add($"{grp.Key} — ({grp.Count()})");
                }
                var record = new ReportViewModel
                {
                    TourName = tour,
                    Places = readyListPlaces
                };
                list.Add(record);
            }
            return list;
        }
        public List<ReportToursGroupsExcursionsViewModel> GetToursExcursionsGroups(ReportBindingModelImplementer model)
        {
            var groups = _groupStorage.GetFilteredList(new GroupBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                UserId = model.UserId
            });
            var excursions = _excursionStorage.GetFilteredList(new ExcursionBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                UserId = model.UserId
            });
            var tours = _tourStorage.GetFullList();
            var list = new List<ReportToursGroupsExcursionsViewModel>();
            foreach (var tour in tours)
            {
                var selectedGroups = new List<string>();
                var selectedExcursions = new List<string>();
                List<int> listExcursionId = new List<int>();
                List<int> listGroupId = new List<int>();
                foreach (var group in groups)
                {
                    if (group.ToursGroups.ContainsValue(tour.TourName))
                    {
                        selectedGroups.Add($"Дата: {group.DateGroup.ToShortDateString()}  —  Кол-во услуг: {group.GroupsPlaces.Count}");
                        listGroupId.Add(group.Id);
                    }
                }
                foreach (var excursion in excursions)
                {
                    if (excursion.ToursExcursions.Contains(tour.TourName))
                    {
                        selectedExcursions.Add($"Дата: {excursion.DatePayment.ToShortDateString()}  —  Кол-во лекарств: {excursion.GuidesExcursions.Count}  —  Сумма: {excursion.Sum.ToString("G", CultureInfo.InvariantCulture)} руб.");
                        listExcursionId.Add(excursion.Id);
                    }
                }
                int maxLength = selectedExcursions.Count >= selectedGroups.Count ? selectedExcursions.Count : selectedGroups.Count;
                for (int i = 0; i < maxLength; i++)
                {
                    tour.TourName = i > 0 ? "" : tour.TourName;
                    var group = i >= selectedGroups.Count ? "" : selectedGroups[i];
                    var excursion = i >= selectedExcursions.Count ? "" : selectedExcursions[i];
                    var excursionId = i >= listExcursionId.Count ? 0 : listExcursionId[i];
                    var groupId = i >= listGroupId.Count ? 0 : listGroupId[i];
                    if (selectedExcursions.Count != 0 || selectedGroups.Count != 0)
                    {
                        var record = new ReportToursGroupsExcursionsViewModel
                        {
                            TourName = tour.TourName,
                            Excursions = excursion,
                            Groups = group,
                            ExcursionId = excursionId,
                            GroupId = groupId
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }
        public void SavePlacesToWordFile(ReportBindingModelImplementer model)
        {
            SaveToWord.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список услуг животных",
                Places = GetPlaces(model),
            });
        }
        public void SaveToursToExcelFile(ReportBindingModelImplementer model)
        {
            SaveToExcel.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список услуг животных",
                Places = GetPlaces(model),
            });
        }
        [Obsolete]
        public void SaveToursGroupsExcursionsToPDFFile(ReportBindingModelImplementer model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
                Title = "Список животных, их покупок и посещений",
                ToursGroupsExcursions = GetToursExcursionsGroups(model),
                Username = _userStorage.GetElement(new UserBindingModel { Id = model.UserId })?.Fullname,
            });
            MailLogicImplementer.MailSendAsync(new MailSendInfo
            {
                MailAddress = model.LoginCurrentUserInSystem,
                Subject = $"Новый отчет",
                Text = $"Отчет от {DateTime.Now}"
            });
        }
    }
}