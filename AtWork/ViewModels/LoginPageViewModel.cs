using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Constructor
        public LoginPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
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
        #endregion

        #region private methods
        async Task LoginToApp()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(UserPassword))
                {
                    await DisplayAlertAsync(AppResources.LoginEmptyFieldMsg);
                    return;
                }
                else if (!CommonUtility.emailIsValid(UserEmail))
                {
                    await DisplayAlertAsync(AppResources.InvalidEmailMsg);
                    return;
                }
                else if (UserPassword.Length < 6)
                {
                    await DisplayAlertAsync(AppResources.PasswordLengthMsg);
                    return;
                }
#if DEBUG    
                //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
#endif
                await ExistingUserLoginToApp();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task ExistingUserLoginToApp()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                await ClosePopup();
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
