using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    internal class CreateAttractionMapViewModel:ViewModelBase
    {
        public Attraction _attraction { get; set; }
        public Attraction Attraction
        {
            get
            {
                return _attraction;
            }
            set
            {
                _attraction = value;
                OnPropertyChanged(nameof(Attraction));
            }
        }
        public readonly IAttractionService _attractionService;
        private HttpClient httpClient;
        public ObservableCollection<MapLocation> Locations { get; private set; }

        public ICommand TurnOnLocationCommand { get; }

        public ICommand SearchStartLocationCommand { get; }
        public ICommand SearchEndLocationCommand { get; }
        public ICommand NavigateToCreateJourneyCommand { get; }
        public ICommand CreateAttractionCommand { get; }

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
                return _attraction.Location;
            }
            set
            {
                _attraction.Location = value;
                OnPropertyChanged(nameof(StartLocationModel));

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


        public CreateAttractionMapViewModel(Attraction attraction)
        {
            Attraction = attraction;
            _attractionService = App.host.Services.GetService<IAttractionService>();
            Locations = new ObservableCollection<MapLocation>();
            TurnOnLocationCommand = new TurnOnAttractionLocationPinCommand(this);
            SearchStartLocationCommand = new SearchAttractionLocationCommand(this);
            CreateAttractionCommand= new CreateAttractionCommand(this);
            NavigateToCreateJourneyCommand = new NavigateToCreateAttractionCommand(this, "Map", "Home");
            httpClient = new HttpClient();
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            if (_attraction.Location != null)
            {
                Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(_attraction.Location.Longitude, _attraction.Location.Latitude), "S", true));
                StartCity = _attraction.Location.Address;
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
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations/" + location.Latitude + "," + location.Longitude + "?key=AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr";


            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseBody);
                StartLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), location.Latitude, location.Longitude);
                StartCity = StartLocationModel.Address;
                _attraction.Location = StartLocationModel;
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
}
