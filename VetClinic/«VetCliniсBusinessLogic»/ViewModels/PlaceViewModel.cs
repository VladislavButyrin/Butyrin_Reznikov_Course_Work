using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название услуги")]
        public string PlaceName { get; set; }
        [DisplayName("ФИО врача, оказывающего услугу")]
        public string FIO { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
        public override string ToString()
        {
            return PlaceName;
        }
    }
}
