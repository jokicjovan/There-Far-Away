using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace Three_Far_Away.Commands
{
    internal class SearchAttractionLocationCommand:CommandBase
    {
        private readonly CreateAttractionMapViewModel _createAttractionMapViewModel;
        private HttpClient httpClient;
        public SearchAttractionLocationCommand(CreateAttractionMapViewModel createJourneyMapViewModel)
        {
            _createAttractionMapViewModel = createJourneyMapViewModel;
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
                {"query",_createAttractionMapViewModel.StartCity },
                { "maxResults", "1" },
                { "key", "AnlBf0cuDvauQWCdsCnr3pZRaTRoYRRuPmabScrTtje7XUhhHmDDEz6aKfpX6wFr" }
            };

            string queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));

            apiUrl = $"{apiUrl}?{queryString}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var data = (JObject)JsonConvert.DeserializeObject(responseBody);
                if (data["resourceSets"][0]["estimatedTotal"].Value<int>() == 0)
                {
                    _createAttractionMapViewModel.AddError("No results", nameof(_createAttractionMapViewModel.StartCity));
                    return;
                }
                _createAttractionMapViewModel.StartLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][0].Value<Double>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][1].Value<Double>());

                bool hasStart = false;
                foreach (MapLocation item in _createAttractionMapViewModel.Locations)
                {
                    if (item.IsStart)
                    {
                        _createAttractionMapViewModel.Locations.Remove(item);
                        Location location = new Location(_createAttractionMapViewModel.StartLocationModel.Longitude, _createAttractionMapViewModel.StartLocationModel.Latitude);
                        _createAttractionMapViewModel.Locations.Add(new MapLocation(location, "S", true,""));
                        _createAttractionMapViewModel.UpdateStartLocationAsync(location);
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Location location = new Location(_createAttractionMapViewModel.StartLocationModel.Longitude, _createAttractionMapViewModel.StartLocationModel.Latitude);
                    _createAttractionMapViewModel.Locations.Add(new MapLocation(location, "S", true, ""));
                    _createAttractionMapViewModel.UpdateStartLocationAsync(location);
                }
                _createAttractionMapViewModel.StartCity = _createAttractionMapViewModel.StartLocationModel.Address;
            }
            else
            {
                _createAttractionMapViewModel.AddError("Query is not valid", nameof(_createAttractionMapViewModel.StartCity));
            }

        }
    }
}
