﻿using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class OrganizatorLogic
    {
        private readonly IOrganizatorStorage _organizatorStorage;
        public OrganizatorLogic(IOrganizatorStorage userStorage)
        {
            _organizatorStorage = userStorage;
        }
        public List<OrganizatorViewModel> Read(OrganizatorBindingModel model)
        {
            if (model == null)
            {
                return _organizatorStorage.GetFullList();
            }
            if (model.Id.HasValue || model.Login != null)
            {
                return new List<OrganizatorViewModel> { _organizatorStorage.GetElement(model) };
            }
            return _organizatorStorage.GetFilteredList(model);
        }
        public void Login(OrganizatorBindingModel model)
        {
            var element = _organizatorStorage.GetElement(new OrganizatorBindingModel
            {
                Login = model.Login
            });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (element.Password != model.Password)
            {
                throw new Exception("Неверный пароль");
            }
        }
        public void CreateOrUpdate(OrganizatorBindingModel model)
        {
            var element = _organizatorStorage.GetElement(new OrganizatorBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть пользователь с таким логином");
            }
            if (model.Id.HasValue)
            {
                _organizatorStorage.Update(model);
            }
            else
            {
                _organizatorStorage.Insert(model);
            }
        }
        public void Delete(OrganizatorBindingModel model)
        {
            var element = _organizatorStorage.GetElement(new OrganizatorBindingModel
            {
                Id =  model.Id
            });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _organizatorStorage.Delete(model);
        }
    }
}
