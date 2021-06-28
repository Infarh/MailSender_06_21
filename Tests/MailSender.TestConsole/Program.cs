using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MailSender.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TasksOverview.Run();
            //TPLOverview.Run();
            //TasksOverview.RunAsync().Wait();
            //await TasksOverview.RunAsync();
            await ReadingFileTest.RunAsync();

            Console.WriteLine("Работа завершена. Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
