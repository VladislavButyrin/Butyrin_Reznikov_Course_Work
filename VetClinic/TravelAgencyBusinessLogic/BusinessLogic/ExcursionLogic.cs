using System;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ExcursionLogic
    {
        private readonly IExcursionStorage _excursionStorage;
        public ExcursionLogic(IExcursionStorage excursionStorage)
        {
            _excursionStorage = excursionStorage;
        }
        public List<ExcursionViewModel> Read()
        {
            return _excursionStorage.GetFullList();
        }
    }
}
