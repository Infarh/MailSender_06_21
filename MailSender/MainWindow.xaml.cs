using System.Windows;
using MailSender.Data;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ServersList.ItemsSource = TestData.Servers;
        }

        private void Exit_OnClick(object Sender, RoutedEventArgs E)
        {
            Close();
        }

        private void About_OnClick(object Sender, RoutedEventArgs E) => 
            MessageBox.Show("Рассыльщик почты", "О программе", MessageBoxButton.OK);
    }
}
