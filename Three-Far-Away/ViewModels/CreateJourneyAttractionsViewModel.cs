using GongSolutions.Wpf.DragDrop;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    class CreateJourneyAttractionsViewModel : ViewModelBase,IDropTarget
    {
        public readonly IJourneyService _journeyService;

        public ObservableCollection<MapLocation> Locations { get; private set; }

        public ObservableCollection<Attraction> AllAttractions { get; private set; }
        public ObservableCollection<Attraction> SelectedAttractions { get; private set; }

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
            _journeyService = App.host.Services.GetService<IJourneyService>();
            AllAttractions = new ObservableCollection<Attraction>();
            SelectedAttractions = new ObservableCollection<Attraction>();
            AllAttractions.Add(new Attraction("Žičа","Najbolji manastir ikad",AttractionType.ATTRACTION,"",new Location("Nepoznata Adresa",48,49)));
            AllAttractions.Add(new Attraction("Studenica", "Najbolji manastir ikad", AttractionType.ATTRACTION, "", new Location("Nepoznata Adresa", 49, 49)));
            AllAttractions.Add(new Attraction("Decani", "Najbolji manastir ikad", AttractionType.ATTRACTION, "", new Location("Nepoznata Adresa", 20, 49)));
            Locations = new ObservableCollection<MapLocation>();
            NavigateToCreateJourneyMapCommand = new NavigateToCreateJourneyCommand(this,"Attractions","Map");
            AllAttractionsView = CollectionViewSource.GetDefaultView(AllAttractions);
            AllAttractionsView.Filter = o => String.IsNullOrEmpty(FilterAll) ? true : ((Attraction)o).Name.ToLower().Contains(FilterAll.ToLower());
            SelectedAttractionsView = CollectionViewSource.GetDefaultView(SelectedAttractions);
            SelectedAttractionsView.Filter = o => String.IsNullOrEmpty(FilterSelected) ? true : ((Attraction)o).Name.ToLower().Contains(FilterSelected.ToLower());
            Journey = journey;

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
