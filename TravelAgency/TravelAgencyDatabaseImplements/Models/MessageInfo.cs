using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgencyDatabaseImplements.Models
{
    public class MessageInfo
    {
        [Key]
        public string MessageId { get; set; }
        public int? OrganizatorId { get; set; }
        public string SenderName { get; set; }
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Organizator Organizator { get; set; }

    }
}
