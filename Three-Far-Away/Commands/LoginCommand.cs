using System;
using System.ComponentModel;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        public LoginCommand(LoginViewModel loginViewModel) {
            _loginViewModel = loginViewModel;
            _loginViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_loginViewModel.Username) && !string.IsNullOrEmpty(_loginViewModel.Password) &&
                   base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException(); 
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            if (e.PropertyName == nameof(LoginViewModel.Username) ||
                e.PropertyName == nameof(LoginViewModel.Password))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
