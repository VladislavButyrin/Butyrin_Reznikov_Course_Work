using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название услуги")]
        public string ServiceName { get; set; }
        [DisplayName("ФИО врача, оказывающего услугу")]
        public string FIO { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Medications { get; set; }
        public override string ToString()
        {
            return ServiceName;
        }
    }
}
