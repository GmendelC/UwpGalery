using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;

namespace UWPUtiles
{
    public class Camera
    {
        static public async Task<IStorageItem> GetImage()
        {
            CameraCaptureUI cam = new CameraCaptureUI();
            cam.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            cam.PhotoSettings.CroppedAspectRatio = new Size(250, 150);
            IStorageItem imageFile;

            try
            {
                StorageFile imageCameraFile = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);
                imageFile = await ProgramStorageFile.SetFile(imageCameraFile);
            }

            catch (Exception e)
            {
                Toast.SendToToast(e);
                imageFile = null;
            }
            return imageFile;
        }
    }
}
