using System.ComponentModel;

namespace MailSender.TestWPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Title = "Hello World!";

        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }
    }
}
