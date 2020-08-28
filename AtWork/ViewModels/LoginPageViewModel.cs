using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

namespace AtWork.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Constructor
        public LoginPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = "Back";
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private bool _wrongPasswordLabelIsVisible = false;
        #endregion

        #region Public Properties        
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
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
        public bool WrongPasswordLabelIsVisible
        {
            get { return _wrongPasswordLabelIsVisible; }
            set { SetProperty(ref _wrongPasswordLabelIsVisible, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await LoginToApp()); } }
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
                    await DisplayAlertAsync(TextResources.LoginEmptyFieldMsg);
                    return;
                }

                await ShowLoader();
                LoginInputModel inputModel = new LoginInputModel();
                inputModel.email = UserEmail;
                inputModel.password = UserPassword;
                SettingsService.LoggedInUserEmail = UserEmail;
                SettingsService.LoggedInUserPassword = UserPassword;
                var serviceResult = await UserServices.LoginToApp(inputModel);
                var serviceResultBody = JsonConvert.DeserializeObject<LoginResponce>(serviceResult.Body);
                
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    SettingsService.LoggedInUserEmail = UserEmail;
                    SettingsService.LoggedInUserPassword = UserPassword;
                    SettingsService.CompanyLogo = serviceResultBody.Data.coLogo;
                    SettingsService.UserProfileImage = serviceResultBody.Data.coLogo;
                    LayoutService.ConvertThemeAsPerSettings(serviceResultBody.Data);
                    if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                    {
                        //await _navigationService.NavigateAsync(nameof(NewsPage));
                        await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                    }
                    else
                    {
                        await DisplayAlertAsync(TextResources.InvalidUserNameorPaddword);
                    }
                }
                await ClosePopup();

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
