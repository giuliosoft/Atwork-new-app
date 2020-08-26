using System;
using AtWork.Services;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
