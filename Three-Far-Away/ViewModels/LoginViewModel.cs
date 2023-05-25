using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services;

namespace Three_Far_Away.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

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

        public ICommand SubmitCommand { get; }

        public LoginViewModel(NavigationService<UserMainViewModel> navigationService)
        {
            SubmitCommand = new LoginCommand(this);
        }
    }
}
