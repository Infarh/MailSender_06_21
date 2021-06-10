using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailSender.TestWPF
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            using var message = new MailMessage("shmachilin@yandex.ru", "shmachilin@gmail.com");
            message.Subject = "Тестовое сообщение от " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
            message.Body = "Тело тестового сообщения " + DateTime.Now.ToString("F");

            //message.Attachments.Add(new Attachment());

            using var client = new SmtpClient("smtp.yandex.ru", 25)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential
                {
                    UserName = LoginEdit.Text,
                    SecurePassword = PasswordEdit.SecurePassword,
                }
            };

            try
            {
                client.Send(message);
                MessageBox.Show("Почта успешно отправлена", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SmtpException smtp_exception)
            {
                MessageBox.Show(smtp_exception.Message, "Ошибка при отправке почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
