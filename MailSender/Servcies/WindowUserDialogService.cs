using MailSender.Interfaces;
using MailSender.Models;
using MailSender.ViewModels;
using MailSender.Views;

namespace MailSender.Servcies
{
    internal class WindowUserDialogService : IUserDialog
    {
        public bool EditServer(Server server)
        {
            var model = new ServerEditorDialogViewModel
            {
                Name = server.Name,
                Address = server.Address,
                Port = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password,
            };

            var view = new ServerEditorDialogWindow
            {
                DataContext = model
            };

            model.EditCompleted += (s, e) =>
            {
                view.DialogResult = true;
                view.Close();
            };

            model.EditCanceled += (s, e) =>
            {
                view.DialogResult = false;
                view.Close();
            };

            if (view.ShowDialog() != true) 
                return false;

            server.Name = model.Name;
            server.Address = model.Address;
            server.Port = model.Port;
            server.UseSSL = model.UseSSL;
            server.Login = model.Login;
            server.Password = model.Password;

            return true;
        }
    }
}
