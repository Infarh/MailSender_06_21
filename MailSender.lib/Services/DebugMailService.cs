using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using MailSender.Interfaces;

namespace MailSender.Services
{
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(string ServerAddress, int Port, bool UseSSL, string Login, string Password)
        {
            return new Sender(ServerAddress, Port, UseSSL, Login, Password);
        }

        private class Sender : IMailSender
        {
            private readonly string _ServerAddress;
            private readonly int _Port;
            private readonly bool _UseSsl;
            private readonly string _Login;
            private readonly string _Password;

            public Sender(string ServerAddress, int Port, bool UseSSL, string Login, string Password)
            {
                _ServerAddress = ServerAddress;
                _Port = Port;
                _UseSsl = UseSSL;
                _Login = Login;
                _Password = Password;
            }

            public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
            {
                Debug.WriteLine($"Отправка почты через {_ServerAddress}:{_Port} ssl:{_UseSsl}\r\n\t"
                    + $"from {SenderAddress} to {RecipientAddress}\r\n\t"
                    + $"msg ({Subject}):{Body}");
            }

            public void Send(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
            {
                foreach (var recipients_address in RecipientsAddresses)
                    Send(SenderAddress, recipients_address, Subject, Body);
            }

            public void SendParallel(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
            {
                foreach (var recipient_address in RecipientsAddresses)
                    //ThreadPool.QueueUserWorkItem(_ => Send(SenderAddress, recipient_address, Subject, Body));
                    ThreadPool.QueueUserWorkItem(p =>
                            Send((string)((object[])p)[0], (string)((object[])p)[1], (string)((object[])p)[2], (string)((object[])p)[3]),
                        new[] { SenderAddress, recipient_address, Subject, Body });
            }

            public Task SendAsync(
                string SenderAddress, 
                string RecipientAddress, 
                string Subject, string Body, 
                CancellationToken Cancel = default)
            {
                Debug.WriteLine("Отправка почты ... асинхронно");
                return Task.CompletedTask;
            }

            public Task SendAsync(
                string SenderAddress,
                IEnumerable<string> RecipientsAddresses, 
                string Subject, string Body,
                CancellationToken Cancel = default)
            {
                Debug.WriteLine("Отправка почты ... асинхронно последовательно по списку получателей");
                return Task.CompletedTask;
            }

            public Task SendParallelAsync(
                string SenderAddress, 
                IEnumerable<string> RecipientsAddresses,
                string Subject, string Body, 
                CancellationToken Cancel = default)
            {
                Debug.WriteLine("Отправка почты ... асинхронно параллельно по списку получателей");
                return Task.CompletedTask;
            }
        }
    }
}
