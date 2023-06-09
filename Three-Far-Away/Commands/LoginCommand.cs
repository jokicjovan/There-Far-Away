﻿using System;
using System.ComponentModel;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
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
                   !(_loginViewModel.Username.Length < 6) && !(_loginViewModel.Username.Length > 20) &&
                   !(_loginViewModel.Password.Length < 6) && !(_loginViewModel.Password.Length > 20) &&
                   base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            _loginViewModel.ErrorMessage = String.Empty;
            _loginViewModel.IsLoading = true;
            try
            {
                User user = await _loginViewModel.credentialService.Authenticate(_loginViewModel.Username, _loginViewModel.Password);
                _loginViewModel.accountStore.Name = user.Name;
                _loginViewModel.accountStore.Role = user.Role;
                _loginViewModel.accountStore.Id = user.Id;
                if (user.Role == Role.CLIENT)
                {
                    EventBus.FireEvent("ClientLogin");
                }
                else
                {
                    EventBus.FireEvent("AgentLogin");
                }
            }
            catch (Exception ex)
            {
                _loginViewModel.ErrorMessage = "Invalid username or password!";
            }
            _loginViewModel.IsLoading = false;
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
