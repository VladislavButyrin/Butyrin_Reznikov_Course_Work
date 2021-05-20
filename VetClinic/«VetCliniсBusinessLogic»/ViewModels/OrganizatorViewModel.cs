using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class OrganizatorViewModel
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string FIO { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return FIO;
        }
    }
}
