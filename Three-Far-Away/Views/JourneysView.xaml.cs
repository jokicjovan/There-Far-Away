using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Three_Far_Away.Models;
using Three_Far_Away.Stores;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for AgentJourneysView.xaml
    /// </summary>
    public partial class JourneysView : UserControl
    {
        public JourneysView()
        {
            InitializeComponent();
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            title.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            AccountStore accountStore = App.host.Services.GetRequiredService<AccountStore>();
            if (focusedControl is DependencyObject)
            {
                if (accountStore.Role == Role.CLIENT)
                    HelpProvider.SetHelpKey((DependencyObject)focusedControl, "clientJourneys");
                else
                    HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentJourneys");
            }
        }
    }
}
