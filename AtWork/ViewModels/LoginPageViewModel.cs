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
            //SessionService.AppNavigationService = null;
            AddNewsCancelImage = AppResources.BackButtonText;
#if DEBUG
            UserEmail = "hans.meier@volunty.com";
            UserPassword = "Password01#";
#endif
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
        public DelegateCommand ForgotPasswordCommand { get { return new DelegateCommand(async () => await ForgotPassword()); } }
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

                var serviceResult = await UserServices.LoginToApp(inputModel);
                var serviceResultBody = JsonConvert.DeserializeObject<LoginResponce>(serviceResult?.Body);

                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    SettingsService.LoggedInUserEmail = UserEmail;
                    SettingsService.LoggedInUserPassword = UserPassword;
                    if (serviceResultBody.Data != null)
                    {
                        SettingsService.LoggedInUserData = serviceResultBody.Data;
                        LayoutService.ConvertThemeAsPerSettings();
                    }
                    if (serviceResultBody.Data != null)
                    {
                        SettingsService.VolunteersUserData = serviceResultBody.Data1;
                        SettingsService.UserProfile = serviceResultBody.Data1?.volPicture;
                        await ChangeLanguage(serviceResultBody.Data1?.volLanguage);
                    }
                    if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok && !serviceResultBody.Message.ToLower().Contains("no record found."))
                    {
                        //await _navigationService.NavigateAsync(nameof(NewsPage));
                        await ClosePopup();
                        if (serviceResultBody.Data1?.volOnBoardStatus.ToLower() == "complete" && serviceResultBody.Data1?.volStatus.ToLower() == "active")
                            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                        else
                            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomeSetupPage)}", null);
                        return;
                    }
                    else
                    {
                        await DisplayAlertAsync(AppResources.InvalidEmailOrPassword);
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task ForgotPassword()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ForgotPasswordPage));
            }
            catch (Exception ex)
            {
                await ClosePopup();
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
