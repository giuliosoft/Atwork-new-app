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
            return $"/{nameof(NavigationPage)}/{nameof(StartUpPage)}";
        }
    }
}
