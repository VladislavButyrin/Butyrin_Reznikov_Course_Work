using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
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
