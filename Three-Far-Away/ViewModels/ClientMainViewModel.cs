using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class ClientMainViewModel : NavigableViewModel
    {

        private ClientHamburgerNavigationBarViewModel _clientHamburgerNavigationBarViewModel;
        public ClientHamburgerNavigationBarViewModel ClientHamburgerNavigationBarViewModel
        {
            get
            {
                return _clientHamburgerNavigationBarViewModel;
            }
            set
            {
                _clientHamburgerNavigationBarViewModel = value;
                OnPropertyChanged(nameof(ClientHamburgerNavigationBarViewModel));
            }
        }

        public ClientMainViewModel(JourneysViewModel agentJourneysViewModel, ClientHamburgerNavigationBarViewModel clientHamburgerNavigationBarViewModel)
        {
            _clientHamburgerNavigationBarViewModel = clientHamburgerNavigationBarViewModel;
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
            
            EventBus.RegisterHandler("MyJourneys", () =>
            {
                ClientsJourneysViewModel Cjvm = App.host.Services.GetRequiredService<ClientsJourneysViewModel>();
                SwitchCurrentViewModel(Cjvm);
            });

            EventBus.RegisterHandler("ClientJourneyPreview", (object journeyId) =>
            {
                JourneyPreviewViewModel jpvm = new JourneyPreviewViewModel((Guid)journeyId);
                SwitchCurrentViewModel(jpvm);
            });

            EventBus.RegisterHandler("ClientAttractionPreview", (object attractionId) =>
            {
                AttractionPreviewViewModel apvm = new AttractionPreviewViewModel((Guid)attractionId);
                SwitchCurrentViewModel(apvm);
            });
        }
    }
}
