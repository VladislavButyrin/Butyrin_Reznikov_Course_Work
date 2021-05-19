using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements.Modules;
using System;
using System.Linq;
using VetClinikEntitiesImplements;
using System.Collections.Generic;
using _VetCliniсBusinessLogic_.ViewModels;

namespace VetClinikEntitiesImplements.Implements
{
    public class DoctorStorage : IDoctorStorage
    {
        public void Delete(DoctorBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                Doctor element = context.Doctors.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Doctors.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public DoctorViewModel GetElement(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var component = context.Doctors
                .FirstOrDefault(rec => rec.Login == model.Login ||
               rec.Id == model.Id);
                return component != null ?
                new DoctorViewModel
                {
                    Id = component.Id,
                    Login = component.Login,
                    FIO = component.FIO,
                    Password = component.Password
                } :
               null;
            }
        }

        public List<DoctorViewModel> GetFilteredList(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                return context.Doctors
                .Where(rec => rec.FIO.Contains(model.FIO))
               .Select(rec => new DoctorViewModel
               {
                   Id = rec.Id,
                   FIO = rec.FIO,
                   Login = rec.Login,
                   Password = rec.Password
               })
                .ToList();
            }
        }

        public List<DoctorViewModel> GetFullList()
        {
            using (var context = new VetClinicDataBase())
            {
                return context.Doctors
                .Select(rec => new DoctorViewModel
                {
                    Id = rec.Id,
                    FIO = rec.FIO,
                    Login = rec.Login,
                    Password = rec.Password
                }).ToList();
            }
        }

        public void Insert(DoctorBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                context.Doctors.Add(CreateModel(model, new Doctor()));
                context.SaveChanges();
            }
        }

        public void Update(DoctorBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                var element = context.Doctors.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private Doctor CreateModel(DoctorBindingModel model, Doctor doctor)
        {
            doctor.Login = model.Login;
            doctor.Password = model.Password;
            doctor.FIO = model.FIO;
            doctor.Password = model.Password;
            return doctor;
        }
    }
}
