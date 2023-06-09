using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Three_Far_Away.Models;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            usernameTextBox.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            var windows = Application.Current.Windows.OfType<Window>();
            foreach (var window in windows)
            {
                if (window is DependencyObject)
                {
                    HelpProvider.SetHelpKey(window, "register");
                }
            }
        }
    }
}
