using System;
using System.ComponentModel;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Recipient : Person, IDataErrorInfo
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Задана пустая строка имени", nameof(value));
                base.Name = value;
            }
        }

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return null;

                    case nameof(Name):
                        var name = Name;

                        if (name is null) return "Строка имени не определена";
                        if (name == "") return "Строка имени пуста";
                        if (name.Length < 3) return "Имя должно быть не менее 3 символов";
                        return null;
                }
            }
        }
    }
}
