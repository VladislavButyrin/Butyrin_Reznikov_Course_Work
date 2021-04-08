using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class UserLogic
    {
        private readonly IUserStorage _UserStorage;
        public UserLogic(IUserStorage UserStorage)
        {
            _UserStorage = UserStorage;
        }

        public List<UserViewModel> Read(UserBindingModel model)
        {
            if (model == null)
            {
                return _UserStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<UserViewModel> { _UserStorage.GetElement(model) };
            }
            return _UserStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(UserBindingModel model)
        {
            var element = _UserStorage.GetElement(new UserBindingModel
            {
                UserName = model.UserName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть документ с таким названием");
            }
            if (model.Id.HasValue)
            {
                _UserStorage.Update(model);
            }
            else
            {
                _UserStorage.Insert(model);
            }
        }
        public void Delete(UserBindingModel model)

        {
            var element = _UserStorage.GetElement(new UserBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _UserStorage.Delete(model);
        }
    }
}
