using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.TestWPF
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        //private void Start_OnClick(object Sender, RoutedEventArgs E)
        //{
        //    var thread_id = Thread.CurrentThread.ManagedThreadId;

        //    new Thread(() => Calculate()) { IsBackground = true }.Start();
        //}

        private void Cancel_OnClick(object Sender, RoutedEventArgs E)
        {
            _Cancellation?.Cancel();
        }

        private CancellationTokenSource _Cancellation;
        private async void Start_OnClick(object Sender, RoutedEventArgs E)
        {
            var button = (Button) Sender;
            button.IsEnabled = false;
            CancelButton.IsEnabled = true;

            var cancellation = new CancellationTokenSource();
            _Cancellation = cancellation;

            var progress = new Progress<double>(p => Progress.Value = p * 100);
            try
            {
                var result = await CalculateAsync(Progress: progress, Cancel: cancellation.Token);
                Result.Text = result.ToString();
            }
            catch (OperationCanceledException)
            {
                Result.Text = "Отмена!";
                Progress.Value = 0;
            }

            button.IsEnabled = true;
            CancelButton.IsEnabled = false;
        }

        private static async Task<int> CalculateAsync(int Max = 100, int Timeout = 100, IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            //var thread_id = Thread.CurrentThread.ManagedThreadId;

            await Task.Delay(Timeout, Cancel).ConfigureAwait(false);

            //var thread2_id = Thread.CurrentThread.ManagedThreadId;

            var s = 0;
            for (var i = 1; i <= Max && !Cancel.IsCancellationRequested; i++)
            {
                s += i;
                Progress?.Report((double)i / Max);
                await Task.Delay(Timeout, Cancel).ConfigureAwait(false);
            }
            Cancel.ThrowIfCancellationRequested();

            Progress?.Report(1);
            return s;
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
