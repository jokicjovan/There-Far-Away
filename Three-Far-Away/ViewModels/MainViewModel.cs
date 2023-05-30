using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.ViewModels
{
    public class MainViewModel : NavigableViewModel
    {
        public MainViewModel(LoginViewModel lvm)
        {
            SwitchCurrentViewModel(lvm);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            EventBus.RegisterHandler("ClientLogin", () =>
            {
                ClientMainViewModel cmvm = App.host.Services.GetRequiredService<ClientMainViewModel>();
                SwitchCurrentViewModel(cmvm);
            });

            EventBus.RegisterHandler("AgentLogin", () =>
            {
                AgentMainViewModel amvm = App.host.Services.GetRequiredService<AgentMainViewModel>();
                SwitchCurrentViewModel(amvm);
            });

            EventBus.RegisterHandler("GoToLogin", () =>
            {
                LoginViewModel lvm = App.host.Services.GetRequiredService<LoginViewModel>();
                SwitchCurrentViewModel(lvm);
            });

            EventBus.RegisterHandler("GoToRegister", () =>
            {
                RegistrationViewModel rvm = App.host.Services.GetRequiredService<RegistrationViewModel>();
                SwitchCurrentViewModel(rvm);
            });
        }
    }
}
