using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MailSender.TestConsole
{
    internal static class CriticalSection
    {
        public static void Run()
        {
            var messages = Enumerable.Range(1, 100).Select(i => $"Message({i:00})").ToArray();

            var threads = new Thread[messages.Length];
            for (var i = 0; i < messages.Length; i++)
            {
                var i0 = i;
                threads[i] = new Thread(() => PrintMessage(messages[i0], 10));
            }

            //for (var i = 0; i < messages.Length; i++)
            //    threads[i].Start();

            var messages_list = new List<int>();
            //foreach (var message in messages)
            //    new Thread(
            //        () =>
            //        {
            //            var len = message.Length;
            //            messages_list.Add(len);
            //        }).Start();

            //ThreadPool.SetMinThreads(2, 2);
            //ThreadPool.SetMaxThreads(8, 8);
            foreach (var message in messages)
                ThreadPool.QueueUserWorkItem(
                    _ =>
                    {
                        var len = message.Length;
                        lock (messages_list)
                            messages_list.Add(len);
                        Console.WriteLine("{0} - {1}", Thread.CurrentThread.ManagedThreadId, message);
                    });
            Console.ReadLine();
        }

        private static object __SyncRoot = new object();
        private static void PrintMessage(string Message, int Count, int Timeout = 250)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            for (var i = 1; i <= Count; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write(thread_id.ToString("00"));
                    Console.Write(" - ");
                    Console.Write(i.ToString("00"));
                    Console.Write(" ");
                    Console.WriteLine(Message);
                }

                //Monitor.Enter(__SyncRoot);
                //try
                //{
                //    Console.Write(thread_id.ToString("00"));
                //    Console.Write(" - ");
                //    Console.Write(i.ToString("00"));
                //    Console.Write(" ");
                //    Console.WriteLine(Message);
                //}
                //finally
                //{
                //    Monitor.Exit(__SyncRoot);
                //}

                Thread.Sleep(Timeout);
                //Thread.SpinWait(100);
            }
            Console.WriteLine("{0:00} - Print message {1} completed", thread_id, Message);
        }
    }
}
