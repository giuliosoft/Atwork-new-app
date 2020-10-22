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
    public class BackgroundLoginPageViewModel : ViewModelBase
    {
        #region Constructor
        public BackgroundLoginPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        #region public methods
        async Task LoginCall()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }

                if (SettingsService.VolunteersUserData == null || string.IsNullOrEmpty(SettingsService.VolunteersUserData?.volEmail) || string.IsNullOrEmpty(SettingsService.VolunteersUserData?.VolUserPassword))
                {
                    await _navigationService.NavigateAsync(nameof(StartUpPage));
                    return;
                }

                LoginInputModel inputModel = new LoginInputModel();
                inputModel.email = SettingsService.VolunteersUserData?.volUserName;
                inputModel.password = SettingsService.VolunteersUserData?.VolUserPassword;
                var serviceResult = await UserServices.LoginToApp(inputModel);
                var serviceResultBody = JsonConvert.DeserializeObject<LoginResponce>(serviceResult?.Body);

                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok && serviceResultBody != null &&!serviceResultBody.Message.ToLower().Contains("no record found."))
                {
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
                    if (SettingsService.IsBioMetricAuthenticationEnabled)
                    {
                        await _navigationService.NavigateAsync(nameof(TouchIDLoginPage));
                    }
                    else
                    {
                        if (serviceResultBody.Data1?.volOnBoardStatus.ToLower() == "complete" && serviceResultBody.Data1?.volStatus.ToLower() == "active")
                            await _navigationService.NavigateAsync(nameof(DashboardPage), null);
                        else
                            await _navigationService.NavigateAsync(nameof(WelcomeSetupPage), null);
                    }
                }
                else
                {
                    await _navigationService.NavigateAsync(nameof(StartUpPage));
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await LoginCall();
        }
    }
}
