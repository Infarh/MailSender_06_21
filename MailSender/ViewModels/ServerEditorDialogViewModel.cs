using System;
using System.Windows.Input;
using MailSender.Commands;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    public class ServerEditorDialogViewModel : ViewModel
    {
        public event EventHandler EditCompleted;
        public event EventHandler EditCanceled;

        #region Command OkCommand - Ok

        /// <summary>Ok</summary>
        private LambdaCommand _OkCommand;

        /// <summary>Ok</summary>
        public ICommand OkCommand => _OkCommand ??= new(OnOkCommandExecuted);

        /// <summary>Логика выполнения - Ok</summary>
        private void OnOkCommandExecuted(object p)
        {
            EditCompleted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Command CancelCommand - Cancel

        /// <summary>Cancel</summary>
        private LambdaCommand _CancelCommand;

        /// <summary>Cancel</summary>
        public ICommand CancelCommand => _CancelCommand ??= new(OnCancelCommandExecuted);

        /// <summary>Логика выполнения - Cancel</summary>
        private void OnCancelCommandExecuted(object p)
        {
            EditCanceled?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Name : string - Имя сервера

        /// <summary>Имя сервера</summary>
        private string _Name;

        /// <summary>Имя сервера</summary>
        public string Name { get => _Name; set => Set(ref _Name, value); }

        #endregion

        #region Address : string - Адрес сервера

        /// <summary>Адрес сервера</summary>
        private string _Address;

        /// <summary>Адрес сервера</summary>
        public string Address { get => _Address; set => Set(ref _Address, value); }

        #endregion

        #region Port : int - Порт сервера

        /// <summary>Порт сервера</summary>
        private int _Port;

        /// <summary>Порт сервера</summary>
        public int Port { get => _Port; set => Set(ref _Port, value); }

        #endregion

        #region UseSSL : bool - SSL

        /// <summary>SSL</summary>
        private bool _UseSSL;

        /// <summary>SSL</summary>
        public bool UseSSL { get => _UseSSL; set => Set(ref _UseSSL, value); }

        #endregion

        #region Login : string - Имя пользователя

        /// <summary>Имя пользователя</summary>
        private string _Login;

        /// <summary>Имя пользователя</summary>
        public string Login { get => _Login; set => Set(ref _Login, value); }

        #endregion

        #region Password : string - Пароль

        /// <summary>Пароль</summary>
        private string _Password;

        /// <summary>Пароль</summary>
        public string Password { get => _Password; set => Set(ref _Password, value); }

        #endregion
    }
}
