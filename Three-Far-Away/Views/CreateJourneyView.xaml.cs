using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
using Three_Far_Away.Models;
using System.Windows.Input;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for CreateJourneyView.xaml
    /// </summary>
    public partial class CreateJourneyView : UserControl
    {
        public CreateJourneyView()
        {
            InitializeComponent();
            trasportationsCB.ItemsSource = Enum.GetValues(typeof (TransportationType));
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            trasportationsCB.ItemsSource = Enum.GetValues(typeof (TransportationType));
            
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            SetHelpKey(null, null);
            name.Focus();
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentAddJourney1");
            }
        }
    }
}
