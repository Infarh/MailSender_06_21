using System;
using System.Collections.Generic;
using MailSender.Models.Base;

namespace MailSender.Servcies.InMemory
{
    public abstract class InMemoryPersonsRepository<T> : InMemoryRepository<T> where T : Person
    {
        protected InMemoryPersonsRepository(IEnumerable<T> items) : base(items) { }

        public override void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Description = item.Description;
        }
    }
}