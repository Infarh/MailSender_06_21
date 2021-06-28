using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.TestConsole
{
    internal static class TasksOverview
    {
        public static void Run()
        {
            new Thread(
                () =>
                {
                    Console.Title = DateTime.Now.ToLongTimeString();
                    Thread.Sleep(100);
                })
            { IsBackground = true }
               .Start();

            Action<string> printer = str =>
            {
                Console.WriteLine("Сообщение из потока id:{0} - {1}",
                    Thread.CurrentThread.ManagedThreadId, str);
                Thread.Sleep(3000);
                Console.WriteLine("Печать сообщения {1} в потоке {0} завершена",
                    Thread.CurrentThread.ManagedThreadId, str);
            };

            //printer("Hello world!");
            //printer.Invoke("123");

            //printer.BeginInvoke(
            //    "Async call", 
            //    result => Console.WriteLine("Будет напечатано после завершения основного вызова - {0}", result.AsyncState), 
            //    "result.AsyncState");

            //var worker = new BackgroundWorker();
            //worker.DoWork += (s, e) =>
            //{
            //    Console.WriteLine("Запуск асинхронного процесса");
            //    var w = (BackgroundWorker)s;
            //    w.ReportProgress(50);
            //    e.Result = "Completed";
            //};
            //worker.ProgressChanged += (_, e) => Console.WriteLine("Изменился прогресс асинхронной операции {0}", e.ProgressPercentage);
            //worker.RunWorkerCompleted += (s, e) =>
            //{
            //    Console.WriteLine("Действие при завершении асинхронного процесса - {0}", e.Result);
            //};
            //worker.RunWorkerAsync();

            var task = Task.Run(() => printer("Hello world"));

            var calculate_task = new Task<int>(
                () =>
                {
                    printer("Calculate 42");
                    //throw new Exception("43");
                    return 42;
                });

            var continue_task = calculate_task.ContinueWith(
                t =>
                {
                    Console.WriteLine("Код, Выполняемый по завершении основной задачи - значение результата {0}", t.Result);
                });
            //continue_task.ContinueWith().ContinueWith().ContinueWith()...

            calculate_task.Start();

            calculate_task.Wait(); // Можно, но является признаком дурного тона!!!!!!!!!!!11111

            Console.ReadLine();

            Console.WriteLine();

            try
            {
                var result = calculate_task.Result; // Можно, но является признаком дурного тона!!!!!!!!!!!11111
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
