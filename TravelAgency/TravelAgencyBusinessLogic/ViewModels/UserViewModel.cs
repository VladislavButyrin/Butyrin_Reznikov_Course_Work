using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class UserViewModel
    {
        public int? Id { set; get; }
        [DisplayName("Имя пользователя")]
        public string UserName { set; get; }
        [DisplayName("Пароль пользователя")]
        public string Password { set; get; }
        [DisplayName("Почта пользователя")]
        public string Email { set; get; }
    }
}
