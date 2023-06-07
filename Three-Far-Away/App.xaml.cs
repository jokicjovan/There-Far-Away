using System.Windows;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;
using Microsoft.EntityFrameworkCore;
using Three_Far_Away.DbContexts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Services;
using System.Collections.Generic;
using Three_Far_Away.Repositories;
using Microsoft.Extensions.Configuration;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Credential = Three_Far_Away.Models.Credential;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;
using System;

namespace Three_Far_Away
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost host;

        public App() {
            host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                //dbcontext
                string connectionString = hostContext.Configuration.GetConnectionString("Default");
                services.AddSingleton(new ThereFarAwayDbContextFactory(connectionString));
                services.AddSingleton(s => s.GetRequiredService<ThereFarAwayDbContextFactory>().CreateDbContext());

                services.AddTransient<AgentNavigationBarViewModel>();
                services.AddTransient<ClientNavigationBarViewModel>();
                services.AddTransient<AgentHamburgerNavigationBarViewModel>();
                services.AddTransient<ClientHamburgerNavigationBarViewModel>();

                //viewmodels
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegistrationViewModel>();
                services.AddTransient<AgentMainViewModel>();
                services.AddTransient<JourneyPreviewViewModel>();
                services.AddTransient<JourneysViewModel>();
                services.AddTransient<ClientMainViewModel>();
                services.AddTransient<CreateJourneyViewModel>();
                services.AddTransient<JourneyCardViewModel>();
                services.AddTransient<LocationListItemViewModel>();
                services.AddTransient<CreateJourneyMapViewModel>();
                services.AddTransient<CreateJourneyAttractionsViewModel>();

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
                services.AddSingleton<AccountStore>();

            }).Build();
        }
        public void MapWithPushpins_TouchDown()
        {
            MessageBox.Show("1".ToString());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            //loadArrangements(loadUsers(), loadJourneys());

            EventBus.FireEvent("Login");
            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.Dispose();

            base.OnExit(e);
        }

        private User loadUsers() {
            User user1 = new User();
            user1.Name = "Petar";
            user1.Surname = "Petrovic";
            user1.Role = Role.AGENT;
            user1 = App.host.Services.GetService<IUserService>().Create(user1);
            Credential credential = new Credential();
            credential.User = user1;
            credential.Username = "asdasd";
            credential.Password = BCrypt.Net.BCrypt.HashPassword("asdasd");
            credential = App.host.Services.GetService<ICredentialService>().Create(credential);

            User user2 = new User();
            user2.Name = "Marko";
            user2.Surname = "Makrovic";
            user2.Role = Role.CLIENT;
            user2 = App.host.Services.GetService<IUserService>().Create(user2);
            Credential credential2 = new Credential();
            credential2.User = user2;
            credential2.Username = "dsadsa";
            credential2.Password = BCrypt.Net.BCrypt.HashPassword("dsadsa");
            credential2 = App.host.Services.GetService<ICredentialService>().Create(credential2);

            return user1;
        }

        private Journey loadJourneys()
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
            journey.Transportation = TransportationType.Airplane;



            Journey journey1 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey2 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey3 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey4 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey5 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));
            Journey journey6 = App.host.Services.GetService<IJourneyService>().Create(new Journey(journey));


            return journey1;
        }

        private void loadArrangements(User user, Journey journey){
            Arrangement arrangement = new Arrangement();
            arrangement.User = user;
            arrangement.Journey = journey;
            arrangement.Status = ArrangementStatus.RESERVED;
            arrangement.DateArranged = DateTime.Now;
            arrangement = App.host.Services.GetService<IArrangementService>().Create(arrangement);
        }
    }
}
