using System;

namespace MailSender.Interfaces
{
    public interface IStatistic
    {
        int SendedMailsCount { get; }
        event EventHandler SendedMailsCountChanged;

        int SendersCount { get; }

        int RecipientsCount { get; }

        int ServersCount { get; }

        TimeSpan UpTime { get; }

        void MessageSended();
    }
}
