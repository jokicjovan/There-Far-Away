using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for ClientJourneyPreviewView.xaml
    /// </summary>
    public partial class ClientJourneyPreviewView : UserControl
    {
        public ClientJourneyPreviewView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
        }
    }
}
