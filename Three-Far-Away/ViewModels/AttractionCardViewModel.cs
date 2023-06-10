using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Three_Far_Away.Commands;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Views
{
    public class AttractionCardViewModel : ViewModelBase
    {
        public readonly AccountStore accountStore;
        public readonly IAttractionService attractionService;

        public Guid AttractionId { get; set; }
        public Role role;
        public ICommand ViewAttractionPreviewCommand { get; }
        public ICommand NavigateEditAttractionCommand { get; }
        public ICommand DeleteAttractionFromCardCommand { get; }


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

        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
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


        public event EventHandler AttractionDeletedEvent;
        public virtual void OnJourneyDeletedEvent()
        {
            AttractionDeletedEvent?.Invoke(this, EventArgs.Empty);
        }

        public AttractionCardViewModel(Attraction attraction)
        {
            AttractionId = attraction.Id;
            Name = attraction.Name;
            Type = attraction.Type.ToString().ToLower();
            Type = char.ToUpper(Type[0]) + Type.Substring(1);
            attractionService = App.host.Services.GetService<IAttractionService>();
            accountStore = App.host.Services.GetService<AccountStore>();
            role = accountStore.Role;

            if (accountStore.Role.Equals(Role.AGENT))
            {
                MenuVisibility = Visibility.Visible;
            }
            else
            {
                MenuVisibility = Visibility.Collapsed;
            }

            ViewAttractionPreviewCommand = new ViewAttractionPreviewCommandFromAttractionCardCommand(this);
            // DeleteAttractionFromCardCommand = new DeleteJourneyFromCardCommand(this);
            // NavigateEditAttractionCommand = new NavigateToCreateJourneyCommand(journeyService.Read(journey.Id));
        }
    }
}
