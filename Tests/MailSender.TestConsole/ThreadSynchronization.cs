using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.TestConsole
{
    internal static class ThreadSynchronization
    {
        public static void Run()
        {
            var messages = Enumerable.Range(1, 50).Select(i => $"Message({i:00})").ToArray();

            var manual_reset_event = new ManualResetEvent(false);
            var auto_reset_event = new AutoResetEvent(false);

            EventWaitHandle handle = auto_reset_event;

            var messages_list = new List<int>();
            var threads = new Thread[messages.Length];
            for (var i = 0; i < threads.Length; i++)
            {
                var i0 = i;
                threads[i] = new Thread(() =>
                {
                    var msg = messages[i0];
                    Console.WriteLine("Начало обработки сообщения {0} в потоке {1}",
                        msg, Thread.CurrentThread.ManagedThreadId);
                    handle.WaitOne();

                    lock (messages_list)
                        messages_list.Add(messages[i0].Length);

                    Console.WriteLine("Обработка сообщения {0} в потоке {1} завершено",
                        msg, Thread.CurrentThread.ManagedThreadId);
                });
                threads[i].Start();
                Thread.Sleep(100);
            }

            Console.WriteLine("Потоки созданы и готовы к работе. Для запуска нажмите Enter");
            Console.ReadLine();

            handle.Set();

            //Console.ReadLine();
            //handle.Set();
            //Console.ReadLine();
            //handle.Set();
            //Console.ReadLine();
            //handle.Set();
            //Console.ReadLine();

            Mutex mutex = new Mutex(true, "Mutex name", out var is_mutex_first_created);
            Semaphore semaphore = new Semaphore(0, 10, "Semaphore name", out var is_semaphore_first_created);

            semaphore.WaitOne();
            // Критическая секция в которой может находиться только 10 потоков одновременно
            semaphore.Release();
        }

    }
}
