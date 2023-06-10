using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for ClientsJourneysView.xaml
    /// </summary>
    public partial class ClientsJourneysView : UserControl
    {
        public ClientsJourneysView()
        {
            InitializeComponent();
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            this.FocusVisualStyle = null;
            Keyboard.Focus(this);
            name.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "clientsRBJourneys");
            }
        }

    }
}
