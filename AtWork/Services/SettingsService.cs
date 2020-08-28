﻿using System;
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
        private const string CompanyLogoKey = "LoggedInUserPassword_Key";
        private static readonly string CompanyLogoDefault = string.Empty;
        public static string CompanyLogo
        {
            get
            {
                return string.Format("http://app.atwork.ai/images/{0}", AppSettings.GetValueOrDefault(CompanyLogoKey, CompanyLogoDefault));
            }
            set
            {
                AppSettings.AddOrUpdateValue(CompanyLogoKey, value);
            }
        }
        private const string UserProfileImageKey = "UserProfileImage_Key";
        private static readonly string UserProfileImageDefault = string.Empty;
        public static string UserProfileImage
        {
            get
            {
                return string.Format("http://app.atwork.ai/images/{0}", AppSettings.GetValueOrDefault(UserProfileImageKey, UserProfileImageDefault));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserProfileImageKey, value);
            }
        }
    }
}
