using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IMedicineStorage
    {
        List<MedicineViewModel> GetFullList();
        List<MedicineViewModel> GetFilteredList(MedicineBindingModel model);
        MedicineViewModel GetElement(MedicineBindingModel model);
        void Insert(MedicineBindingModel model);
        void Update(MedicineBindingModel model);
        void Delete(MedicineBindingModel model);
    }
}
