using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MailSender
{
    public class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
