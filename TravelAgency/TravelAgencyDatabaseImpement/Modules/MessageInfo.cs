using System;
using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfo
    {
        [Key]
        public string MessageId { get; set; }
        public int? DoctorId { get; set; }
        public string SenderName { get; set; }
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
