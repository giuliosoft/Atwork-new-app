using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AtWork.Services
{
    public class SettingsService
    {
        static ISettings appSettings;
        public static ISettings AppSettings
        {
            get
            {
                return appSettings ?? (appSettings = CrossSettings.Current);
            }
            set
            {
                appSettings = value;
            }
        }

        private const string IsAgreeWithTermsKey = "IsAgreeWithTerms_key";
        private static readonly bool IsAgreeWithTermsDefault = false;
        public static bool IsAgreeWithTerms
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsAgreeWithTermsKey, IsAgreeWithTermsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsAgreeWithTermsKey, value);
            }
        }

        private const string AuthorizationTokenKey = "AuthorizationToken_key";
        private static readonly string AuthorizationTokenDefault = string.Empty;
        public static string AuthorizationToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthorizationTokenKey, AuthorizationTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthorizationTokenKey, value);
            }
        }

        //This should be used with caution as it will remove all of your app’s specific settings that were ever created.
        public static void ClearEverything()
        {
            AppSettings.Clear();
        }
    }
}
