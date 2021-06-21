using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using MailSender.Interfaces;
using MailSender.Models;
using MailSender.Servcies;
using MailSender.Servcies.InMemory;
using MailSender.Services;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailSender
{
    public partial class App
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting 
            ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] Args) => Host
           .CreateDefaultBuilder(Args)
           .ConfigureAppConfiguration(opt => opt.AddJsonFile("settings.json", true, true))
           .ConfigureLogging(opt => opt.AddDebug())
           .ConfigureServices(ConfigureServices)
        ;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
            services.AddSingleton<ServersRepository>();

            //services.AddSingleton<IStatistic, InMemoryStatisticService>();
            services.AddSingleton<IMailService, DebugMailService>();
            services.AddSingleton<IRepository<Server>, InMemoryServersRepository>();
            services.AddSingleton<IRepository<Sender>, InMemorySendersRepository>();
            services.AddSingleton<IRepository<Recipient>, InMemoryRecipientsRepository>();
            services.AddSingleton<IRepository<Message>, InMemoryMessagesRepository>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Hosting.Start();
            base.OnStartup(e);

            //var services = new ServiceCollection();
            //services.AddScoped<MainWindowViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Hosting.Dispose();
        }
    }
}
