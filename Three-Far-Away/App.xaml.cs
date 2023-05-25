using System.Windows;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;
using Microsoft.EntityFrameworkCore;
using Three_Far_Away.DbContexts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Services;
using System;
using Three_Far_Away.Repositories;
using Microsoft.Extensions.Configuration;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Models;

namespace Three_Far_Away
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App() {
            _host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                //dbcontext
                string connectionString = hostContext.Configuration.GetConnectionString("Default");
                services.AddSingleton(new ThereFarAwayDbContextFactory(connectionString));
                services.AddTransient(s => s.GetRequiredService<ThereFarAwayDbContextFactory>().CreateDbContext());

                //viewmodels
                services.AddTransient<LoginViewModel>();
                services.AddSingleton<Func<LoginViewModel>>((s) => () => s.GetRequiredService<LoginViewModel>());
                services.AddSingleton<NavigationService<LoginViewModel>>();

                services.AddTransient<UserMainViewModel>();
                services.AddSingleton<Func<UserMainViewModel>>((s) => () => s.GetRequiredService<UserMainViewModel>());
                services.AddSingleton<NavigationService<UserMainViewModel>>();

                services.AddTransient<AgentMainViewModel>();
                services.AddSingleton<Func<AgentMainViewModel>>((s) => () => s.GetRequiredService<AgentMainViewModel>());
                services.AddSingleton<NavigationService<AgentMainViewModel>>();

                services.AddTransient<CreateJourneyViewModel>();
                services.AddSingleton<Func<CreateJourneyViewModel>>((s) => () => s.GetRequiredService<CreateJourneyViewModel>());
                services.AddSingleton<NavigationService<CreateJourneyViewModel>>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });

                //services
                services.AddSingleton<IArrangementService, ArrangementService>();
                services.AddSingleton<IAttractionService, AttractionService>();
                services.AddSingleton<ICredentialService, CredentialService>();
                services.AddSingleton<IJourneyService, JourneyService>();
                services.AddSingleton<ILocationService, LocationService>();
                services.AddSingleton<IUserService, UserService>();

                //repositories
                services.AddSingleton<IArrangementRepository, ArrangementRepository>();
                services.AddSingleton<IAttractionRepository, AttractionRepository>();
                services.AddSingleton<ICredentialRepository, CredentialRepository>();
                services.AddSingleton<IJourneyRepository, JourneyRepository>();
                services.AddSingleton<ILocationRepository, LocationRepository>();
                services.AddSingleton<IUserRepository, UserRepository>();

                //stores
                services.AddSingleton<NavigationStore>();

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

/*            User user = new User();
            user.Name = "Petar";
            user.Surname = "Petrovic";
            user.Role = Role.CLIENT;
            Credential credential = new Credential();
            credential.User = user;
            credential.Username = "asd";
            credential.Password = BCrypt.Net.BCrypt.HashPassword("asd");
            credential = _host.Services.GetService<ICredentialService>().Create(credential);*/

            _host.Services.GetRequiredService<NavigationService<LoginViewModel>>().Navigate();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
