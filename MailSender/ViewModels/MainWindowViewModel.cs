using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using MailSender.Commands;
using MailSender.Interfaces;
using MailSender.Models;
using MailSender.Servcies;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IRepository<Server> _ServersRepository;
        private readonly IRepository<Sender> _SendersRepository;
        private readonly IRepository<Recipient> _RecipientsRepository;
        private readonly IRepository<Message> _MessagesRepository;
        private readonly IMailService _MailService;
        private readonly IStatistic _Statistic;

        public MainWindowViewModel(
            IUserDialog UserDialog,
            IRepository<Server> ServersRepository,
            IRepository<Sender> SendersRepository,
            IRepository<Recipient> RecipientsRepository,
            IRepository<Message> MessagesRepository,
            IMailService MailService,
            IStatistic Statistic)
        {
            _UserDialog = UserDialog;
            _ServersRepository = ServersRepository;
            _SendersRepository = SendersRepository;
            _RecipientsRepository = RecipientsRepository;
            _MessagesRepository = MessagesRepository;
            _MailService = MailService;
            _Statistic = Statistic;
        }

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Рассыльщик почты";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        private ICommand _ExitCommand;

        public ICommand ExitCommand => _ExitCommand
            ??= new LambdaCommand(OnExitCommandExecuted);

        private static void OnExitCommandExecuted(object _)
        {
            Application.Current.Shutdown();
        }

        private ICommand _AboutCommand;

        public ICommand AboutCommand => _AboutCommand
            ??= new LambdaCommand(OnAboutCommandExecuted);

        private static void OnAboutCommandExecuted(object _)
        {
            MessageBox.Show("Рассыльщик почты", "О программе");
        }

        public ObservableCollection<Server> Servers { get; } = new();
        public ObservableCollection<Sender> Senders { get; } = new();
        public ObservableCollection<Recipient> Recipients { get; } = new();
        public ObservableCollection<Message> Messages { get; } = new();

        #region Command LoadDataCommand - Загрузка данных

        /// <summary>Загрузка данных</summary>
        private LambdaCommand _LoadDataCommand;

        /// <summary>Загрузка данных</summary>
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new(OnLoadDataCommandExecuted);

        /// <summary>Логика выполнения - Загрузка данных</summary>
        private void OnLoadDataCommandExecuted(object p)
        {
            Servers.Clear();
            Senders.Clear();
            Recipients.Clear();
            Messages.Clear();

            foreach (var item in _ServersRepository.GetAll()) Servers.Add(item);
            SelectedServer = Servers.FirstOrDefault();

            foreach (var item in _RecipientsRepository.GetAll()) Recipients.Add(item);
            SelectedRecipient = Recipients.FirstOrDefault();

            foreach (var item in _SendersRepository.GetAll()) Senders.Add(item);
            SelectedSender = Senders.FirstOrDefault();

            foreach (var item in _MessagesRepository.GetAll()) Messages.Add(item);
            SelectedMessage = Messages.FirstOrDefault();
        }

        #endregion

        #region Command SendMessageCommand - Отправка почты

        /// <summary>Отправка почты</summary>
        private LambdaCommand _SendMessageCommand;

        /// <summary>Отправка почты</summary>
        public ICommand SendMessageCommand => _SendMessageCommand
            ??= new(OnSendMessageCommandExecuted);

        /// <summary>Логика выполнения - Отправка почты</summary>
        private void OnSendMessageCommandExecuted(object p)
        {
            var server = SelectedServer;

            //_MailService.SendEmail("Отправитель", "Получатель", "Тема", "Тело письма");
            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);

            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            mail_sender.Send(sender.Address, recipient.Address, message.Title, message.Text);
        }

        #endregion

        #region SelectedRecipient : Recipient - Выбранный получатель

        /// <summary>Выбранный получатель</summary>
        private Recipient _SelectedRecipient;

        /// <summary>Выбранный получатель</summary>
        public Recipient SelectedRecipient { get => _SelectedRecipient; set => Set(ref _SelectedRecipient, value); }

        #endregion

        #region SelectedSender : Sender - Выбранный отправитель

        /// <summary>Выбранный отправитель</summary>
        private Sender _SelectedSender;

        /// <summary>Выбранный отправитель</summary>
        public Sender SelectedSender { get => _SelectedSender; set => Set(ref _SelectedSender, value); }

        #endregion

        #region SelectedServer : Server - Выбранный сервер

        /// <summary>Выбранный сервер</summary>
        private Server _SelectedServer;

        /// <summary>Выбранный сервер</summary>
        public Server SelectedServer { get => _SelectedServer; set => Set(ref _SelectedServer, value); }

        #endregion

        #region SelectedMessage : Message - Выбранное сообщение

        /// <summary>Выбранное сообщение</summary>
        private Message _SelectedMessage;

        /// <summary>Выбранное сообщение</summary>
        public Message SelectedMessage { get => _SelectedMessage; set => Set(ref _SelectedMessage, value); }

        #endregion

        #region Command EditServerCommand - Редактирование сервера

        /// <summary>Редактирование сервера</summary>
        private LambdaCommand _EditServerCommand;

        /// <summary>Редактирование сервера</summary>
        public ICommand EditServerCommand => _EditServerCommand
            ??= new(OnEditServerCommandExecuted, p => p is Server);

        /// <summary>Логика выполнения - Редактирование сервера</summary>
        private void OnEditServerCommandExecuted(object p)
        {
            if (p is not Server server) return;
            if (_UserDialog.EditServer(server))
                _ServersRepository.Update(server);
        }

        #endregion
    }
}
