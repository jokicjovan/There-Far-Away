using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
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

                ClearErrors(nameof(Username));;
                if (Username.Length < 6) 
                {
                    AddError("Username cannot be shorter than 6 characters!", nameof(Username));
                }
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

                ClearErrors(nameof(Password)); ;
                if (Password.Length < 6)
                {
                    AddError("Password cannot be shorter than 6 characters!", nameof(Password));
                }
            } 
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get 
            {
                return _isLoading;
            }
            set 
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        #endregion

        #region commands
        public ICommand SubmitCommand { get; }
        #endregion

        #region errors
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private string _errorMessage;
        public string ErrorMessage 
        {
            get 
            {
                return _errorMessage;
            }
            set 
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        #endregion

        public LoginViewModel(NavigationService<UserMainViewModel> navigationUserMainViewModel, NavigationService<AgentMainViewModel> navigationAgentMainViewModel, 
            ICredentialService credentialService)
        {
            this.credentialService = credentialService;
            this.navigationUserMainViewModel = navigationUserMainViewModel;
            this.navigationAgentMainViewModel = navigationAgentMainViewModel;
            SubmitCommand = new LoginCommand(this);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName) 
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName) 
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string errorMessage, string propertyName) { 
            if(!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
    }
}
