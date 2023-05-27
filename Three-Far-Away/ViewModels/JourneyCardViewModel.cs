using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class JourneyCardViewModel : ViewModelBase
    {
        public readonly IJourneyService journeyService;
		public Guid JourneyId { get; set; }
        public Role role;
        public ICommand ViewJourneyPreviewCommand { get; }
        public readonly INavigationService<AgentJourneyPreviewViewModel> navigationAgentJourneyPreview;
        public readonly INavigationService<ClientJourneyPreviewViewModel> navigationClientJourneyPreview;

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

		private string _date;
		public string Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
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

        public JourneyCardViewModel(JourneyForCard journey)
        {
            JourneyId = journey.Id;
            Name = journey.Name;
			Date = journey.Date;
			Price = journey.Price;
            AccountStore accountStore = App._host.Services.GetService<AccountStore>();
            role = accountStore.Role;
			if(accountStore.Role.Equals(Role.AGENT))
                navigationAgentJourneyPreview =
                    new NavigationService<AgentJourneyPreviewViewModel>(App._host.Services.GetService<NavigationStore>(),
                        () => new AgentJourneyPreviewViewModel(JourneyId));
			else
                navigationClientJourneyPreview =
                    new NavigationService<ClientJourneyPreviewViewModel>(App._host.Services.GetService<NavigationStore>(),
                        () => new ClientJourneyPreviewViewModel(JourneyId));
            ViewJourneyPreviewCommand = new ViewJourneyPreviewCommand(this);

        }
	}
}

