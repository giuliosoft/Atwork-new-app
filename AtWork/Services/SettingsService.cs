using System;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using static AtWork.Models.LoginModel;

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

        private const string IsUsedBioMetricLoginKey = "IsUsedBioMetricLogin_key";
        private static readonly bool IsUsedBioMetricLoginDefault = false;
        public static bool IsUsedBioMetricLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsUsedBioMetricLoginKey, IsUsedBioMetricLoginDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsUsedBioMetricLoginKey, value);
            }
        }
        //This should be used with caution as it will remove all of your app’s specific settings that were ever created.
        public static void ClearEverything()
        {
            AppSettings.Clear();
        }

        private const string LoggedInUserDataKey = "LoggedInUserData_key";
        public static LoginOutputModel LoggedInUserData
        {
            get
            {
                var value = AppSettings.GetValueOrDefault(LoggedInUserDataKey, string.Empty);
                if (string.IsNullOrEmpty(value)) { return null; }
                else { return JsonConvert.DeserializeObject<LoginOutputModel>(value); }
            }
            set
            {
                string data = string.Empty;
                if (value != null) { data = JsonConvert.SerializeObject(value); }
                AppSettings.AddOrUpdateValue(LoggedInUserDataKey, data);
            }
        }

        private const string LoggedInUserEmailKey = "LoggedInUserEmail_Key";
        private static readonly string LoggedInUserEmailDefault = string.Empty;
        public static string LoggedInUserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoggedInUserEmailKey, LoggedInUserEmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoggedInUserEmailKey, value);
            }
        }
        private const string LoggedInUserPasswordKey = "LoggedInUserPassword_Key";
        private static readonly string LoggedInUserPasswordDefault = string.Empty;
        public static string LoggedInUserPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoggedInUserPasswordKey, LoggedInUserPasswordDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoggedInUserPasswordKey, value);
            }
        }


        private const string VolunteersUserDataKey = "VolunteersUserData_key";
        public static Volunteers VolunteersUserData
        {
            get
            {
                var value = AppSettings.GetValueOrDefault(VolunteersUserDataKey, string.Empty);
                if (string.IsNullOrEmpty(value)) { return null; }
                else { return JsonConvert.DeserializeObject<Volunteers>(value); }
            }
            set
            {
                string data = string.Empty;
                if (value != null) { data = JsonConvert.SerializeObject(value); }
                AppSettings.AddOrUpdateValue(VolunteersUserDataKey, data);
            }
        }

    }
}
