using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IExcursionStorage
    {
        List<ExcursionViewModel> GetFullList();
        List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model);
        ExcursionViewModel GetElement(ExcursionBindingModel model);
        void Insert(ExcursionBindingModel model);
        void Update(ExcursionBindingModel model);
        void Delete(ExcursionBindingModel model);
    }
}