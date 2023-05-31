using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.ViewModels
{
    public class AgentMainViewModel : NavigableViewModel
    {
        private AgentHamburgerNavigationBarViewModel _agentHamburgerNavigationBarViewModel;
        public AgentHamburgerNavigationBarViewModel AgentHamburgerNavigationBarViewModel
        {
            get
            {
                return _agentHamburgerNavigationBarViewModel;
            }
            set
            {
                _agentHamburgerNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentHamburgerNavigationBarViewModel));
            }
        }

        public AgentMainViewModel(JourneysViewModel agentJourneysViewModel, AgentHamburgerNavigationBarViewModel agentHamburgerNavigationBarViewModel)
        {
            _agentHamburgerNavigationBarViewModel = agentHamburgerNavigationBarViewModel;
            SwitchCurrentViewModel(agentJourneysViewModel);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            EventBus.RegisterHandler("AgentJourneys", () =>
            {
                JourneysViewModel ajvm = App.host.Services.GetRequiredService<JourneysViewModel>();
                SwitchCurrentViewModel(ajvm);
            });

            EventBus.RegisterHandler("AgentJourneyPreview", (object journeyId) =>
            {
                JourneyPreviewViewModel jpvm = new JourneyPreviewViewModel((Guid)journeyId);
                SwitchCurrentViewModel(jpvm);
            });
        }
    }
}
