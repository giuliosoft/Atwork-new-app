using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using AtWork.Helpers;
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
            try
            {
                CultureInfo cultureInfo = new CultureInfo(TextResources.EnglishCode);
                if (Language.Equals(TextResources.EnglishLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.EnglishCode);
                    SettingsService.AppLanguage = TextResources.EnglishLanguage;
                }
                else if (Language.Equals(TextResources.GermanLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.GermanCode);
                    SettingsService.AppLanguage = TextResources.GermanLanguage;
                }
                else if (Language.Equals(TextResources.ItalianLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.ItalianCode);
                    SettingsService.AppLanguage = TextResources.ItalianLanguage;
                }
                else if (Language.Equals(TextResources.FrenchLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.FrenchCode);
                    SettingsService.AppLanguage = TextResources.FrenchLanguage;
                }
                else if (Language.Equals(TextResources.SpanishLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.SpanishCode);
                    SettingsService.AppLanguage = TextResources.SpanishLanguage;
                }
                else if (Language.Equals(TextResources.RussianLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.RussianCode);
                    SettingsService.AppLanguage = TextResources.RussianLanguage;
                }
                else if (Language.Equals(TextResources.MandarinLanguage, StringComparison.InvariantCultureIgnoreCase))
                {
                    cultureInfo = new CultureInfo(TextResources.MandarinCode);
                    SettingsService.AppLanguage = TextResources.MandarinLanguage;
                }

                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                AppResources.Culture = cultureInfo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
