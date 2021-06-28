using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MailSender.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TasksOverview.Run();
            TPLOverview.Run();

            Console.WriteLine("Работа завершена. Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
