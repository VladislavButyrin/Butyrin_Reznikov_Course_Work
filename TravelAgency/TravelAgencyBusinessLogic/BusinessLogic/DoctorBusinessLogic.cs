using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class DoctorBusinessLogic
    {
        private readonly IDoctorStorage _doctorStorage;
        public DoctorBusinessLogic(IDoctorStorage userStorage)
        {
            _doctorStorage = userStorage;
        }
        public List<DoctorViewModel> Read(DoctorBindingModel model)
        {
            if (model == null)
            {
                return _doctorStorage.GetFullList();
            }
            if (model.Id.HasValue || model.Login != null)
            {
                return new List<DoctorViewModel> { _doctorStorage.GetElement(model) };
            }
            return _doctorStorage.GetFilteredList(model);
        }
        public void Login(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel
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
        public void CreateOrUpdate(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть пользователь с таким логином");
            }
            if (model.Id.HasValue)
            {
                _doctorStorage.Update(model);
            }
            else
            {
                _doctorStorage.Insert(model);
            }
        }
        public void Delete(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel
            {
                Id =  model.Id
            });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _doctorStorage.Delete(model);
        }
    }
}
