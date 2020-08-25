using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AtWork.Droid.HelperServices;
using AtWork.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(HelperService))]
namespace AtWork.Droid.HelperServices
{
    public class HelperService : IHelper
    {
        public async void RetriveImageFromLocation(string location, Image image)
        {
            var memoryStream = new MemoryStream();

            using (var source = System.IO.File.OpenRead(location))
            {
                await source.CopyToAsync(memoryStream);
            }

            image.Source = ImageSource.FromStream(() => memoryStream);
        }

        public Task SaveImageFile(Stream StreamToWrite, string originalPathToReplace)
        {
            throw new NotImplementedException();
        }
    }
}
