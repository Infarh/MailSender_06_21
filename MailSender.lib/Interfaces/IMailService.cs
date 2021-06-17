namespace MailSender.Interfaces
{
    public interface IMailService
    {
        void SendEmail(string From, string To, string Title, string Body);
    }
}
