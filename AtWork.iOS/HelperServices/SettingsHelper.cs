using System;
using System.Threading.Tasks;
using AtWork.iOS.HelperServices;
using AtWork.Services;
using Foundation;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsHelper))]
namespace AtWork.iOS.HelperServices
{
    public class SettingsHelper : ISettingsHelper
    {
        public async Task OpenAppSettings()
        {
            string app_bundle_id = AppInfo.PackageName;//"com.atlasics.atwork";
            var url = new NSUrl($"app-settings:{app_bundle_id}");
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}
