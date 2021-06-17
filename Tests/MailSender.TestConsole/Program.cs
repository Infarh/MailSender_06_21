using System;
using System.Net;
using System.Net.Mail;
using MailSender.Models;


namespace MailSender.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sender sender = new Sender
            //{
            //    Name = "2waeq",
            //    Description = "asdas",
            //    Address = "asdasd",
            //    Id = 123
            //};

            using var message = new MailMessage("shmachiline@yandex.ru", "shmachilin@gmail.com");

            using var client = new SmtpClient("smtp.yandex.ru", 465);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential
            {
                UserName = "shmachilin",
                Password = "123"
            };

            client.Send(message);
        }
    }
}
