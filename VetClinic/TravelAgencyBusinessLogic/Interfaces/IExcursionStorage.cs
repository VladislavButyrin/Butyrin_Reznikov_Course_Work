using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IExcursionStorage
    {
        List<ExcursionViewModel> GetFullList();
        List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model);
    }
}
