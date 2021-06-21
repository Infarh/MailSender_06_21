using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Message : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
