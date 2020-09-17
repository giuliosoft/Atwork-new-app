using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
namespace AtWork.ViewModels
{
    public class TouchIdLoginPageViewModel : ViewModelBase
    {
        #region Constructor
        public TouchIdLoginPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            ClaimProfileBackCommand = HeaderBackCommand;
        }
        #endregion

        #region Private Properties
        bool isBusyInAuthentication = false;
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
        public DelegateCommand BiometricAuthCommand { get { return new DelegateCommand(async () => await BiometricAuthClick()); } }
        public DelegateCommand<string> HeaderBackCommand { get { return new DelegateCommand<string>(async (obj) => await PageHeaderBack(obj)); } }
        #endregion

        #region private methods
        async Task PageHeaderBack(string str)
        {
            try
            {
                await BackClick();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task BiometricAuthClick()
        {
            try
            {
                var IsConnected = await CheckConnectivity();
                if (!IsConnected) return;

                if (isBusyInAuthentication)
                {
                    return;
                }
                isBusyInAuthentication = true;

                bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
                if (!isFingerprintAvailable)
                {
                    await _facadeService.dialogService.DisplayAlertAsync(AppResources.ErrorText, AppResources.BiometricAlertMsgText, AppResources.AlertOkText);
                    return;
                }

                AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration(AppResources.AuthenticationText,
                                                                        AppResources.AuthenticationAlertMsgText);

                var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
                if (authResult.Authenticated)
                {
                    if (SettingsService.LoggedInUserData != null && SettingsService.VolunteersUserData != null)
                    {
                        LayoutService.ConvertThemeAsPerSettings();
                        if (SettingsService.VolunteersUserData?.volOnBoardStatus.ToLower() == "complete" && SettingsService.VolunteersUserData?.volStatus.ToLower() == "active")
                            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}");
                        else
                            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomeSetupPage)}");
                    }
                    else
                    {
                        await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}");
                    }
                }
                else
                {
                    await _facadeService.dialogService.DisplayAlertAsync(AppResources.ErrorText, AppResources.AuthenticationFailedAlertMsgText, AppResources.AlertOkText);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                isBusyInAuthentication = false;
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
            await BiometricAuthClick();
        }
    }
}
