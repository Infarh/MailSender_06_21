using System;
using MailSender.Interfaces;
using MailSender.Reports;

namespace MailSender.Services
{
    public class OpenXMLReportService : IReportService
    {
        private readonly IStatistic _Statistic;

        public OpenXMLReportService(IStatistic Statistic) => _Statistic = Statistic;

        public void CreateStatisticReport(string FileName)
        {
            var report = new StatisticReport
            {
                UserName = Environment.UserName,
                ServersCount = _Statistic.ServersCount,
                MailSendedCount = _Statistic.SendedMailsCount,
            };

            report.CreatePackage(FileName);
        }
    }
}
