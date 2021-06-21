using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemoryServersRepository : InMemoryRepository<Server>
    {
        private static IEnumerable<Server> GetTestData(int Count = 10) => Enumerable.Range(1, Count)
           .Select(i => new Server
            {
                Id = i,
                Name = $"Сервер {i}",
                Address = $"smtp.server-{i}.ru",
                Login = $"User-{i}",
                Password = $"Password - {i}",
                UseSSL = i % 2 == 0
            });

        public InMemoryServersRepository() : base(GetTestData()) { }

        public override void Update(Server item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if(db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.UseSSL = item.UseSSL;
            db_item.Login = item.Login;
            db_item.Password = item.Password;
        }
    }
}
