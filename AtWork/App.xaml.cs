using System;
using AtWork.Services;
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
            LanguageService.Init("language");
            NavigationService.NavigateAsync(InitNavigationPageService.Navigate()).Wait();
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
