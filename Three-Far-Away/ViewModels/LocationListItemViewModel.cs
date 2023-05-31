using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;

namespace Three_Far_Away.ViewModels
{
    public class LocationListItemViewModel : ViewModelBase
    {
		#region properties

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

		#endregion
		public LocationListItemViewModel(string location)
        {
            Location = location;
        }
    }
}
