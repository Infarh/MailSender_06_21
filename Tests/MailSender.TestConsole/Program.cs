using System;
using System.Net;
using System.Net.Mail;


namespace MailSender.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using var message = new MailMessage("shmachilin@yandex.ru", "shmachilin@gmail.com");

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
