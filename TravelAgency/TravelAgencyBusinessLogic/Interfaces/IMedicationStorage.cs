using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IMedicationStorage
    {
        List<MedicationViewModel> GetFullList();
        List<MedicationViewModel> GetFilteredList(MedicationBindingModel model);
        MedicationViewModel GetElement(MedicationBindingModel model);
        void Insert(MedicationBindingModel model);
        void Update(MedicationBindingModel model);
        void Delete(MedicationBindingModel model);
    }
}
