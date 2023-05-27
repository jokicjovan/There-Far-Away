using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Components;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class AgentJourneyPreviewViewModel : ViewModelBase
    {
        #region services
        public readonly IJourneyService journeyService;

        #endregion


        #region properties

        private AgentNavigationBarViewModel _agentNavigationBarViewModel;
        public AgentNavigationBarViewModel AgentNavigationBarViewModel
        {
            get { return _agentNavigationBarViewModel; }
            set
            {
                _agentNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentNavigationBarViewModel));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _startDate;
        public string StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private string _endDate;
        public string EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        #endregion

        public AgentJourneyPreviewViewModel(Guid id)
        {
            AgentNavigationBarViewModel = App._host.Services.GetService<AgentNavigationBarViewModel>();
            this.journeyService = App._host.Services.GetService<IJourneyService>();
            Journey journey = journeyService.Read(id);
            Name = journey.Name;
            StartDate = journey.StartDate.ToString().Split(" ")[0];
            EndDate = journey.EndDate.ToString().Split(" ")[0];
            Price = journey.Price.ToString();
        }
    }
}
