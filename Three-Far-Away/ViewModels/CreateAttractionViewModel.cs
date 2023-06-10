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
    public class CreateAttractionViewModel: ViewModelBase,INotifyDataErrorInfo
    {
        public Attraction _attraction { get; set; }
        public Attraction Attraction
        {
            get
            {
                return _attraction;
            }
            set
            {
                _attraction = value;
                OnPropertyChanged(nameof(Attraction));
            }
        }
        public readonly IAttractionService _attractionService;

        public string AttractionImage
        {
            get
            {
                return _attraction.Image;
            }
            set
            {
                _attraction.Image = value;
                OnPropertyChanged(nameof(AttractionImage));
            }
        }
        
        public string Name
        {
            get
            {
                return _attraction.Name;
            }
            set
            {
                _attraction.Name = value;
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

        public string Description
        {
            get
            {
                return _attraction.Description;
            }
            set
            {
                _attraction.Description = value;
                OnPropertyChanged(nameof(Description));

                ClearErrors(nameof(Description)); ;
                if (Description.Length <= 10)
                {
                    AddError("Description cannot be shorter than 10 characters!", nameof(Description));
                }
                else if (Description.Length > 250)
                {
                    AddError("Description cannot be longer than 250 characters!", nameof(Description));
                }
            }
        }

        public AttractionType AttractionType
        {
            get
            {
                return _attraction.Type;
            }
            set
            {
                _attraction.Type = value;
                OnPropertyChanged(nameof(AttractionType));
            }
        }

        private string _uploadedImageName;
        public string UploadedImageName
        {
            get
            {
                return _uploadedImageName;
            }
            set
            {
                _uploadedImageName = value;
                OnPropertyChanged(nameof(UploadedImageName));
            }
        }
        public ICommand NavigateToCreateAttractionMapCommand { get; }
        public ICommand UploadImageCommand { get; }

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

        public CreateAttractionViewModel(Attraction attraction)
        {
            Attraction = attraction;
            _attractionService = App.host.Services.GetService<IAttractionService>();
            NavigateToCreateAttractionMapCommand = new NavigateToCreateAttractionCommand(this, "Home", "Map");

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            UploadImageCommand = new UploadAttractionImageCommand(this);
            if (attraction.Image != "")
            {
                _uploadedImageName = "image.jpg";
            }
            else
            {
                _uploadedImageName = "No file chosen";
            }
        }


    }
}
