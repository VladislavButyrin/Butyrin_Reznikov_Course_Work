using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyEntitiesImplements.Modules;
using System;
using System.Linq;
using TravelAgencyEntitiesImplements;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyEntitiesImplements.Implements
{
    public class OrganizatorStorage : IOrganizatorStorage
    {
        public void Delete(OrganizatorBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                Organizator element = context.Organizators.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Organizators.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public OrganizatorViewModel GetElement(OrganizatorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                var component = context.Organizators
                .FirstOrDefault(rec => rec.Login == model.Login ||
               rec.Id == model.Id);
                return component != null ?
                new OrganizatorViewModel
                {
                    Id = component.Id,
                    Login = component.Login,
                    FIO = component.FIO,
                    Password = component.Password
                } :
               null;
            }
        }

        public List<OrganizatorViewModel> GetFilteredList(OrganizatorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                return context.Organizators
                .Where(rec => rec.FIO.Contains(model.FIO))
               .Select(rec => new OrganizatorViewModel
               {
                   Id = rec.Id,
                   FIO = rec.FIO,
                   Login = rec.Login,
                   Password = rec.Password
               })
                .ToList();
            }
        }

        public List<OrganizatorViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.Organizators
                .Select(rec => new OrganizatorViewModel
                {
                    Id = rec.Id,
                    FIO = rec.FIO,
                    Login = rec.Login,
                    Password = rec.Password
                }).ToList();
            }
        }

        public void Insert(OrganizatorBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                context.Organizators.Add(CreateModel(model, new Organizator()));
                context.SaveChanges();
            }
        }

        public void Update(OrganizatorBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                var element = context.Organizators.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private Organizator CreateModel(OrganizatorBindingModel model, Organizator organizator)
        {
            organizator.Login = model.Login;
            organizator.Password = model.Password;
            organizator.FIO = model.FIO;
            organizator.Password = model.Password;
            return organizator;
        }
    }
}
