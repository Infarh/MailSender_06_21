using System;
using System.Threading;
using System.Windows;

namespace MailSender.TestWPF
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void Start_OnClick(object Sender, RoutedEventArgs E)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            new Thread(() => Calculate()) { IsBackground = true }.Start();
        }

        private void Calculate(int Max = 100, int Timeout = 100)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            var dispatcher = Application.Current.Dispatcher;

            var s = 0;
            for (var i = 1; i <= Max; i++)
            {
                //Application.Current.Dispatcher.BeginInvoke(new Action(
                dispatcher.BeginInvoke(new Action(
                    () =>
                    {
                        Progress.Value = i;
                    }));

                s += i;
                Thread.Sleep(Timeout);
            }

            //Result.Dispatcher.Invoke(
            dispatcher.Invoke(
                () =>
                {
                    Result.Text = s.ToString();
                });
            
        }
    }
}
