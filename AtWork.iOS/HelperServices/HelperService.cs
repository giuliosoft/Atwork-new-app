using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AtWork.iOS.HelperServices;
using AtWork.Services;
using Photos;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(HelperService))]
namespace AtWork.iOS.HelperServices
{
    public class HelperService : IHelper
    {
        public void RetriveImageFromLocation(string location, Image image)
        {
            string[] str = location.Split('/');

            PHFetchResult assetResult = PHAsset.FetchAssetsUsingLocalIdentifiers(str, null);

            PHAsset asset = assetResult.firstObject as PHAsset;

            PHImageManager.DefaultManager.RequestImageData(asset, null, (data, dataUti,
            orientation, info) =>
            {
                var stream = data.AsStream();
                image.Source = ImageSource.FromStream(() => stream);
            });
        }

        public async Task<string> SaveImageFile(Stream StreamToWrite, string originalPathToReplace)
        {
            string croppedImgFilePath = string.Empty;
            try
            {
                byte[] imgByteData = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    StreamToWrite.CopyTo(ms);
                    imgByteData = ms.ToArray();
                }

                string FileName = "AtWorkImg.jpg";
                var Docdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var directory = Docdirectory + "/AtWork";

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var path = Path.Combine(directory, FileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllBytes(path, imgByteData);
                Debug.WriteLine("Cropped File Save Path=== " + path);
                //File.Replace(path, originalPathToReplace, "oldfile");
                croppedImgFilePath = path;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return croppedImgFilePath;
        }

        public async Task ReplaceCroppedFile(string pathFromReplace, string pathToReplace)
        {
            try
            {
                Debug.WriteLine("Crop File pathFromReplace=== " + pathFromReplace);
                Debug.WriteLine("Crop File pathToReplace=== " + pathToReplace);
                File.Replace(pathFromReplace, pathToReplace, "oldfile");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
