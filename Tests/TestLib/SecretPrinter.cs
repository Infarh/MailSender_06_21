using System.ComponentModel;

namespace TestLib
{
    [Description("Скрытый принтер")]
    internal class SecretPrinter : Printer
    {
        private int _Counter;

        public SecretPrinter() : base("Internal:") { }

        public override void Print(string Message) => base.Print($"[{++_Counter}]{Message}");
    }
}