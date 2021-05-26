using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface ITourStorage
    {
        List<TourViewModel> GetFullList();
        List<TourViewModel> GetFilteredList(TourBindingModel model);
        TourViewModel GetElement(TourBindingModel model);
        void Insert(TourBindingModel model);
        void Update(TourBindingModel model);
        void Delete(TourBindingModel model);
    }
}