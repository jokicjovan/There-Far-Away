using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for AttractionsView.xaml
    /// </summary>
    public partial class AttractionsView : UserControl
    {
        public AttractionsView()
        {
            InitializeComponent();
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            name.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentAttractions");
            }
        }
    }
}
