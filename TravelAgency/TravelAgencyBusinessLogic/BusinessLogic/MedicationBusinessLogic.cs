using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class MedicationBusinessLogic
    {
        private readonly IMedicationStorage _medicationStorage;
        public MedicationBusinessLogic(IMedicationStorage medicationStorage)
        {
            _medicationStorage = medicationStorage;
        }
        public List<MedicationViewModel> Read(MedicationBindingModel model)
        {
            if (model == null)
            {
                return _medicationStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MedicationViewModel> { _medicationStorage.GetElement(model) };
            }
            return _medicationStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(MedicationBindingModel model)
        {
            var element = _medicationStorage.GetElement(new MedicationBindingModel
            {
                MedicationName = model.MedicationName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть медикамент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _medicationStorage.Update(model);
            }
            else
            {
                _medicationStorage.Insert(model);
            }
        }
        public void Delete(MedicationBindingModel model)
        {
            var element = _medicationStorage.GetElement(new MedicationBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Медикамент не найден");
            }
            _medicationStorage.Delete(model);
        }
    }
}