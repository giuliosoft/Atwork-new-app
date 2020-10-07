using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        #endregion

        #region Public Properties        
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
        }
    }
}
