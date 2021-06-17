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

        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
           .Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель - {i}",
                Address = $"sender-{i}.server.ru",
                Description = $"Описание отправителя {i}",
            })
           .ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
           .Select(i => new Recipient
            {
                Id = i,
                Name = $"Получатель - {i}",
                Address = $"recipient-{i}.server.ru",
                Description = $"Описание получателя {i}"
            })
           .ToList();

        public static List<Message> Messages { get; } = Enumerable.Range(1, 100)
           .Select(i => new Message
            {
                Title = $"Сообщение {i}",
                Text = $"Текст сообщения {i}"
            })
           .ToList();
    }
}
