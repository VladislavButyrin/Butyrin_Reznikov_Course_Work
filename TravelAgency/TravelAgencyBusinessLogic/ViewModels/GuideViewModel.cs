
namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public override string ToString()
        {
            return GuideName;
        }
    }
}