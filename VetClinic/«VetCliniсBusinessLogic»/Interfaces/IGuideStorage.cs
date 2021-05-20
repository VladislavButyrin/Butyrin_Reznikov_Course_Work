using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;
using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IGuideStorage
    {
        List<GuideViewModel> GetFullList();
        List<GuideViewModel> GetFilteredList(GuideBindingModel model);
        GuideViewModel GetElement(GuideBindingModel model);
        void Insert(GuideBindingModel model);
        void Update(GuideBindingModel model);
        void Delete(GuideBindingModel model);
    }
}
