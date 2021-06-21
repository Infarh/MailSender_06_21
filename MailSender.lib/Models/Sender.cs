using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Sender : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
