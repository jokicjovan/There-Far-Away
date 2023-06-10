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
        }
    }
}
