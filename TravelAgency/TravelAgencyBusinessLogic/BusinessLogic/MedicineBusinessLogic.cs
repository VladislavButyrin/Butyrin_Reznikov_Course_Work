using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class MedicineBusinessLogic
    {
        private readonly IMedicineStorage _medicineStorage;
        public MedicineBusinessLogic(IMedicineStorage medicineStorage)
        {
            _medicineStorage = medicineStorage;
        }
        public List<MedicineViewModel> Read(MedicineBindingModel model)
        {
            if (model == null)
            {
                return _medicineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MedicineViewModel> { _medicineStorage.GetElement(model) };
            }
            return _medicineStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(MedicineBindingModel model)
        {
            var element = _medicineStorage.GetElement(new MedicineBindingModel
            {
                MedicineName = model.MedicineName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть лекарство с таким названием");
            }
            if (model.Id.HasValue)
            {
                _medicineStorage.Update(model);
            }
            else
            {
                _medicineStorage.Insert(model);
            }
        }
        public void Delete(MedicineBindingModel model)
        {
            var element = _medicineStorage.GetElement(new MedicineBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _medicineStorage.Delete(model);
        }
    }
}
