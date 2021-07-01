using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Servcies.InMemory
{
    public class InMemorySchedulerTasksRepository : InMemoryRepository<SchedulerTask>
    {
        private static IEnumerable<SchedulerTask> GetTestData(int Count = 100) => Enumerable.Empty<SchedulerTask>();

        public InMemorySchedulerTasksRepository() : base(GetTestData()) { }

        public override void Update(SchedulerTask item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Message = item.Message;
            db_item.Recipients = item.Recipients;
            db_item.Sender = item.Sender;
            db_item.Server = item.Server;
            db_item.Time = item.Time;
        }
    }
}