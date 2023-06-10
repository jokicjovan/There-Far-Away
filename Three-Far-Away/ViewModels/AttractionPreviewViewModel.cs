using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class AttractionPreviewViewModel : ViewModelBase
    {
        #region services

        public readonly IAttractionService attractionService;

        #endregion

        #region stores

        public readonly AccountStore accountStore;

        #endregion

        #region commands
        public ICommand DeleteAttractionCommand { get; }
        public ICommand NavigateToEditAttractionCommand { get; }

        #endregion

        #region properties
        public ObservableCollection<MapLocation> Locations { get; private set; }

        private Guid _attractionId;
        public Guid AttractionId
        {
            get
            {
                return _attractionId;
            }
            set
            {
                _attractionId = value;
                OnPropertyChanged(nameof(AttractionId));
            }
        }

        private double _latitude;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(Latitude));
                OnPropertyChanged(nameof(Center));
            }
        }
        private double _longitude;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(Longitude));
                OnPropertyChanged(nameof(Center));

            }
        }

        private String _center;
        public String Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
                OnPropertyChanged(nameof(Center));
            }
        }


        private Boolean _isAgent;
        public Boolean IsAgent
        {
            get
            {
                return _isAgent;
            }
            set
            {
                _isAgent = value;
                OnPropertyChanged(nameof(IsAgent));
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

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _smallAddress;
        public string SmallAddress
        {
            get
            {
                return _smallAddress;
            }
            set
            {
                _smallAddress = value;
                OnPropertyChanged(nameof(SmallAddress));
            }
        }

        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        #endregion


        public AttractionPreviewViewModel(Guid id)
        {
            attractionService = App.host.Services.GetService <IAttractionService>();
            accountStore = App.host.Services.GetService<AccountStore>();
            if (accountStore.Role == Role.AGENT)
            {
                IsAgent = true;
            }
            else
            {
                IsAgent = false;
            }
            Attraction attraction = attractionService.FindWithLocation(id);;
            AttractionId = attraction.Id;
            Name = attraction.Name;
            Image = attraction.Image;
            Type = attraction.Type.ToString().ToLower();
            Type = char.ToUpper(Type[0]) + Type.Substring(1);
            Description = attraction.Description;
            Address = attraction.Location.Address;
            if (Address.Length > 20)
            {
                SmallAddress = attraction.Location.Address.Substring(0, 20) + "...";
            }
            else
            {
                SmallAddress = attraction.Location.Address.Substring(0, Address.Length);
            }
            Latitude = attraction.Location.Latitude;
            Longitude = attraction.Location.Longitude;
            Locations = new ObservableCollection<MapLocation>();
            Center = attraction.Location.Longitude.ToString().Replace(",", ".") + ", " + attraction.Location.Latitude.ToString().Replace(",", ".");
            Locations.Add(new MapLocation(
                new Microsoft.Maps.MapControl.WPF.Location(attraction.Location.Longitude, attraction.Location.Latitude),
                "S", true));

            NavigateToEditAttractionCommand = new NavigateToCreateAttractionCommand(attraction)
            DeleteAttractionCommand = new DeleteAttractionFromPreviewCommand(this);
        }
    }
}
