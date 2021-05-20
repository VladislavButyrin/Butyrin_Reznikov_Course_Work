using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IGroupStorage
    {
        List<GroupViewModel> GetFullList();
        List<GroupViewModel> GetFilteredList(GroupBindingModel model);
        GroupViewModel GetElement(GroupBindingModel model);
        void Update(GroupBindingModel model);
    }
}
