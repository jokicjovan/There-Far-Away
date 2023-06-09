using Microsoft.Win32;
using System;
using System.Windows;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class UploadJourneyImageCommand : CommandBase
    {
        private readonly CreateJourneyViewModel _createJourneyViewModel;
        public UploadJourneyImageCommand(CreateJourneyViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;
        }


        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            var dialogResult = fileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value != false)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(fileDialog.FileName);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                _createJourneyViewModel.UploadedImageName = fileDialog.SafeFileName;
                _createJourneyViewModel.JourneyImage = base64ImageRepresentation;
            }
        }
    }
}
