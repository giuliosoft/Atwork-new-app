using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class WelcomeSetupPageViewModel : ViewModelBase
    {
        #region Constructor
        public WelcomeSetupPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            
        }
        #endregion

        #region Private Properties
        private string _BeginSetupText = AppResources.BeginSetup;
        private string _SkipText = AppResources.SkipText;
        private string _WelcomeSetup = AppResources.WelcomeSetup;
        private string _WelcomeSetup2 = AppResources.WelcomeSetup2;
        private string _WelcomeSetup1 = AppResources.WelcomeSetup1;
        #endregion

        #region Public Properties
        public string BeginSetupText
        {
            get { return _BeginSetupText; }
            set { SetProperty(ref _BeginSetupText, value); }
        }
        public string SkipText
        {
            get { return _SkipText; }
            set { SetProperty(ref _SkipText, value); }
        }
        public string WelcomeSetup
        {
            get { return _WelcomeSetup; }
            set { SetProperty(ref _WelcomeSetup, value); }
        }
        public string WelcomeSetup2
        {
            get { return _WelcomeSetup2; }
            set { SetProperty(ref _WelcomeSetup2, value); }
        }
        public string WelcomeSetup1
        {
            get { return _WelcomeSetup1; }
            set { SetProperty(ref _WelcomeSetup1, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForSkipCommand { get { return new DelegateCommand(async () => await GoForSkip()); } }
        public DelegateCommand GoForBeginSetupCommand { get { return new DelegateCommand(async () => await GoForBeginSetup()); } }
        #endregion

        #region private methods
        async Task GoForSkip()
        {
            try
            {
                SessionService.IsWelcomeSetup = false;
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task GoForBeginSetup()
        {
            try
            {
                SessionService.IsWelcomeSetup = true;
                SessionService.CurrentTab = 0;
                await _navigationService.NavigateAsync(nameof(LanguageListPage), null);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        #region public methods

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            BeginSetupText = AppResources.BeginSetup;
            SkipText = AppResources.SkipText;
            WelcomeSetup = AppResources.WelcomeSetup;
            WelcomeSetup2 = AppResources.WelcomeSetup2;
            WelcomeSetup1 = AppResources.WelcomeSetup1;
        }
    }
}
