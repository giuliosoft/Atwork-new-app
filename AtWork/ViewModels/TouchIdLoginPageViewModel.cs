using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        //public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand BiometricAuthCommand { get { return new DelegateCommand(async () => await BiometricAuthClick()); } }
        #endregion

        #region private methods
        //async Task GoForLogin()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        public async Task BiometricAuthClick()
        {
            try
            {
                var IsConnected = await CheckConnectivity();
                if (!IsConnected) return;

                bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
                if (!isFingerprintAvailable)
                {
                    await _facadeService.dialogService.DisplayAlertAsync("Error", "Biometric authentication is not available or is not configured.", "OK");
                    return;
                }

                AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration("Authentication",
                                                                        "Tap this fingerprint sensor!");

                var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
                if (authResult.Authenticated)
                {
                    if (!SettingsService.IsUsedBioMetricLogin)
                    {
                        await _navigationService.NavigateAsync(nameof(LoginPage),null);
                    }
                    else
                    {
                        SettingsService.IsUsedBioMetricLogin = true;
                        await _navigationService.NavigateAsync(nameof(LoginPage),null);
                    }

                }
                else
                {
                    //SettingsService.IsUsedBioMetricLogin = false;
                    await _facadeService.dialogService.DisplayAlertAsync("Error", "Authentication failed", "OK");
                }
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
