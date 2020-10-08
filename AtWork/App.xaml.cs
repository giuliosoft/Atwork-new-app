using System;
using System.Net.Http;
using AtWork.Services;
using FFImageLoading;
using FFImageLoading.Config;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Lato_Black.ttf", Alias = "Lato_Black")]
[assembly: ExportFont("Lato_BlackItalic.ttf", Alias = "Lato_BlackItalic")]
[assembly: ExportFont("Lato_Bold.ttf", Alias = "Lato_Bold")]
[assembly: ExportFont("Lato_BoldItalic.ttf", Alias = "Lato_BoldItalic")]
[assembly: ExportFont("Lato_Italic.ttf", Alias = "Lato_Italic")]
[assembly: ExportFont("Lato_Light.ttf", Alias = "Lato_Light")]
[assembly: ExportFont("Lato_LightItalic.ttf", Alias = "Lato_LightItalic")]
[assembly: ExportFont("Lato_Regular.ttf", Alias = "Lato_Regular")]
[assembly: ExportFont("Lato_Thin.ttf", Alias = "Lato_Thin")]
[assembly: ExportFont("Lato_ThinItalic.ttf", Alias = "Lato_ThinItalic")]
namespace AtWork
{
    public partial class App
    {
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk3NDM5QDMxMzgyZTMxMmUzMFpzMmlxV0pZOHV3eG15VVBIZ3BwQlI1L1hVTjM1ZUhTMCtsSHlIR3VtY2s9");
            InitializeComponent();

            LayoutService.Init();
            LanguageService.Init(SettingsService.AppLanguage);
            NavigationService.NavigateAsync(InitNavigationPageService.Navigate()).Wait();
            //ImageService.Instance.Initialize(new Configuration
            //{
            //    HttpClient = BuildImageHttpClient()
            //});
            var config = new Configuration();
            config.DownloadCache = new CustomDownloadCacheService(config);
            ImageService.Instance.Initialize(config);
        }

        private static HttpClient BuildImageHttpClient()
        {
            var result = new HttpClient();
            result.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
            return result;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Registering all the services
            IoCService.RegisterTypes(containerRegistry);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
