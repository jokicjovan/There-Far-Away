using Microsoft.Maps.MapControl.WPF;
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
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for CreateAttractioonMapView.xaml
    /// </summary>
    public partial class CreateAttractioonMapView : UserControl
    {
        public CreateAttractioonMapView()
        {
            InitializeComponent();
        }

        private void myMap_MouseDown(object sender, MouseEventArgs e)
        {
            CreateAttractionMapViewModel vm = (CreateAttractionMapViewModel)this.DataContext;
            vm.Coordinates = new Location(e.GetPosition(myMap).X, e.GetPosition(myMap).Y);
            Point mousePosition = e.GetPosition(myMap);
            if (vm.CanDropStart)
            {
                bool hasStart = false;
                foreach (MapLocation item in vm.Locations)
                {
                    if (item.IsStart)
                    {
                        vm.Locations.Remove(item);
                        Location location = myMap.ViewportPointToLocation(mousePosition);
                        
                        vm.UpdateStartLocationAsync(location);
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Location location = myMap.ViewportPointToLocation(mousePosition);
                    vm.UpdateStartLocationAsync(location);
                }

            }


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
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentAddAtraction2");
            }
        }
    }
}
