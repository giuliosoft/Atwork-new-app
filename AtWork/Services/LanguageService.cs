using System;
using System.Globalization;
using System.Threading;
using AtWork.Multilingual;

namespace AtWork.Services
{
    public class LanguageService
    {
        public LanguageService()
        {
        }

        public static void Init(string Language)
        {
            /*
            CultureInfo cultureInfo = new CultureInfo(TextResources.EnglishCode);
            if (Language == TextResources.EnglishLanguage)
            {
                cultureInfo = new CultureInfo(TextResources.EnglishCode);
                SettingsService.AppLanguage = TextResources.EnglishLanguage;
            }
            else if (Language == TextResources.SpanishLanguage)
            {
                cultureInfo = new CultureInfo(TextResources.SpanishCode);
                SettingsService.AppLanguage = TextResources.SpanishLanguage;
            }

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            AppResources.Culture = cultureInfo;
            */
        }
    }
}
