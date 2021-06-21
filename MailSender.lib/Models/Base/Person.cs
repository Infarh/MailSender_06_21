namespace MailSender.Models.Base
{
    public abstract class Person : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
