using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImpement.Implements
{
    public class UserStorage: IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Users
                .Select(rec => new UserViewModel
                {
                    Id = rec.Id,
                    UserName = rec.UserName
                })
               .ToList();
            }
        }
        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Users
                .Where(rec => rec.UserName.Contains(model.UserName))
               .Select(rec => new UserViewModel
               {
                   Id = rec.Id,
                   UserName = rec.UserName
               })
                .ToList();
            }
        }
        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var user = context.Users
                .FirstOrDefault(rec => rec.UserName == model.UserName ||
               rec.Id == model.Id);
                return user != null ?
                new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                } :
               null;
            }
        }
        public void Insert(UserBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                context.Users.Add(CreateModel(model, new User()));
                context.SaveChanges();
            }
        }
        public void Update(UserBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var element = context.Users.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(UserBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private User CreateModel(UserBindingModel model, User user)
        {
            user.UserName = model.UserName;
            return user;
        }
    }
}
