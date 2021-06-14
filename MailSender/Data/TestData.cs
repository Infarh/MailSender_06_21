using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Data
{
    public class TestData
    {
        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
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
}
