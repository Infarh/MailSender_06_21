namespace MailSender.Models.Base
{
    public abstract class Person : NamedEntity
    {
        public string Address { get; set; }

        public string Description { get; set; }
    }
}
