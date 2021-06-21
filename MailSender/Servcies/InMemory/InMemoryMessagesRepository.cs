using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemoryMessagesRepository : InMemoryRepository<Message>
    {
        private static IEnumerable<Message> GetTestData(int Count = 100) => Enumerable.Range(1, Count)
           .Select(i => new Message
            {
                Id = i,
                Title = $"Сообщение {i}",
                Text = $"Текст сообщения {i}",
            });

        public InMemoryMessagesRepository() : base(GetTestData()) { }

        public override void Update(Message item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Title = item.Title;
            db_item.Text = item.Text;
        }
    }
}
