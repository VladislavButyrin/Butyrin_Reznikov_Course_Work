using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class MessageInfoBindingModel
    {
        [DataMember]
        public int? OrganizatorId { get; set; }
        [DataMember]
        public string MessageId { get; set; }
        [DataMember]
        public string FromMailAddress { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public DateTime DateDelivery { get; set; }
    }
}
