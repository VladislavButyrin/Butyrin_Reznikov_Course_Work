using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class GroupStorage
    {
        private readonly DataListSingleton source;
        public GroupStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<GroupViewModel> GetFullList()
        {
            List<GroupViewModel> result = new List<GroupViewModel>();
            foreach (var Group in source.Groups)
            {
                result.Add(CreateModel(Group));
            }
            return result;
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<GroupViewModel> result = new List<GroupViewModel>();
            foreach (var group in source.Groups)
            {
                if (group.Id == model.Id)
                {
                    result.Add(CreateModel(group));
                }
            }
            return result;
        }
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var group in source.Groups)
            {
                if (group.Id == model.Id)
                {
                    return CreateModel(group);
                }
            }
            return null;
        }
        public void Insert(GroupBindingModel model)
        {
            Group tempGroup = new Group { Id = 1 };
            foreach (var group in source.Groups)
            {
                if (group.Id >= tempGroup.Id)
                {
                    tempGroup.Id = group.Id + 1;
                }
            }
            source.Groups.Add(CreateModel(model, tempGroup));
        }
        public void Update(GroupBindingModel model)
        {

            Group tempGroup = null;
            foreach (var group in source.Groups)
            {
                if (group.Id == model.Id)
                {
                    tempGroup = group;
                }
            }
            if (tempGroup == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempGroup);
        }
        public void Delete(GroupBindingModel model)
        {
            for (int i = 0; i < source.Groups.Count; ++i)
            {
                if (source.Groups[i].Id == model.Id.Value)
                {
                    source.Groups.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Group CreateModel(GroupBindingModel model, Group Group)
        {
            // удаляем убранные
            foreach (var key in Group.Tours.Keys.ToList())
            {
                if (!model.Tours.ContainsKey(key))
                {
                    Group.Tours.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var tour in model.Tours)
            {
                if (Group.Tours.ContainsKey(tour.Key))
                {
                    Group.Tours[tour.Key] =
                    model.Tours[tour.Key];
                }
                else
                {
                    Group.Tours.Add(tour.Key,
                    model.Tours[tour.Key]);
                }
            }


            foreach (var key in Group.Tourists.Keys.ToList())
            {
                if (!model.Tourists.ContainsKey(key))
                {
                    Group.Tourists.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var tourist in model.Tourists)
            {
                if (Group.Tourists.ContainsKey(tourist.Key))
                {
                    Group.Tourists[tourist.Key] =
                    model.Tourists[tourist.Key];
                }
                else
                {
                    Group.Tourists.Add(tourist.Key,
                    model.Tourists[tourist.Key]);
                }
            }
            return Group;
        }
        private GroupViewModel CreateModel(Group Group)
        {
            Dictionary<int, string> tours = new
        Dictionary<int, string>();
            foreach (var db in Group.Tours)
            {
                string tourName = string.Empty;
                foreach (var tour in source.Tours)
                {
                    if (db.Key == tour.Id)
                    {
                        tourName = tour.TourName;
                        break;
                    }
                }
                tours.Add(db.Key, tourName);
            }

            Dictionary<int, string> tourists = new
        Dictionary<int, string>();


            foreach (var db in Group.Tourists)
            {
                string touristName = string.Empty;
                foreach (var tourist in source.Tourists)
                {
                    if (db.Key == tourist.Id)
                    {
                        touristName = tourist.TouristName;
                        break;
                    }
                }
                tours.Add(db.Key, touristName);
            }

            return new GroupViewModel
            {
                Id = Group.Id
            };
        }
    }
}
