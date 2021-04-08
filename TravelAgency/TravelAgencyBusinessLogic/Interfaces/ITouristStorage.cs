using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface ITouristStorage
    {
        List<TouristViewModel> GetFullList();

        List<TouristViewModel> GetFilteredList(TouristBindingModel model);

        TouristViewModel GetElement(TouristBindingModel model);

        void Insert(TouristBindingModel model);

        void Update(TouristBindingModel model);

        void Delete(TouristBindingModel model);
    }
}
