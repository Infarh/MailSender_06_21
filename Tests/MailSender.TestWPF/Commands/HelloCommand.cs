using System;
using System.Windows;
using System.Windows.Input;

namespace MailSender.TestWPF.Commands
{
    public class HelloCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private int _Counter = 3;

        public bool CanExecute(object parameter) => _Counter > 0;

        public void Execute(object parameter)
        {
            _Counter--;
            MessageBox.Show("Всем привет!");
        }
    }
}
