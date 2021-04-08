using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IPlaceStorage
    {
        List<TourViewModel> GetFullList();

        List<TourViewModel> GetFilteredList(TourBindingModel model);

        TourViewModel GetElement(TourBindingModel model);

        void Insert(TourBindingModel model);

        void Update(TourBindingModel model);

        void Delete(TourBindingModel model);
    }
}
