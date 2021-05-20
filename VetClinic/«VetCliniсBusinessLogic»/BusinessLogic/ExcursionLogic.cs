using System;
using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;

namespace _VetCliniсBusinessLogic_.BusinessLogic
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
