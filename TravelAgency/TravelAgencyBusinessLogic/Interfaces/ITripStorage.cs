using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using System.Text;

namespace TravelAgencyBusinessLogic.Interfaces
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
