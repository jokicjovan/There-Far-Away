using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Components
{
    public class ClientNavigationBarViewModel : ViewModelBase
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

        public ClientNavigationBarViewModel(INavigationService<ClientJourneysViewModel> navigationClientJourneys, INavigationService<LoginViewModel> navigationLogin, AccountStore accountStore)
        {
            _name = accountStore.Name;
            NavigateJourneys = new NavigateCommand<ClientJourneysViewModel>(navigationClientJourneys);
            NavigateLogin = new NavigateCommand<LoginViewModel>(navigationLogin);
        }
    }
}
