using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyDatabaseImplements.Models;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDataBase())
            {
                return context.MessageInfos
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
               .ToList();
            }
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDataBase())
            {
                return context.MessageInfos
                .Where(rec => (model.OrganizatorId.HasValue && rec.OrganizatorId ==
               model.OrganizatorId) ||
                (!model.OrganizatorId.HasValue && rec.DateDelivery.Date ==
               model.DateDelivery.Date))
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
               .ToList();
            }
        }
        public void Insert(MessageInfoBindingModel model)
        {
            using (var context = new TravelAgencyDataBase())
            {
                MessageInfo element = context.MessageInfos.FirstOrDefault(rec =>
               rec.MessageId == model.MessageId);
                if (element != null)
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
                context.MessageInfos.Add(new MessageInfo
                {
                    MessageId = model.MessageId,
                    OrganizatorId = model.OrganizatorId,
                    SenderName = model.FromMailAddress,
                    DateDelivery = model.DateDelivery,
                    Subject = model.Subject,
                    Body = model.Body
                });
                context.SaveChanges();
            }
        }
    }
}
