using System;
using System.Threading;

namespace MailSender.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadsIntro.Run();
            CriticalSection.Run();

            Console.WriteLine("Работа завершена. Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
