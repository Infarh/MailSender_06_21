using System.Collections.Generic;
using MailSender.Models.Base;

namespace MailSender.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();

        T GetById(int Id);

        int Add(T item);

        void Update(T item);

        bool Remove(int id);
    }
}
