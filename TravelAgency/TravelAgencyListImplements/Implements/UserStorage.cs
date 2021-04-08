using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyListImplement;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplements.Implements
{
    public class UserStorage: IUserStorage
    {
        private readonly DataListSingleton source;
        public UserStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<UserViewModel> GetFullList()
        {
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in source.Users)
            {
                result.Add(CreateModel(user));
            }
            return result;
        }
        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var User in source.Users)
            {
                if (User.UserName.Contains(model.UserName))
                {
                    result.Add(CreateModel(User));
                }
            }
            return result;
        }
        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var User in source.Users)
            {
                if (User.Id == model.Id || User.UserName ==
               model.UserName)
                {
                    return CreateModel(User);
                }
            }
            return null;
        }
        public void Insert(UserBindingModel model)
        {
            User tempUser = new User { Id = 1 };
            foreach (var user in source.Users)
            {
                if (user.Id >= tempUser.Id)
                {
                    tempUser.Id = user.Id + 1;
                }
            }
            source.Users.Add(CreateModel(model, tempUser));
        }
        public void Update(UserBindingModel model)
        {

            User tempUser = null;
            foreach (var User in source.Users)
            {
                if (User.Id == model.Id)
                {
                    tempUser = User;
                }
            }
            if (tempUser == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempUser);
        }
        public void Delete(UserBindingModel model)
        {
            for (int i = 0; i < source.Users.Count; ++i)
            {
                if (source.Users[i].Id == model.Id.Value)
                {
                    source.Users.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private User CreateModel(UserBindingModel model, User user)
        {
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Password = model.Password;
            return user;
        }
        private UserViewModel CreateModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email=user.Email
            };
        }
    }
}
