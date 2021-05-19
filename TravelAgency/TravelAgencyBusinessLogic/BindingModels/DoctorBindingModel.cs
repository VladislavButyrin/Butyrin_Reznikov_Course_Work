using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class DoctorBindingModel
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
    }
}
