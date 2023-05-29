using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class ClientMainViewModel : NavigableViewModel
    {
        private ClientNavigationBarViewModel _clientNavigationBarViewModel;
        public ClientNavigationBarViewModel ClientNavigationBarViewModel
        {
            get { return _clientNavigationBarViewModel; }
            set
            {
                _clientNavigationBarViewModel = value;
                OnPropertyChanged(nameof(ClientNavigationBarViewModel));
            }
        }

        public ClientMainViewModel(JourneysViewModel agentJourneysViewModel, ClientNavigationBarViewModel clientNavigationBarViewModel)
        {
            _clientNavigationBarViewModel = clientNavigationBarViewModel;
            SwitchCurrentViewModel(agentJourneysViewModel);
            RegisterHandlers();
        }

        private void RegisterHandlers() 
        {
            EventBus.RegisterHandler("ClientJourneys", () =>
            {
                JourneysViewModel Cjvm = App.host.Services.GetRequiredService<JourneysViewModel>();
                SwitchCurrentViewModel(Cjvm);
            });

            EventBus.RegisterHandler("ClientJourneyPreview", (object journeyId) =>
            {
                JourneyPreviewViewModel jpvm = new JourneyPreviewViewModel((Guid)journeyId);
                SwitchCurrentViewModel(jpvm);
            });
        }
    }
}
