using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
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
