using MailSender.Models;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Data
{
    public class MailSenderDB : DbContext
    {
        public DbSet<Server> Servers { get; set; }

        public DbSet<Sender> Senders { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<EmailsList> EmailsLists { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SchedulerTask> SchedulerTasks { get; set; }

        public MailSenderDB(DbContextOptions<MailSenderDB> options) : base(options) { }
    }
}
