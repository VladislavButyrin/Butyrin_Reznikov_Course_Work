using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();
        UserViewModel GetElement(UserBindingModel model);
        void Insert(UserBindingModel model);
        void Update(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}