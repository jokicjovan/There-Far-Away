using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class CreateJourneyViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        public readonly IJourneyService _journeyService;
		
        private Journey _journey;
        public Journey Journey
        {
            get
            {
                return _journey;
            }
            set
            {
                _journey = value;
                OnPropertyChanged(nameof(Journey));
            }
        }
        public string Name
        {
            get
            {
                return Journey.Name;
            }
            set
            {
                Journey.Name = value;
                OnPropertyChanged(nameof(Name));

                ClearErrors(nameof(Name)); ;
                if (Name.Length <= 2)
                {
                    AddError("Name cannot be shorter than 2 characters!", nameof(Name));
                }
                else if (Name.Length > 30)
                {
                    AddError("Name cannot be longer than 30 characters!", nameof(Name));
                }
            }
        }

		public DateTime StartDate
        {
			get
			{
				return Journey.StartDate;
			}
			set
			{
				Journey.StartDate = value;
				OnPropertyChanged(nameof(StartDate));
                ClearErrors(nameof(StartDate)); ;
                if (StartDate<=DateTime.Now)
                {
                    AddError("Start Time Cannot Be In Past", nameof(StartDate));
                }
            }
		}
		public DateTime EndDate
		{
			get
			{
				return Journey.EndDate;
			}
			set
			{
				Journey.EndDate = value;
				OnPropertyChanged(nameof(EndDate));
                ClearErrors(nameof(EndDate)); ;
                if (EndDate <= StartDate)
                {
                    AddError("End Time Cannot Be Before Start Time", nameof(EndDate));
                }
            }
		}
		public Double Price
		{
			get
			{
				return Journey.Price;
			}
			set
			{
				Journey.Price = value;
				OnPropertyChanged(nameof(Price));
                ClearErrors(nameof(Price));
                if (Price<0)
                {
                    AddError("Price Cannot Be Lower Than 0", nameof(Price));
                }
            }
		}

		public TransportationType Transportation
		{
			get
			{
				return Journey.Transportation;
			}
			set
			{
				Journey.Transportation = value;
				OnPropertyChanged(nameof(Transportation));
			}
		}


        public ObservableCollection<TransportationType> Transporations { get; private set; }

        public ICommand CreateJourneyCommand { get; }
        public ICommand NavigateToCreateJourneyMapCommand { get; }

        #region errors
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        #endregion

        public CreateJourneyViewModel(Journey journey)
        {
            Journey = journey;
            _journeyService = App.host.Services.GetService<IJourneyService>();
            NavigateToCreateJourneyMapCommand = new NavigateToCreateJourneyCommand(this,"Home","Map");

			Transporations=new ObservableCollection<TransportationType>();
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }


    }
}
