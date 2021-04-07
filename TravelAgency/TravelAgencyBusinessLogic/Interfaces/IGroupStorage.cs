using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using System.Text;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IGroupStorage
    {
        List<GroupViewModel> GetFullList();

        List<GroupViewModel> GetFilteredList(GroupBindingModel model);

        GroupViewModel GetElement(GroupBindingModel model);

        void Insert(GroupBindingModel model);

        void Update(GroupBindingModel model);

        void Delete(GroupBindingModel model);
    }
}
