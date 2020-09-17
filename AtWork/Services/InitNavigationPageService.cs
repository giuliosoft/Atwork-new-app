using System;
using AtWork.Views;
using Xamarin.Forms;

namespace AtWork.Services
{
    public class InitNavigationPageService
    {
        /// <summary>
        /// Navigate
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static string Navigate()
        {
            if (SettingsService.LoggedInUserData != null && SettingsService.VolunteersUserData != null)
            {
                LayoutService.ConvertThemeAsPerSettings();
                if (SettingsService.VolunteersUserData?.volOnBoardStatus.ToLower() == "complete" && SettingsService.VolunteersUserData?.volStatus.ToLower() == "active")
                    return $"/{nameof(NavigationPage)}/{nameof(DashboardPage)}";
                else
                    return $"/{nameof(NavigationPage)}/{nameof(WelcomeSetupPage)}";
            }
            else
            {
                return $"/{nameof(NavigationPage)}/{nameof(StartUpPage)}";
            }

        }
    }
}
