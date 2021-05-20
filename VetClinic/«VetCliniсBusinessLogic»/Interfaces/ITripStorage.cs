using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface ITripStorage
    {
        List<TripViewModel> GetFullList();
        List<TripViewModel> GetFilteredList(TripBindingModel model);
        TripViewModel GetElement(TripBindingModel model);
        void Insert(TripBindingModel model);
        void Update(TripBindingModel model);
        void Delete(TripBindingModel model);
    }
}
