using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IOrganizatorStorage
    {
        List<OrganizatorViewModel> GetFullList();
        List<OrganizatorViewModel> GetFilteredList(OrganizatorBindingModel model);
        OrganizatorViewModel GetElement(OrganizatorBindingModel model);
        void Insert(OrganizatorBindingModel model);
        void Update(OrganizatorBindingModel model);
        void Delete(OrganizatorBindingModel model);
    }
}
