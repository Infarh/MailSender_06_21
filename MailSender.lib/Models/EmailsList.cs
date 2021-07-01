using System.Collections.Generic;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class EmailsList : Entity
    {
        public ICollection<Recipient> Recipients { get; set; } = new HashSet<Recipient>();
    }
}
