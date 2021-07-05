using System.ComponentModel;

namespace TestLib
{
    [Description("Скрытый принтер")]
    internal class SecretPrinter : Printer
    {
        public int Counter { get; private set; }

        public SecretPrinter() : base("Internal:") { }

        public override void Print(string Message) => base.Print($"[{++Counter}]{Message}");
    }
}