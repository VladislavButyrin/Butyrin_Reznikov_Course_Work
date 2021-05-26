using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IGroupStorage
    {
        List<GroupViewModel> GetFullList();
        List<GroupViewModel> GetFilteredList(GroupBindingModel model);
        GroupViewModel GetElement(GroupBindingModel model);
        void Update(GroupBindingModel model);
    }
}
