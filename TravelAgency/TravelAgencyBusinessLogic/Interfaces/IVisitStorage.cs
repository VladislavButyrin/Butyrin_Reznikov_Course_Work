using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IVisitStorage
    {
        List<VisitViewModel> GetFullList();
        List<VisitViewModel> GetFilteredList(VisitBindingModel model);
        VisitViewModel GetElement(VisitBindingModel model);
        void Update(VisitBindingModel model);
    }
}
