using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemoryEmailsListsRepository : InMemoryRepository<EmailsList>
    {
        private static IEnumerable<EmailsList> GetTestData(int Count = 100) => Enumerable.Empty<EmailsList>();

        public InMemoryEmailsListsRepository() : base(GetTestData()) { }

        public override void Update(EmailsList item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Recipients = item.Recipients;
        }
    }
}