using System;
using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class VisitBusinessLogic
    {
        private readonly IVisitStorage _visitStorage;
        public VisitBusinessLogic(IVisitStorage visitStorage)
        {
            _visitStorage = visitStorage;
        }
        public List<VisitViewModel> Read(VisitBindingModel model)
        {
            if (model == null)
            {
                return _visitStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<VisitViewModel> { _visitStorage.GetElement(model) };
            }
            else return null;
        }
        public void AddService(AddServiceToVisitBindingModel model)
        {
            VisitViewModel visit = _visitStorage.GetElement(new VisitBindingModel
            {
                Id = model.DoctorVisitId
            });
            if (visit.ServicesVisits == null)
            {
                visit.ServicesVisits = new Dictionary<int, string>();
            }
            if (visit.ServicesVisits.ContainsKey((int)model.Service.Id))
            {
                throw new Exception("Невозможно привязать услугу");
            }
            visit.ServicesVisits.Add((int)model.Service.Id, model.Service.ServiceName);
            _visitStorage.Update(new VisitBindingModel { 
                Id = visit.Id,
                DateVisit = visit.DateVisit,
                ServicesVisits = visit.ServicesVisits,
                AnimalsVisits = visit.AnimalsVisits,
            });
        }
    }
}
