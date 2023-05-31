using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.ViewModels
{
    public class AgentMainViewModel : NavigableViewModel
    {
        private AgentNavigationBarViewModel _agentNavigationBarViewModel;
        public AgentNavigationBarViewModel AgentNavigationBarViewModel
        {
            get { return _agentNavigationBarViewModel; }
            set
            {
                _agentNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentNavigationBarViewModel));
            }
        }

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

        public AgentMainViewModel(JourneysViewModel agentJourneysViewModel, AgentNavigationBarViewModel agentNavigationBarViewModel, AgentHamburgerNavigationBarViewModel agentHamburgerNavigationBarViewModel)
        {
            _agentNavigationBarViewModel = agentNavigationBarViewModel;
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

            EventBus.RegisterHandler("CreateJourney", (object journey) =>
            {
                CreateJourneyViewModel cjvm = new CreateJourneyViewModel((Journey)journey);
                SwitchCurrentViewModel(cjvm);
            });

            EventBus.RegisterHandler("CreateJourneyMap", (object journey) =>
            {
                CreateJourneyMapViewModel cjmvm = new CreateJourneyMapViewModel((Journey)journey);
                SwitchCurrentViewModel(cjmvm);
            });
            EventBus.RegisterHandler("CreateJourneyAttractions", (object journey) =>
            {
                CreateJourneyAttractionsViewModel cjmvm = new CreateJourneyAttractionsViewModel((Journey)journey);
                SwitchCurrentViewModel(cjmvm);
            });
        }
    }
}
