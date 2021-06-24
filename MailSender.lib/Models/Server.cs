using System.ComponentModel;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Server : NamedEntity//, INotifyPropertyChanged
    {
        public string Address { get; set; }

        public int Port { get; set; } = 25;

        public bool UseSSL { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public override string ToString() => $"{Name}:{Port}";
    }
}
