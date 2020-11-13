using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class StartUpPageViewModel : ViewModelBase
    {
        #region Constructor
        public StartUpPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        private string _ProductDetail = string.Empty;
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand GoForFindAndClaimAccountCommand { get { return new DelegateCommand(async () => await FindAndClaimAccount()); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {
                //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", null);
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task FindAndClaimAccount()
        {
            try
            {
                //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", null);
                await _navigationService.NavigateAsync(nameof(FindAccountPage));
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
