using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IExcursionStorage
    {
        List<ExcursionViewModel> GetFullList();
        List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model);
    }
}
