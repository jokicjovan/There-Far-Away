using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region services
        public readonly ICredentialService credentialService;
        #endregion

        #region navigations
        public readonly NavigationService<UserMainViewModel> navigationUserMainViewModel;
        public readonly NavigationService<AgentMainViewModel> navigationAgentMainViewModel;
        #endregion

        #region properties
        private string _username;
        public string Username
        {
            get 
            {
                return _username;
            }
            set 
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password 
        {
            get 
            {
                return _password;
            }
            set 
            {
                _password = value; 
                OnPropertyChanged(nameof(Password));
            } 
        }
        #endregion

        #region commands
        public ICommand SubmitCommand { get; }
        #endregion

        public LoginViewModel(NavigationService<UserMainViewModel> navigationUserMainViewModel, NavigationService<AgentMainViewModel> navigationAgentMainViewModel, 
            ICredentialService credentialService)
        {
            this.credentialService = credentialService;
            this.navigationUserMainViewModel = navigationUserMainViewModel;
            this.navigationAgentMainViewModel = navigationAgentMainViewModel;
            SubmitCommand = new LoginCommand(this);
        }
    }
}
