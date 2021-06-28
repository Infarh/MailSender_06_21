using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.TestConsole
{
    internal static class TPLOverview
    {
        public static void Run()
        {
            var opt = new ParallelOptions { MaxDegreeOfParallelism = 2 };

            //var methods = Enumerable.Repeat(new Action(ParallelInvokeMethod), 100).ToArray();
            //Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 2 }, methods);

            //Parallel.Invoke(
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("123"));

            //Parallel.For(0, 100, opt, i => ParallelInvokeMethod($"Message - {i}"));
            //var result = Parallel.For(0, 100, (i, state) =>
            //{
            //    if(i >= 20)
            //        state.Break();
            //    ParallelInvokeMethod($"Message - {i}");
            //});
            //Console.WriteLine(result.LowestBreakIteration);

            var rnd = new Random(100);
            var messages = Enumerable
               .Range(1, 100)
               .Select(i => $"Message = {i}: {new string('*', rnd.Next(5, 31))}")
               .ToArray();

            //Parallel.ForEach(messages, opt, msg => ParallelInvokeMethod(msg));
            //var foreach_result = Parallel.ForEach(messages, (msg, state) =>
            //{
            //    if(msg.EndsWith("36")) state.Break();
            //    ParallelInvokeMethod(msg);
            //});
            //Console.WriteLine(foreach_result.LowestBreakIteration);

            static int GetMessageLength(string msg)
            {
                Thread.Sleep(200);
                return msg.Length;
            }

            var timer = Stopwatch.StartNew();
            var messages_sum = messages
               .AsParallel()
               .WithDegreeOfParallelism(10)
               .Select(GetMessageLength)
               .AsSequential()
               .Sum();

            var result = timer.Elapsed;
        }

        private static void ParallelInvokeMethod()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("ThreadId:{0} - started", thread_id);
            Thread.Sleep(250);
            Console.WriteLine("ThreadId:{0} - completed", thread_id);
        }

        private static void ParallelInvokeMethod(string Message)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("ThreadId:{0} - started :: {1}", thread_id, Message);
            Thread.Sleep(250);
            Console.WriteLine("ThreadId:{0} - completed :: {1}", thread_id, Message);
        }
    }
}
