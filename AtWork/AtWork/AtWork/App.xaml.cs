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
