using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.Interfaces
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
