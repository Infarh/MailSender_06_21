using System;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; }

        public Sender Sender { get; set; }

        public EmailsList Recipients { get; set; }

        public Server Server { get; set; }

        public Message Message { set; get; }
    }
}
