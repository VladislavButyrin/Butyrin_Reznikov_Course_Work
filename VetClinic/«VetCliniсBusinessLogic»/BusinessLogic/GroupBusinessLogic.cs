using System;
using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class GroupBusinessLogic
    {
        private readonly IGroupStorage _groupStorage;
        public GroupBusinessLogic(IGroupStorage groupStorage)
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
            else return null;
        }
        public void AddPlace(AddPlaceToGroupBindingModel model)
        {
            GroupViewModel group = _groupStorage.GetElement(new GroupBindingModel
            {
                Id = model.OrganizatorGroupId
            });
            if (group.PlacesGroups == null)
            {
                group.PlacesGroups = new Dictionary<int, string>();
            }
            if (group.PlacesGroups.ContainsKey((int)model.Place.Id))
            {
                throw new Exception("Невозможно привязать услугу");
            }
            group.PlacesGroups.Add((int)model.Place.Id, model.Place.PlaceName);
            _groupStorage.Update(new GroupBindingModel { 
                Id = group.Id,
                DateGroup = group.DateGroup,
                PlacesGroups = group.PlacesGroups,
                ToursGroups = group.ToursGroups,
            });
        }
    }
}
