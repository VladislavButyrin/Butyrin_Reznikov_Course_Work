using System;
using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class PurchaseLogic
    {
        private readonly IPurchaseStorage _purchaseStorage;
        public PurchaseLogic(IPurchaseStorage purchaseStorage)
        {
            _purchaseStorage = purchaseStorage;
        }
        public List<PurchaseViewModel> Read()
        {
            return _purchaseStorage.GetFullList();
        }
    }
}
