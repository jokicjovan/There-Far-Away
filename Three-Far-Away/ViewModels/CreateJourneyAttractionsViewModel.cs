using GongSolutions.Wpf.DragDrop;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    class CreateJourneyAttractionsViewModel : ViewModelBase,IDropTarget
    {
        public readonly IJourneyService _journeyService;


        public ObservableCollection<Attraction> AllAttractions { get; private set; }
        public ObservableCollection<Attraction> SelectedAttractions { get; private set; }

        public CreateJourneyAttractionsViewModel(IJourneyService journeyService)
        {
            _journeyService = journeyService;
            AllAttractions = new ObservableCollection<Attraction>();
            AllAttractions.Add(new Attraction());
            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

            AllAttractions.Add(new Attraction());

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
