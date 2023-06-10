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
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Models;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for AttractionPreviewView.xaml
    /// </summary>
    public partial class AttractionPreviewView : UserControl
    {
        public AttractionPreviewView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            edit.Focus();
            SetHelpKey(null, null);
        }

        public void SetHelpKey(object sender, EventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            AccountStore accountStore = App.host.Services.GetRequiredService<AccountStore>();
            if (focusedControl is DependencyObject && accountStore.Role.Equals(Role.AGENT))
            {
                HelpProvider.SetHelpKey((DependencyObject)focusedControl, "agentAttractionsPreview");
            }
        }

        private void myMap_MouseDown(object sender, MouseEventArgs e)
        {
            CreateJourneyMapViewModel vm = (CreateJourneyMapViewModel)this.DataContext;
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
                        vm.Locations.Add(new MapLocation(location, "S", true));
                        vm.UpdateStartLocationAsync(location);
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Location location = myMap.ViewportPointToLocation(mousePosition);
                    vm.Locations.Add(new MapLocation(location, "S", true));
                    vm.UpdateStartLocationAsync(location);
                }

            }
            if (vm.CanDropEnd)
            {
                bool hasStart = false;
                foreach (MapLocation item in vm.Locations)
                {
                    if (!item.IsStart)
                    {
                        vm.Locations.Remove(item);
                        Location location = myMap.ViewportPointToLocation(mousePosition);
                        vm.Locations.Add(new MapLocation(location, "F", false));
                        vm.UpdateEndLocationAsync(location);
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Location location = myMap.ViewportPointToLocation(mousePosition);
                    vm.Locations.Add(new MapLocation(location, "F", false));
                    vm.UpdateEndLocationAsync(location);
                }
            }
        }

        private void myMap_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
    }
}
