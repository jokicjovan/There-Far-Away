using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;
using System.Net.Http;
using Microsoft.Maps.MapControl.WPF;

namespace Three_Far_Away.Commands
{
    class SearchEndLocationCommand : CommandBase
    {
        private readonly CreateJourneyMapViewModel _createJourneyMapViewModel;
        private HttpClient httpClient;
        public SearchEndLocationCommand(CreateJourneyMapViewModel createJourneyMapViewModel)
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
                {"query",_createJourneyMapViewModel.EndCity },
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
                    _createJourneyMapViewModel.AddError("No results", nameof(_createJourneyMapViewModel.EndCity));
                    return;
                }
                _createJourneyMapViewModel.EndLocationModel = new Models.Location(data["resourceSets"][0]["resources"][0]["address"]["formattedAddress"].Value<string>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][0].Value<Double>(), data["resourceSets"][0]["resources"][0]["point"]["coordinates"][1].Value<Double>());

                bool hasStart = false;
                foreach (MapLocation item in _createJourneyMapViewModel.Locations)
                {
                    if (!item.IsStart)
                    {
                        _createJourneyMapViewModel.Locations.Remove(item);
                        Location location = new Location(_createJourneyMapViewModel.EndLocationModel.Longitude, _createJourneyMapViewModel.EndLocationModel.Latitude);
                        _createJourneyMapViewModel.Locations.Add(new MapLocation(location, "F", false));
                        _createJourneyMapViewModel.UpdateEndLocationAsync(location);
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Location location = new Location(_createJourneyMapViewModel.EndLocationModel.Longitude, _createJourneyMapViewModel.EndLocationModel.Latitude);
                    _createJourneyMapViewModel.Locations.Add(new MapLocation(location, "F", false));
                    _createJourneyMapViewModel.UpdateEndLocationAsync(location);
                }
                _createJourneyMapViewModel.EndCity = _createJourneyMapViewModel.EndLocationModel.Address;
            }
            else
            {
                _createJourneyMapViewModel.AddError("Query is not valid", nameof(_createJourneyMapViewModel.EndCity));
            }
        }
    }
}
