using GongSolutions.Wpf.DragDrop.Utilities;
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
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for CreateJourneyAttractionsView.xaml
    /// </summary>
    public partial class CreateJourneyAttractionsView : UserControl
    {
        public CreateJourneyAttractionsView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateJourneyAttractionsViewModel vm = (CreateJourneyAttractionsViewModel)this.DataContext;
            List<Attraction> attractions= listBoxAll.SelectedItems.OfType<Attraction>().ToList();
            if (vm != null)
            {
                vm.Locations.Clear();
                foreach (Attraction attraction in attractions)
                {
                    vm.Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(attraction.Location.Longitude, attraction.Location.Latitude), "", false));
                }
            }

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            CreateJourneyAttractionsViewModel vm = (CreateJourneyAttractionsViewModel)this.DataContext;
            List<Attraction> attractions = listBoxSelected.SelectedItems.OfType<Attraction>().ToList();
            if (vm != null)
            {
                vm.Locations.Clear();
                foreach (Attraction attraction in attractions)
                {
                    vm.Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(attraction.Location.Longitude, attraction.Location.Latitude), "", false));
                }
            }
        }


    }
}
