using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class LocationListItemViewModel : ViewModelBase
    {
		#region properties

        public AccountStore accountStore;
        public Guid AttractionId;

		private string _location;
		public string Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
				OnPropertyChanged(nameof(Location));
			}
		}

		private Visibility _visibility;
		public Visibility Visibility
		{
			get
			{
				return _visibility;
			}
			set
			{
                _visibility = value;
				OnPropertyChanged(nameof(Visibility));
			}
		}

		#endregion

		public ICommand ShowAttractionCommand { get; }
		public LocationListItemViewModel(string location, Guid? id)
        {
            accountStore = App.host.Services.GetService<AccountStore>();
            ShowAttractionCommand = new ShowAttractionCommand(this);
            Location = location;
            if (id != null)
            {
				AttractionId = id.Value;
                Visibility = Visibility.Visible;
            }
            else
            {
				Visibility = Visibility.Collapsed;
            }
        }
    }
}
