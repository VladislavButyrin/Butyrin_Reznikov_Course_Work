using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class ServiceBusinessLogic
    {
        private readonly IServiceStorage _serviceStorage;
        public ServiceBusinessLogic(IServiceStorage serviceStorage)
        {
            _serviceStorage = serviceStorage;
        }
        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            if (model == null)
            {
                return _serviceStorage.GetFullList();
            }
            if (model.DoctorId.HasValue)
            {
                return _serviceStorage.GetFilteredList(model);
            }
            if (model.Id.HasValue)
            {
                return new List<ServiceViewModel> { _serviceStorage.GetElement(model) };
            }
            return _serviceStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ServiceBindingModel model)
        {
            var element = _serviceStorage.GetElement(new ServiceBindingModel
            {
                ServiceName = model.ServiceName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть лекарство с таким названием");
            }
            if (model.Id.HasValue)
            {
                _serviceStorage.Update(model);
            }
            else
            {
                _serviceStorage.Insert(model);
            }
        }
        public void Delete(ServiceBindingModel model)
        {
            var element = _serviceStorage.GetElement(new ServiceBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _serviceStorage.Delete(model);
        }
    }
}
