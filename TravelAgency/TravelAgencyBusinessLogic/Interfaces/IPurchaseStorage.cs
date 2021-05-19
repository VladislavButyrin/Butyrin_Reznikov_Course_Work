using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IPurchaseStorage
    {
        List<PurchaseViewModel> GetFullList();
        List<PurchaseViewModel> GetFilteredList(PurchaseBindingModel model);
    }
}
