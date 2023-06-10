using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class UploadAttractionImageCommand:CommandBase
    {
        private readonly CreateAttractionViewModel _createAttractionViewModel;
        public UploadAttractionImageCommand(CreateAttractionViewModel createAttractionViewModel)
        {
            _createAttractionViewModel = createAttractionViewModel;
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
                _createAttractionViewModel.UploadedImageName = fileDialog.SafeFileName;
                _createAttractionViewModel.AttractionImage = base64ImageRepresentation;
            }
        }
    }
}
