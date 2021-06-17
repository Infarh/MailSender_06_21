using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Models;

namespace MailSender.Servcies
{
    public class ServersRepository
    {
        private List<Server> _Servers;

        public ServersRepository()
        {
            _Servers = Enumerable.Range(1, 10)
               .Select(i => new Server
                {
                    Name = $"Сервер {i}",
                    Address = $"smtp.server-{i}.ru",
                    Login = $"User-{i}",
                    Password = $"Password - {i}",
                    UseSSL = i % 2 == 0
                })
               .ToList();
        }

        public IEnumerable<Server> GetAll() => _Servers;

        public Server Create(string Name, string Address, int Port, bool UseSSL, string Login, string Password)
        {
            var server = new Server
            {
                Name = Name,
                Address = Address,
                Port = Port,
                UseSSL = UseSSL,
                Login = Login,
                Password = Password,
            };
            Add(server);
            return server;
        }

        public void Add(Server server)
        {
            _Servers.Add(server);
        }

        public void Remove(Server server)
        {
            _Servers.Remove(server);
        }
    }
}
