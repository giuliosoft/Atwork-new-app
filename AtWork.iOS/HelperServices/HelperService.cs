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

        public async Task<Stream> RetriveCompressedImageStreamFromLocation(string location)
        {
            Stream imgStream = null;
            try
            {
                //FileStream fs = new FileStream(location, FileMode.Open, FileAccess.Read);
                //StreamReader streamReader = new StreamReader(fs);
                //imgStream = streamReader.BaseStream;
                byte[] imageAsBytes = File.ReadAllBytes(location);
                UIKit.UIImage images = new UIKit.UIImage(Foundation.NSData.FromArray(imageAsBytes));
                byte[] bytes = images.AsJPEG(0.5f).ToArray();
                Stream imgStreamFromByte = new MemoryStream(bytes);
                imgStream = imgStreamFromByte;
                return imgStream;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return imgStream;
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

        public async Task<string> SaveProfileImage(Stream StreamToWrite)
        {
            string profileImgFilePath = string.Empty;
            try
            {
                byte[] imgByteData = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    StreamToWrite.CopyTo(ms);
                    imgByteData = ms.ToArray();
                }

                Guid uniqueId = Guid.NewGuid();
                string FileName = $"{uniqueId}{".jpg"}";
                var Docdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var directory = Docdirectory + "/AtWorkImages";

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
                Debug.WriteLine("Profile Image Save Path=== " + path);
                profileImgFilePath = path;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return profileImgFilePath;
        }

        public async Task ReplaceCroppedFile(string pathFromReplace, string pathToReplace)
        {
            try
            {
                Debug.WriteLine("Crop File pathFromReplace=== " + pathFromReplace);
                Debug.WriteLine("Crop File pathToReplace=== " + pathToReplace);
                File.Replace(pathFromReplace, pathToReplace, "oldfile");//"oldfile.jpg.bac"
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task MoveCroppedFile(string pathFromMove, string pathToMove)
        {
            try
            {
                Debug.WriteLine("Crop File pathFromMove=== " + pathFromMove);
                Debug.WriteLine("Crop File pathToMove=== " + pathToMove);
                if (File.Exists(pathToMove))
                {
                    File.Delete(pathToMove);
                }
                File.Move(pathFromMove, pathToMove);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
