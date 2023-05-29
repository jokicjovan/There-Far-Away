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

        public ClientMainViewModel(ClientJourneysViewModel clientJourneysViewModel, ClientNavigationBarViewModel clientNavigationBarViewModel)
        {
            _clientNavigationBarViewModel = clientNavigationBarViewModel;
            SwitchCurrentViewModel(clientJourneysViewModel);
            RegisterHandlers();
        }

        private void RegisterHandlers() 
        {
            EventBus.RegisterHandler("ClientJourneys", () =>
            {
                ClientJourneysViewModel Cjvm = App.host.Services.GetRequiredService<ClientJourneysViewModel>();
                SwitchCurrentViewModel(Cjvm);
            });

            EventBus.RegisterHandler("ClientJourneyPreview", (object journeyId) =>
            {
                ClientJourneyPreviewViewModel cjpvm = new ClientJourneyPreviewViewModel((Guid)journeyId);
                SwitchCurrentViewModel(cjpvm);
            });
        }
    }
}
