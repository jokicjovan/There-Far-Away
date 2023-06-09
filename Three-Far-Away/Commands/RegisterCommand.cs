using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Windows;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class RegisterCommand : CommandBase
    {
        private readonly RegistrationViewModel _registrationViewModel;
        public RegisterCommand(RegistrationViewModel registrationViewModel)
        {
            _registrationViewModel = registrationViewModel;
            _registrationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_registrationViewModel.Username) && !string.IsNullOrEmpty(_registrationViewModel.Password) &&
                   !string.IsNullOrEmpty(_registrationViewModel.Name) && !string.IsNullOrEmpty(_registrationViewModel.Surname) &&
                   !(_registrationViewModel.Username.Length < 6) && !(_registrationViewModel.Username.Length > 20) &&  
                   !(_registrationViewModel.Password.Length < 6) && !(_registrationViewModel.Password.Length > 20) &&  
                   !(_registrationViewModel.Name.Length < 3) && !(_registrationViewModel.Name.Length > 50) &&
                   !(_registrationViewModel.Password.Length < 3) && !(_registrationViewModel.Password.Length > 50) &&
                   base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            _registrationViewModel.ErrorMessage = String.Empty;
            _registrationViewModel.IsLoading = true;

            User user = new User();
            try
            {
                user.Name = _registrationViewModel.Name;
                user.Surname = _registrationViewModel.Surname;
                user.Role = Role.CLIENT;
                user = _registrationViewModel.userService.Create(user);
            }
            catch (Exception ex)
            {
                _registrationViewModel.IsLoading = false;
                return;
            }

            try
            {
                Credential credential = new Credential();
                credential.User = user;
                credential.Username = _registrationViewModel.Username;
                credential.Password = BCrypt.Net.BCrypt.HashPassword(_registrationViewModel.Password);
                credential = _registrationViewModel.credentialService.Create(credential);

                MessageBoxResult messageBoxResult = MessageBox.Show("You registered successfully, continue to login", "Success", MessageBoxButton.OK);
                EventBus.FireEvent("GoToLogin");
            }
            catch (Exception ex)
            {
               _registrationViewModel.userService.Delete(user.Id);
               _registrationViewModel.AddError("User with this username already exists!", nameof(_registrationViewModel.Username));
            }

            _registrationViewModel.IsLoading = false;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegistrationViewModel.Username) ||
                e.PropertyName == nameof(RegistrationViewModel.Password) ||
                e.PropertyName == nameof(RegistrationViewModel.Name) ||
                e.PropertyName == nameof(RegistrationViewModel.Surname))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
