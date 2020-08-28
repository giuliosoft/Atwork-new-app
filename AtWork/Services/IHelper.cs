using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AtWork.Services
{
    public interface IHelper
    {
        void RetriveImageFromLocation(string location, Image image);
        Task<string> SaveImageFile(Stream StreamToWrite, string originalPathToReplace);
        Task ReplaceCroppedFile(string pathFromReplace, string pathToReplace);
    }
}
