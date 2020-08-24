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
    public class ClaimProfilePageViewModel : ViewModelBase
    {
        #region Constructor
        public ClaimProfilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties

        private string _UserEmail = string.Empty;
        public string UserEmail
        {
            get { return _UserEmail; }
            set { SetProperty(ref _UserEmail, value); }
        }
        private string _UserPassword = string.Empty;
        public string UserPassword
        {
            get { return _UserPassword; }
            set { SetProperty(ref _UserPassword, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForCancelCommand { get { return new DelegateCommand(async () => await GoForCancel()); } }
        public DelegateCommand GoForFindAndClaimAccountCommand { get { return new DelegateCommand(async () => await GoForFindAndClaimAccount()); } }
        public DelegateCommand GoForPasswordCommand { get { return new DelegateCommand(async () => await GoForPassword()); } }
        #endregion

        #region private methods
        async Task GoForCancel()
        {
            try
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}", null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GoForFindAndClaimAccount()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ClaimEditProfilePage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GoForPassword()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreatePasswordPage),null);
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
