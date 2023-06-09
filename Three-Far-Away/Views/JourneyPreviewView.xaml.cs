using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for JourneyPreviewView.xaml
    /// </summary>
    public partial class JourneyPreviewView : UserControl
    {
        public JourneyPreviewView()
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
