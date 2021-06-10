using System.Windows;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void Exit_OnClick(object Sender, RoutedEventArgs E)
        {
            Close();
        }
    }
}
