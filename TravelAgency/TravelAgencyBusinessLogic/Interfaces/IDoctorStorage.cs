using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IDoctorStorage
    {
        List<DoctorViewModel> GetFullList();
        List<DoctorViewModel> GetFilteredList(DoctorBindingModel model);
        DoctorViewModel GetElement(DoctorBindingModel model);
        void Insert(DoctorBindingModel model);
        void Update(DoctorBindingModel model);
        void Delete(DoctorBindingModel model);
    }
}
