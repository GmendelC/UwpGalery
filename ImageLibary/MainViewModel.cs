using ImageLibary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using UWPUtiles; // statics methodos

namespace ImageLibary
{
    public class MainViewModel
    {
        public ObservableCollection<ImageModel> Images { get; set; }
        public ImageModel Selected { get; set; }
        private ProgramLocation _locationString;

        private event EventHandler _onNavigateToGalery;
        public event EventHandler OnNavigateToGalery { add { _onNavigateToGalery += value; } remove { _onNavigateToGalery -= value; } }

        private event EventHandler _onNavigateToEdit;
        public event EventHandler OnNavigateToEdit { add { _onNavigateToEdit += value; } remove { _onNavigateToEdit -= value; } }

        public MainViewModel()
        {
            Images = new ObservableCollection<ImageModel>();
            _locationString = ProgramLocation.Create();
        }
        public void NavigateToGalery()
        {
            _onNavigateToGalery?.Invoke(this, null);
        }

        public void NavigateToEdit()
        {
            _onNavigateToEdit?.Invoke(this, null);
        }

        public async void AddFromCamera()
        {
            IStorageItem imageFile = await Camera.GetImage();

            if (imageFile != null)
            {
                AddNewImage(imageFile,"Camera");
                NavigateToEdit();
            }
        }

        public async void AddDrag(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            IStorageItem localFile = await ProgramStorageFile.GetImage(e);
            if (localFile != null)
            {
                AddNewImage(localFile);
                NavigateToEdit();
            }
        }

        private void AddNewImage(IStorageItem localFile, string name = null)
        {
            ImageModel newImage = new ImageModel { Name = name == null? localFile.Name: name, Paht = localFile.Path, Location = _locationString.TextLocation };
            Images.Add(newImage);
            Selected = newImage;
        }

        public async void EditDrag(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            IStorageItem localFile = await ProgramStorageFile.GetImage(e);
            if (localFile != null)
            {
                Selected.Paht = localFile.Path;
            }
        }

        public async void DragOver(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
           // if (await ProgramStorageFile.IsImage(e))
            {
                e.AcceptedOperation = DataPackageOperation.Copy;
            }
        }

        public void SaveImage()
        {
            bool exist = Images.FirstOrDefault(X => X == Selected) != null;

            if (!exist)
            {
                Images.Add(Selected);
            }

            NavigateToGalery();
        }

        public async void DeleteImage()
        {

            if (Selected != null)
            {

            bool delete = await ProgramMessageDialog.YesNoDialog(
                 Selected.Name , "You Want to delete?");

            if (delete)
            {
                Images.Remove(Selected);
                Selected = null;
            }

            NavigateToGalery();
            }
        }
    }
}
