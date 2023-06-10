using System;
using System.Windows;
using Three_Far_Away.Views;

namespace Three_Far_Away.Commands
{
    public class DeleteAttractionFromCardCommand : CommandBase
    {
        private readonly AttractionCardViewModel _attractionCardViewModel;
        public DeleteAttractionFromCardCommand(AttractionCardViewModel attractionCardViewModel)
        {
            _attractionCardViewModel = attractionCardViewModel;

        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }


        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this attraction?", "Delete Confirmation", MessageBoxButton.YesNo);


            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _attractionCardViewModel.attractionService.Delete(_attractionCardViewModel.AttractionId);
                _attractionCardViewModel.OnAttractionDeletedEvent();
            }
        }
    }
}
