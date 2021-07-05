using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestLib
{
    public class Printer
    {
        private string _Prefix;

        public Printer([Required] string Prefix) => _Prefix = Prefix;

        [Description("Метод печати")]
        public virtual void Print([Description("Печатаемое сообщение")] string Message)
        {
            Console.WriteLine("{0}{1}", _Prefix, Message);
        }
    }
}
