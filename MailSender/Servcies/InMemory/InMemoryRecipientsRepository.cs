using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemoryRecipientsRepository : InMemoryPersonsRepository<Recipient>
    {
        private static IEnumerable<Recipient> GetTestData(int Count = 10) => Enumerable.Range(1, Count)
           .Select(i => new Recipient
            {
                Id = i,
                Name = $"Получатель {i}",
                Address = $"recipient-{i}@server.ru",
                Description = $"Описание получателя {i}"
            });

        public InMemoryRecipientsRepository() : base(GetTestData()) { }
    }
}