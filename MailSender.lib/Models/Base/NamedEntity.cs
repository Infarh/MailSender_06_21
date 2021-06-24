using System;

namespace MailSender.Models.Base
{
    public abstract class NamedEntity : Entity
    {
        private string _Name;
        public virtual string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                NameChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler NameChanged;
    }
}
