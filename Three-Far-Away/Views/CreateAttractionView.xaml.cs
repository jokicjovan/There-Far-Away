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
using Three_Far_Away.Models;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for CreateAttractionView.xaml
    /// </summary>
    public partial class CreateAttractionView : UserControl
    {
        public CreateAttractionView()
        {
            InitializeComponent();
            typeAttractionCB.ItemsSource = Enum.GetValues(typeof(AttractionType));



        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            firstTextBox.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentAddAtraction1");
            }
        }
    }
}
