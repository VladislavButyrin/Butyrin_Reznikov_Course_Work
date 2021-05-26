using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class GroupLogic
    {
        private readonly IGroupStorage _groupStorage;
        public GroupLogic(IGroupStorage groupStorage)
        {
            _groupStorage = groupStorage;
        }
        public List<GroupViewModel> Read(GroupBindingModel model)
        {
            if (model == null)
            {
                return _groupStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GroupViewModel> { _groupStorage.GetElement(model) };
            }
            return _groupStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть посещение с таким ID");
            }
            if (model.Id.HasValue)
            {
                _groupStorage.Update(model);
            }
            else
            {
                _groupStorage.Insert(model);
            }
        }
        public void Delete(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Посещение не найдено");
            }
            _groupStorage.Delete(model);
        }

        public void AddPlace(AddPlaceToGroupBindingModel model)
        {
            GroupViewModel group = _groupStorage.GetElement(new GroupBindingModel
            {
                Id = model.OrganizatorGroupId
            });
            if (group.GroupsPlaces == null)
            {
                group.GroupsPlaces = new Dictionary<int, string>();
            }
            if (group.GroupsPlaces.ContainsKey((int)model.Place.Id))
            {
                throw new Exception("Невозможно привязать услугу");
            }
            group.GroupsPlaces.Add((int)model.Place.Id, model.Place.PlaceName);
            _groupStorage.Update(new GroupBindingModel
            {
                Id = group.Id,
                DateGroup = group.DateGroup,
                GroupsPlaces = group.GroupsPlaces,
                ToursGroups = group.ToursGroups,
            });
        }
    }
}