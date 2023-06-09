using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Maps.MapControl.WPF;

namespace Three_Far_Away.Commands
{
    internal class SearchStartLocationCommand : CommandBase
    {
        private readonly CreateJourneyMapViewModel _createJourneyMapViewModel;
        private HttpClient httpClient;
        public SearchStartLocationCommand(CreateJourneyMapViewModel createJourneyMapViewModel)
        {
            _createJourneyMapViewModel = createJourneyMapViewModel;
            httpClient = new HttpClient();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            string apiUrl = "http://dev.virtualearth.net/REST/v1/Locations";

            // Create a dictionary to hold your request parameters
            var parameters = new Dictionary<string, string>
            {
                {"query",_createJourneyMapViewModel.StartCity },
                { "maxResults", "1" },
                { "key", "AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr" }
            };

            string queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));

            apiUrl = $"{apiUrl}?{queryString}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var data = (JObject)JsonConvert.DeserializeObject(responseBody);
            _createJourneyMapViewModel.StartLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][0].Value<Double>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][1].Value<Double>());

            bool hasStart = false;
            foreach (MapLocation item in _createJourneyMapViewModel.Locations)
            {
                if (item.IsStart)
                {
                    _createJourneyMapViewModel.Locations.Remove(item);
                    Location location = new Location(_createJourneyMapViewModel.StartLocationModel.Longitude,_createJourneyMapViewModel.StartLocationModel.Latitude);
                    _createJourneyMapViewModel.Locations.Add(new MapLocation(location, "S", true));
                    _createJourneyMapViewModel.UpdateStartLocationAsync(location);
                    hasStart = true;
                    break;
                }
            }
            if (!hasStart)
            {
                Location location = new Location(_createJourneyMapViewModel.StartLocationModel.Longitude, _createJourneyMapViewModel.StartLocationModel.Latitude);
                _createJourneyMapViewModel.Locations.Add(new MapLocation(location, "S", true));
                _createJourneyMapViewModel.UpdateStartLocationAsync(location);
            }
            _createJourneyMapViewModel.StartCity = _createJourneyMapViewModel.StartLocationModel.Address;

        }
    }
}
