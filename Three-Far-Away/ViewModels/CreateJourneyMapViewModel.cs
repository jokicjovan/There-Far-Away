using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    class CreateJourneyMapViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        public readonly IJourneyService _journeyService;
        private HttpClient httpClient;
        public ObservableCollection<MapLocation> Locations { get; private set; }

        public ICommand MapClickCommand { get; }

        public ICommand TurnOnStartLocationCommand { get; }

        public ICommand TurnOnEndLocationCommand { get; }
        public ICommand SearchStartLocationCommand { get; }
        public ICommand SearchEndLocationCommand { get; }
        public ICommand NavigateToCreateJourneyCommand { get; }
        public ICommand NavigateToCreateJourneyAttractionsCommand { get; }

        private Microsoft.Maps.MapControl.WPF.Location _coordinates;
        public Microsoft.Maps.MapControl.WPF.Location Coordinates
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

        public Models.Location StartLocationModel
        {
            get
            {
                return Journey.StartLocation;
            }
            set
            {
                Journey.StartLocation = value;
                OnPropertyChanged(nameof(StartLocationModel));
                
            }
        }

        public Models.Location EndLocationModel
        {
            get
            {
                return Journey.EndLocation;
            }
            set
            {
                Journey.EndLocation = value;
                OnPropertyChanged(nameof(EndLocationModel));
            }
        }

        private Journey _journey;
        public Journey Journey
        {
            get
            {
                return _journey;
            }
            set
            {
                _journey = value;
                OnPropertyChanged(nameof(Journey));
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
                ClearErrors(nameof(StartCity));
                if (StartLocationModel == null)
                {
                    AddError("Location Is Not Selected", nameof(StartCity));
                }
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
                ClearErrors(nameof(EndCity));
                if (EndLocationModel == null)
                {
                    AddError("Location Is Not Selected", nameof(EndCity));
                }
            }
        }

        #region errors
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        #endregion


        public CreateJourneyMapViewModel(Journey journey)
        {
            Journey = journey;
            _journeyService = App.host.Services.GetService<IJourneyService>();
            Locations = new ObservableCollection<MapLocation>();
            MapClickCommand= new AddStartLocationPinCommand(this);
            TurnOnStartLocationCommand = new TurnOnStartLocationPin(this);
            TurnOnEndLocationCommand = new TurnOnEndLocationPin(this);
            SearchStartLocationCommand = new SearchStartLocationCommand(this);
            SearchEndLocationCommand = new SearchEndLocationCommand(this);
            NavigateToCreateJourneyAttractionsCommand = new NavigateToCreateJourneyCommand(this,"Map","Attractions");
            NavigateToCreateJourneyCommand = new NavigateToCreateJourneyCommand(this,"Map","Home");
            httpClient = new HttpClient();
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            if (Journey.StartLocation != null)
            {
                Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(Journey.StartLocation.Longitude,Journey.StartLocation.Latitude),"S",true));
                StartCity = Journey.StartLocation.Address;
            }
            if (Journey.EndLocation != null)
            {
                Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(Journey.EndLocation.Longitude, Journey.EndLocation.Latitude), "F", false));
                EndCity = Journey.EndLocation.Address;
            }

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


        public async Task UpdateStartLocationAsync(Microsoft.Maps.MapControl.WPF.Location location)
        {
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations/"+location.Latitude+","+ location.Longitude+ "?key=AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr";
    

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseBody);
                if (data["resourceSets"][0]["resources"][0]["address"]["countryRegion"].Value<string>() != "Serbia" && data["resourceSets"][0]["resources"][0]["address"]["countryRegion"].Value<string>() != "Kosovo")
                {
                    MessageBox.Show("We only operate in Serbia, please select location in Serbia.");
                    StartCity = "";
                    StartLocationModel = null;

                    return;
                }
                StartLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), location.Latitude, location.Longitude);
                StartCity = StartLocationModel.Address;
                Journey.StartLocation = StartLocationModel;
                Locations.Add(new MapLocation(location, "S", true));

            }
            else
            {
                AddError("Query is not valid", nameof(StartCity));
            }
            
        }

        public async Task UpdateEndLocationAsync(Microsoft.Maps.MapControl.WPF.Location location)
        {
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations/" + location.Latitude + "," + location.Longitude + "?key=AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr";


            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseBody);
                if (data["resourceSets"][0]["resources"][0]["address"]["countryRegion"].Value<string>() != "Serbia" && data["resourceSets"][0]["resources"][0]["address"]["countryRegion"].Value<string>() != "Kosovo")
                {
                    MessageBox.Show("We only operate in Serbia, please select location in Serbia.");
                    EndCity = "";
                    EndLocationModel = null;

                    return;
                }
                EndLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), location.Latitude, location.Longitude);
                EndCity = EndLocationModel.Address;
                Journey.EndLocation = EndLocationModel;
                Locations.Add(new MapLocation(location, "F", false));
            }
            else
            {
                AddError("Query is not valid", nameof(StartCity));
            }


        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        internal void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

    }
    public class MapLocation
    {
        public Microsoft.Maps.MapControl.WPF.Location Location { get; set; }
        public string Name { get; set; }
        public bool IsStart { get; set; }

        public MapLocation(Microsoft.Maps.MapControl.WPF.Location location, string name, bool isStart)
        {
            Location = location;
            Name = name;
            IsStart = isStart;
        }
    }
}
