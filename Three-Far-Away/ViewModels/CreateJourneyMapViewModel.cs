using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    class CreateJourneyMapViewModel : ViewModelBase
    {
        public readonly IJourneyService _journeyService;
        private HttpClient httpClient;
        public ObservableCollection<MapLocation> Locations { get; private set; }

        public ICommand MapClickCommand { get; }

        public ICommand TurnOnStartLocationCommand { get; }

        public ICommand TurnOnEndLocationCommand { get; }
        public ICommand SearchStartLocationCommand { get; }
        public ICommand SearchEndLocationCommand { get; }

        private Location _coordinates;
        public Location Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
                OnPropertyChanged(nameof(Coordinates));
            }
        }

        private Models.Location _startLocationModel;
        public Models.Location StartLocationModel
        {
            get
            {
                return _startLocationModel;
            }
            set
            {
                _startLocationModel = value;
                OnPropertyChanged(nameof(StartLocationModel));
            }
        }

        private Models.Location _endLocationModel;
        public Models.Location EndLocationModel
        {
            get
            {
                return _endLocationModel;
            }
            set
            {
                _endLocationModel = value;
                OnPropertyChanged(nameof(EndLocationModel));
            }
        }

        private string _startCity;
        public string StartCity
        {
            get
            {
                return _startCity;
            }
            set
            {
                _startCity = value;
                OnPropertyChanged(nameof(StartCity));
            }
        }

        private bool _canDropStart;
        public bool CanDropStart
        {
            get
            {
                return _canDropStart;
            }
            set
            {
                _canDropStart = value;
                OnPropertyChanged(nameof(CanDropStart));
            }
        }

        private bool _canDropEnd;
        public bool CanDropEnd
        {
            get
            {
                return _canDropEnd;
            }
            set
            {
                _canDropEnd = value;
                OnPropertyChanged(nameof(CanDropEnd));
            }
        }


        private string _endCity;
        public string EndCity
        {
            get
            {
                return _endCity;
            }
            set
            {
                _endCity = value;
                OnPropertyChanged(nameof(EndCity));
            }
        }

        public CreateJourneyMapViewModel(IJourneyService journeyService)
        {
            _journeyService = journeyService;
            Locations = new ObservableCollection<MapLocation>();
            MapClickCommand= new AddStartLocationPinCommand(this);
            TurnOnStartLocationCommand = new TurnOnStartLocationPin(this);
            TurnOnEndLocationCommand = new TurnOnEndLocationPin(this);
            SearchStartLocationCommand = new SearchStartLocationCommand(this);
            SearchEndLocationCommand = new SearchEndLocationCommand(this);
            httpClient = new HttpClient();
        }
        private Map _map;
        public Map MapControl
        {
            get { return _map; }
            set
            {
                _map = value;
                OnPropertyChanged(nameof(MapControl));
            }
        }


        public async Task UpdateStartLocationAsync(Location location)
        {
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations/"+location.Latitude+","+ location.Longitude+ "?key=AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr";
    

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var data = (JObject)JsonConvert.DeserializeObject(responseBody);
            StartLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), location.Latitude, location.Longitude);
            StartCity = StartLocationModel.Address;
        }

        public async Task UpdateEndLocationAsync(Location location)
        {
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations/" + location.Latitude + "," + location.Longitude + "?key=AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr";


            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var data = (JObject)JsonConvert.DeserializeObject(responseBody);
            EndLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), location.Latitude, location.Longitude);
            EndCity = EndLocationModel.Address;
        }
    }
    public class MapLocation
    {
        public Location Location { get; set; }
        public string Name { get; set; }
        public bool IsStart { get; set; }

        public MapLocation(Location location, string name, bool isStart)
        {
            Location = location;
            Name = name;
            IsStart = isStart;
        }
    }
}
