using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace UWPUtiles
{
    public class ProgramStorageFile
    {
        private static StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        public static async Task<IStorageItem> SetFile(StorageFile file)
        {
            return await file.CopyAsync(_localFolder);
        }

        public static async Task<IStorageItem> GetImage(Windows.UI.Xaml.DragEventArgs e)
        {
            IStorageItem res = null;

            if (await IsImage(e))
            {
                var files = await e.DataView.GetStorageItemsAsync();
                var storageImage = files[0] as StorageFile;

                res = await _localFolder.TryGetItemAsync(storageImage.Name);
                if (res == null)
                {
                    res = await storageImage.CopyAsync(_localFolder);
                }
            }
            return res;
        }

        public static async Task<bool> IsImage(Windows.UI.Xaml.DragEventArgs e)
        {
            var stores = await e.DataView.GetStorageItemsAsync();
            var image = stores[0] as StorageFile;
            return image != null && image.ContentType.Contains("image");
        }
    }
}
