using System.Windows.Input;
using Three_Far_Away.Commands;

namespace Three_Far_Away.ViewModels
{
    public class ImagePreviewViewModel : ViewModelBase
    {

        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }


        public ICommand CloseDialogCommand { get; }

        public ImagePreviewViewModel(string base64ImageRepresentation)
        {
            Image = base64ImageRepresentation;
            CloseDialogCommand = new CloseDialogCommand();
        }
    }
}
