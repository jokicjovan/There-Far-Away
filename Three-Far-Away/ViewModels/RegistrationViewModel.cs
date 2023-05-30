using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class RegistrationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region services
        public readonly ICredentialService credentialService;
        public readonly IUserService userService;
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

                ClearErrors(nameof(Username)); ;
                if (Username.Length < 6)
                {
                    AddError("Username cannot be shorter than 6 characters!", nameof(Username));
                }
                else if (Username.Length > 20)
                {
                    AddError("Username cannot be longer than 20 characters!", nameof(Username));
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
                else if (Password.Length > 20)
                {
                    AddError("Password cannot be longer than 20 characters!", nameof(Password));
                }
            }
        }

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

                ClearErrors(nameof(Name)); ;
                if (Name.Length < 3)
                {
                    AddError("Name cannot be shorter than 3 characters!", nameof(Name));
                }
                else if (Name.Length > 50)
                {
                    AddError("Name cannot be longer than 50 characters!", nameof(Name));
                }
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));

                ClearErrors(nameof(Surname)); ;
                if (Surname.Length < 3)
                {
                    AddError("Surname cannot be shorter than 3 characters!", nameof(Surname));
                }
                else if (Surname.Length > 50)
                {
                    AddError("Surname cannot be longer than 50 characters!", nameof(Surname));
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
        public ICommand LoginCommand { get; }
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

        public RegistrationViewModel(ICredentialService credentialService, IUserService userService)
        {
            this.credentialService = credentialService;
            this.userService = userService;
            SubmitCommand = new RegisterCommand(this);
            LoginCommand = new FireEventCommand("GoToLogin");
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

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
    }
}
