using System.Windows;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;
using Microsoft.EntityFrameworkCore;
using Three_Far_Away.DbContexts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Services;
using System;
using System.Collections.Generic;
using Three_Far_Away.Repositories;
using Microsoft.Extensions.Configuration;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Credential = Three_Far_Away.Models.Credential;
using Three_Far_Away.Components;
using Three_Far_Away.Views;

namespace Three_Far_Away
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost _host;

        public App() {
            _host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                //dbcontext
                string connectionString = hostContext.Configuration.GetConnectionString("Default");
                services.AddSingleton(new ThereFarAwayDbContextFactory(connectionString));
                services.AddSingleton(s => s.GetRequiredService<ThereFarAwayDbContextFactory>().CreateDbContext());

                services.AddTransient<AgentNavigationBarViewModel>();

                //viewmodels
                services.AddTransient<LoginViewModel>();
                services.AddSingleton<Func<LoginViewModel>>((s) => () => s.GetRequiredService<LoginViewModel>());
                services.AddSingleton<INavigationService<LoginViewModel>, NavigationService<LoginViewModel>>();

                services.AddTransient<CreateJourneyViewModel>();
                services.AddSingleton<Func<CreateJourneyViewModel>>((s) => () => s.GetRequiredService<CreateJourneyViewModel>());
                services.AddSingleton<INavigationService<CreateJourneyViewModel>, NavigationService<CreateJourneyViewModel>>();

                services.AddTransient<AgentJourneysViewModel>();
                services.AddSingleton<Func<AgentJourneysViewModel>>((s) => () => s.GetRequiredService<AgentJourneysViewModel>());
                services.AddSingleton<INavigationService<AgentJourneysViewModel>, NavigationService<AgentJourneysViewModel>>();
                
                services.AddTransient<JourneyCardViewModel>();
                services.AddSingleton<Func<JourneyCardViewModel>>((s) => () => s.GetRequiredService<JourneyCardViewModel>());
                services.AddSingleton<INavigationService<JourneyCardViewModel>, NavigationService<JourneyCardViewModel>>();
                
                services.AddTransient<AgentJourneyPreviewViewModel>();
                services.AddSingleton<Func<AgentJourneyPreviewViewModel>>((s) => () => s.GetRequiredService<AgentJourneyPreviewViewModel>());
                services.AddSingleton<INavigationService<AgentJourneyPreviewViewModel>, NavigationService<AgentJourneyPreviewViewModel>>();

                services.AddTransient<LocationListItemViewModel>();
                services.AddSingleton<Func<LocationListItemViewModel>>((s) => () => s.GetRequiredService<LocationListItemViewModel>());
                services.AddSingleton<INavigationService<LocationListItemViewModel>, NavigationService<LocationListItemViewModel>>();

                services.AddTransient<ClientJourneyPreviewViewModel>();
                services.AddSingleton<Func<ClientJourneyPreviewViewModel>>((s) => () => s.GetRequiredService<ClientJourneyPreviewViewModel>());
                services.AddSingleton<INavigationService<ClientJourneyPreviewViewModel>, NavigationService<ClientJourneyPreviewViewModel>>();

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
                services.AddSingleton<AccountStore>();

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            // loadData(_host);
            // loadJourney(_host);
            _host.Services.GetRequiredService<INavigationService<LoginViewModel>>().Navigate();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }

        private void loadData(IHost host) {
            User user = new User();
            user.Name = "Petar";
            user.Surname = "Petrovic";
            user.Role = Role.CLIENT;
            user = _host.Services.GetService<IUserService>().Create(user);
            Credential credential = new Credential();
            credential.User = user;
            credential.Username = "asdasdasd";
            credential.Password = BCrypt.Net.BCrypt.HashPassword("asdasdasd");
            credential = _host.Services.GetService<ICredentialService>().Create(credential);
        }

        private void loadJourney(IHost host)
        {
            Location location = new Location();
            location.Address = "Partizanska 2";
            location.Latitude = 14.44;
            location.Longitude = 12.22;
            
            Attraction attraction = new Attraction();
            attraction.Name = "Atrakcija";
            attraction.Description = "opis";
            attraction.Type = AttractionType.ATTRACTION;
            attraction.Location = location;
            attraction.Image = "slika";
            
            Journey journey = new Journey();
            journey.Name = "ime";
            journey.StartLocation = location;
            journey.EndLocation = location;
            journey.Attractions = new List<Attraction>();
            journey.Attractions.Add(attraction);
            journey.Attractions.Add(attraction);
            journey.Attractions.Add(attraction);
            journey.Transportation = TransportationType.PLANE;
            
            
            
            Journey journey1 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey2 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey3 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey4 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey5 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey6 = _host.Services.GetService<IJourneyService>().Create(new Journey(journey));

            
        }
    }
}
