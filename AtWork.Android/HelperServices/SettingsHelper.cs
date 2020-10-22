using System;
using System.Threading.Tasks;
using Android.Content;
using AtWork.Droid.HelperServices;
using AtWork.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(SettingsHelper))]
namespace AtWork.Droid.HelperServices
{
    public class SettingsHelper : ISettingsHelper
    {
        public async Task OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            string package_name = AppInfo.PackageName;//"com.atlasics.atwork";
            var uri = Android.Net.Uri.FromParts("package", package_name, null);
            intent.SetData(uri);
            Application.Context.StartActivity(intent);
        }
    }
}
