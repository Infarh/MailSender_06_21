using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Interfaces;
using MailSender.Models;

namespace MailSender.Services
{
    public class MailSchedulerTPL : IMailScheduler
    {
        private readonly IMailService _MailService;
        private readonly IRepository<SchedulerTask> _Tasks;
        private Task _WaitTask;
        private CancellationTokenSource _Cancellation;

        public MailSchedulerTPL(IMailService MailService, IRepository<SchedulerTask> Tasks)
        {
            _MailService = MailService;
            _Tasks = Tasks;
        }

        public void Start()
        {
            var cancellation = new CancellationTokenSource();
            Interlocked.Exchange(ref _Cancellation, cancellation)?.Cancel();

            var first_task = _Tasks.GetAll()
               .Where(task => task.Time > DateTime.Now)
               .OrderBy(task => task.Time)
               .FirstOrDefault();

            _WaitTask = null;
            if (first_task is null)
                return;

            _WaitTask = WaitTaskAndRunAsync(first_task, cancellation.Token);
        }

        private async Task WaitTaskAndRunAsync(SchedulerTask task, CancellationToken Cancel)
        {
            Cancel.ThrowIfCancellationRequested();

            var time = task.Time;
            var delta = time.Subtract(DateTime.Now);

            if (delta.TotalMilliseconds > 25)
                await Task.Delay(delta, Cancel).ConfigureAwait(false);

            Cancel.ThrowIfCancellationRequested();

            await ExecuteTaskAsync(task, Cancel).ConfigureAwait(false);

            _Tasks.Remove(task.Id);

            await Task.Run(Start, Cancel).ConfigureAwait(false);
        }

        private async Task ExecuteTaskAsync(SchedulerTask task, CancellationToken Cancel)
        {
            Cancel.ThrowIfCancellationRequested();

            var server = task.Server;
            var sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            await sender.SendAsync(
                task.Sender.Address, 
                task.Recipients.Recipients.Select(recipient => recipient.Address), 
                task.Message.Title, 
                task.Message.Text,
                Cancel).ConfigureAwait(false);
        }

        public void Stop()
        {
            _Cancellation?.Cancel();
            _WaitTask = null;
        }

        public SchedulerTask AddToPlan(
            DateTime Time,
            Sender Sender,
            EmailsList Recipients,
            Server Server,
            Message Message)
        {

            var task = new SchedulerTask
            {
                Time = Time,
                Sender = Sender,
                Recipients = Recipients,
                Server = Server,
                Message = Message
            };

            throw new NotImplementedException();
        }
    }
}
