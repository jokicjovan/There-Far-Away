using GongSolutions.Wpf.DragDrop;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class CreateJourneyAttractionsViewModel : ViewModelBase,IDropTarget
    {
        public readonly IJourneyService _journeyService;
        public readonly IAttractionService _attractionService;

        public ObservableCollection<MapLocation> Locations { get; private set; }

        public ObservableCollection<Attraction> AllAttractions { get; private set; }
        private ObservableCollection<Attraction> _selectedAttractions;

        public ObservableCollection<Attraction> SelectedAttractions
        {
            get { return _selectedAttractions; }
            set
            {
                if (_selectedAttractions != value)
                {
                    _selectedAttractions = value;
                    OnPropertyChanged(nameof(SelectedAttractions));
                }
            }
        }

        public ICollectionView AllAttractionsView;
        
        public ICollectionView SelectedAttractionsView;

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

        public ICommand NavigateToCreateJourneyMapCommand { get; }
        
        public ICommand CreateJourneyCommand { get; }

        private string _filterAll;
        public string FilterAll
        {
            get
            {
                return _filterAll;
            }
            set
            {
                _filterAll = value;
                OnPropertyChanged(nameof(FilterAll));
                ApplyFilterAll();
            }
        }
        private string _filterSelected;
        public string FilterSelected
        {
            get
            {
                return _filterSelected;
            }
            set
            {
                _filterSelected = value;
                OnPropertyChanged(nameof(FilterSelected));
                ApplyFilterSelected();
            }
        }
        public CreateJourneyAttractionsViewModel(Journey journey)
        {
            Journey = journey;
            _journeyService = App.host.Services.GetService<IJourneyService>();
            _attractionService = App.host.Services.GetService<IAttractionService>();
            AllAttractions = new ObservableCollection<Attraction>(_attractionService.ReadAll());
            SelectedAttractions = new ObservableCollection<Attraction>();
            Locations = new ObservableCollection<MapLocation>();
            AllAttractionsView = CollectionViewSource.GetDefaultView(AllAttractions);
            AllAttractionsView.Filter = o => String.IsNullOrEmpty(FilterAll) ? true : ((Attraction)o).Name.ToLower().Contains(FilterAll.ToLower());
            SelectedAttractionsView = CollectionViewSource.GetDefaultView(SelectedAttractions);
            SelectedAttractionsView.Filter = o => String.IsNullOrEmpty(FilterSelected) ? true : ((Attraction)o).Name.ToLower().Contains(FilterSelected.ToLower());
            SelectedAttractionsView.CollectionChanged += AttractionsViewChanged;
            if (journey.Attractions != null)
            {
                foreach (Attraction attraction in journey.Attractions)
                {
                    SelectedAttractions.Add(attraction);
                    AllAttractions.Remove(attraction);
                }

            }
            CreateJourneyCommand = new CreateJourneyCommand(this);
            NavigateToCreateJourneyMapCommand = new NavigateToCreateJourneyCommand(this, "Attractions", "Map");


        }

        private void AttractionsViewChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Journey.Attractions= SelectedAttractionsView.OfType<Attraction>().ToList();
            OnPropertyChanged("Attractions");
        }

        void ApplyFilterAll()
        {
            AllAttractions = new ObservableCollection<Attraction>(AllAttractions.Where(item => item.Name.ToLower().Contains(FilterAll.ToLower())));
            AllAttractionsView.Refresh();
        }

        void ApplyFilterSelected()
        {
            SelectedAttractions = new ObservableCollection<Attraction>(SelectedAttractions.Where(item => item.Name.ToLower().Contains(FilterSelected.ToLower())));
            SelectedAttractionsView.Refresh();
            
        }


        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            AllAttractionsModel sourceItem = dropInfo.Data as AllAttractionsModel;
            AllAttractionsModel targetItem = dropInfo.TargetItem as AllAttractionsModel;

            if (sourceItem != null && targetItem != null )
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }
            
            
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            AllAttractionsModel sourceItem = (AllAttractionsModel)dropInfo.Data;
            AllAttractionsModel targetItem = (AllAttractionsModel)dropInfo.TargetItem;
            targetItem.Attractions.Add(sourceItem);
        }
    }

    class AllAttractionsModel
    {
        public ObservableCollection<AllAttractionsModel> Attractions { get; private set; }
    }
}
