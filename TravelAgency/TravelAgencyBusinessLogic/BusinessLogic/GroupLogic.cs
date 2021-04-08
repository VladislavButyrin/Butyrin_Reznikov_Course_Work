using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class GroupLogic
    {
        private readonly IGroupStorage _GroupStorage;
        public GroupLogic(IGroupStorage GroupStorage)
        {
            _GroupStorage = GroupStorage;
        }

        public List<GroupViewModel> Read(GroupBindingModel model)
        {
            if (model == null)
            {
                return _GroupStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GroupViewModel> { _GroupStorage.GetElement(model) };
            }
            return _GroupStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(GroupBindingModel model)
        {

            if (model.Id.HasValue)
            {
                _GroupStorage.Update(model);
            }
            else
            {
                _GroupStorage.Insert(model);
            }
        }
        public void Delete(GroupBindingModel model)

        {
            var element = _GroupStorage.GetElement(new GroupBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _GroupStorage.Delete(model);
        }
    }
}
