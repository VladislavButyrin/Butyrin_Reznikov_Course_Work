using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IPlaceStorage
    {
        List<PlaceViewModel> GetFullList();

        List<PlaceViewModel> GetFilteredList(PlaceBindingModel model);

        PlaceViewModel GetElement(PlaceBindingModel model);

        void Insert(PlaceBindingModel model);

        void Update(PlaceBindingModel model);

        void Delete(PlaceBindingModel model);
    }
}
