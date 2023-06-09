using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Maps.MapControl.WPF;
using Three_Far_Away.ViewModels;

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
