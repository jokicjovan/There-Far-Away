using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Three_Far_Away.Commands;
using Three_Far_Away.Events;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class JourneyCardViewModel : ViewModelBase
    {
        public readonly AccountStore accountStore;
        public readonly IJourneyService journeyService;

		public Guid JourneyId { get; set; }
        public Role role;
        public ICommand ViewJourneyPreviewCommand { get; }
        public ICommand NavigateEditJourneyCommand { get; }
        public ICommand DeleteJourneyFromCardCommand { get; }


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

        private Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get
            {
                return _menuVisibility;
            }
            set
            {
                _menuVisibility = value;
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }


        public event EventHandler JourneyDeletedEvent;
        public virtual void OnJourneyDeletedEvent()
		{
            JourneyDeletedEvent?.Invoke(this, EventArgs.Empty);
		}

		public JourneyCardViewModel(JourneyForCard journey)
        {
            JourneyId = journey.Id;
            Name = journey.Name;
			Date = journey.Date;
			Price = journey.Price;
            journeyService = App.host.Services.GetService<IJourneyService>();
            accountStore = App.host.Services.GetService<AccountStore>();
            role = accountStore.Role;

			if (accountStore.Role.Equals(Role.AGENT))
			{
                MenuVisibility = Visibility.Visible;
            }
			else
			{
                MenuVisibility = Visibility.Hidden;
            }

            ViewJourneyPreviewCommand = new ViewJourneyPreviewCommand(this);
            DeleteJourneyFromCardCommand = new DeleteJourneyFromCardCommand(this);
        }
	}
}

