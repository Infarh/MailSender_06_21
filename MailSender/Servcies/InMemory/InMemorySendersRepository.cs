using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemorySendersRepository : InMemoryPersonsRepository<Sender>
    {
        private static IEnumerable<Sender> GetTestData(int Count = 10) => Enumerable.Range(1, Count)
           .Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель {i}",
                Address = $"sender-{i}@server.ru",
                Description = $"Описание отправителя {i}"
            });

        public InMemorySendersRepository() : base(GetTestData()) { }
    }
}