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
    public class AuthenticationIDPageViewModel : ViewModelBase
    {
        #region Constructor
        public AuthenticationIDPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
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
        public DelegateCommand GoForenableCommand { get { return new DelegateCommand(async () => await GoForEnable()); } }

        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {
                SettingsService.IsBioMetricAuthenticationEnabled = false;
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}/{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        private async Task GoForEnable()
        {
            try
            {
                SettingsService.IsBioMetricAuthenticationEnabled = true;
                await _navigationService.NavigateAsync(nameof(TouchIDLoginPage), null);
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
        }
    }
}

