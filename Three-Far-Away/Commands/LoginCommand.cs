﻿using System;
using System.ComponentModel;
using Three_Far_Away.Models;
using Three_Far_Away.Services;
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

        public override async void Execute(object parameter)
        {
            try
            {
                User user = await _loginViewModel.credentialService.Authenticate(_loginViewModel.Username, _loginViewModel.Password);
                if (user.Role == Role.CLIENT)
                {
                    _loginViewModel.navigationUserMainViewModel.Navigate();
                }
                else 
                {
                    _loginViewModel.navigationAgentMainViewModel.Navigate();
                }
            }
            catch (Exception ex)
            {

            }
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
