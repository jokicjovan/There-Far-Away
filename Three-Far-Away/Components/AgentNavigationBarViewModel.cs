using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Three_Far_Away.Commands;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Components
{
    public class AgentNavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateJourneys { get; }
        public ICommand NavigateLogin { get; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public AgentNavigationBarViewModel(INavigationService<AgentJourneysViewModel> navigationAgentJourneys, INavigationService<LoginViewModel> navigationLogin, AccountStore accountStore)
        {
            _name = accountStore.Name;
            NavigateJourneys = new NavigateCommand<AgentJourneysViewModel>(navigationAgentJourneys);
            NavigateLogin = new NavigateCommand<LoginViewModel>(navigationLogin);
        }
    }
}
