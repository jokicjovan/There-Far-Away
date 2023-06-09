using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }
    }
}
