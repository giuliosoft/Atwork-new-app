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
    public class ChangeUserPasswordPageViewModel : ViewModelBase
    {
        #region Constructor
        public ChangeUserPasswordPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(ChangeUserPasswordPage);
            AddNewsCancelImage = AppResources.Cancel;
            HeaderDetailsTitle = AppResources.ChangePasswordTitle;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.NextButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _CurrentPassword = string.Empty;
        private string _NewPassword = string.Empty;
        private string _ConfirmNewpassword = string.Empty;
        private string _EnterValidPassword = AppResources.EnterCorrectPassword;
        bool _isCurrentPassword = false;
        bool _isNewPassword = false;
        bool _isNewConfirmPassword = false;
        bool _ShowValidPasswordMessage = false;
        bool _ShowAlrtEnterCorrectPassword = false;
        bool _Samepasswordworning = false;
        int RetryCount = 0;
        #endregion

        #region Public Properties        
        public string CurrentPassword
        {
            get { return _CurrentPassword; }
            set { SetProperty(ref _CurrentPassword, value); }
        }
        public string NewPassword
        {
            get { return _NewPassword; }
            set { SetProperty(ref _NewPassword, value); }
        }
        public string ConfirmNewpassword
        {
            get { return _ConfirmNewpassword; }
            set { SetProperty(ref _ConfirmNewpassword, value); }
        }
        public string EnterValidPassword
        {
            get { return _EnterValidPassword; }
            set { SetProperty(ref _EnterValidPassword, value); }
        }
        public bool isCurrentPassword
        {
            get { return _isCurrentPassword; }
            set { SetProperty(ref _isCurrentPassword, value); }
        }
        public bool isNewPassword
        {
            get { return _isNewPassword; }
            set { SetProperty(ref _isNewPassword, value); }
        }
        public bool isNewConfirmPassword
        {
            get { return _isNewConfirmPassword; }
            set { SetProperty(ref _isNewConfirmPassword, value); }
        }
        public bool ShowValidPasswordMessage
        {
            get { return _ShowValidPasswordMessage; }
            set { SetProperty(ref _ShowValidPasswordMessage, value); }
        }
        public bool ShowAlrtEnterCorrectPassword
        {
            get { return _ShowAlrtEnterCorrectPassword; }
            set { SetProperty(ref _ShowAlrtEnterCorrectPassword, value); }
        }
        public bool Samepasswordworning
        {
            get { return _Samepasswordworning; }
            set { SetProperty(ref _Samepasswordworning, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveDetail(obj)); } }
        public DelegateCommand<string> OpenFotgotPasswordPageCommand { get { return new DelegateCommand<string>(async (obj) => await OpenFotgotPasswordPage(obj)); } }
        #endregion

        #region private methods

        async Task OpenFotgotPasswordPage(string str)
        {
            try
            {
                SessionService.Logout();
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}/{nameof(LoginPage)}/{nameof(ForgotPasswordPage)}");
                //await _navigationService.NavigateAsync(nameof(ForgotPasswordPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task SaveDetail(string str)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (isCurrentPassword)
                {
                    
                    await ShowLoader();
                    LoginInputModel inputLoginModel = new LoginInputModel();
                    inputLoginModel.email = SettingsService.VolunteersUserData?.volUserName;
                    inputLoginModel.password = CurrentPassword;
                    var serviceResultLogin = await UserServices.LoginToApp(inputLoginModel);
                    await ClosePopup();
                    var serviceResultBody = JsonConvert.DeserializeObject<LoginResponce>(serviceResultLogin?.Body);
                    if (serviceResultLogin != null && serviceResultLogin.Result == ResponseStatus.Ok
                        && serviceResultBody != null && !serviceResultBody.Message.ToLower().Contains("no record found."))
                    {
                        isCurrentPassword = false;
                        isNewPassword = true;
                        isNewConfirmPassword = false;
                    }
                    else
                    {
                       
                        RetryCount++;
                        if (RetryCount >= 3)
                        {
                            EnterValidPassword = AppResources.Requestpasswordresethere;
                            return;
                        }
                        CurrentPassword = string.Empty;
                        ShowAlrtEnterCorrectPassword = true;
                        EnterValidPassword = AppResources.EnterCorrectPassword;
                        //await DisplayAlertAsync(AppResources.InvalidEmailOrPassword);
                    }
                    
                }
                else if(isNewPassword)
                {
                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        ShowValidPasswordMessage = false;
                        await DisplayAlertAsync(AppResources.EnterConfirmNewPasswordAlert);
                        return;
                    }
                    else if (!CommonUtility.PasswordIsValid(NewPassword))
                    {
                        ShowValidPasswordMessage = true;
                        return;
                    }
                    else
                    {
                        isCurrentPassword = false;
                        isNewPassword = false;
                        isNewConfirmPassword = true;
                        AddNewsNextImage = AppResources.SaveButtonText;
                    }
                }
                else if(isNewConfirmPassword)
                {

                    if (NewPassword != ConfirmNewpassword)
                    {
                        Samepasswordworning = true;
                        //await DisplayAlertAsync(AppResources.EnterConfirmPasswordNotMatchdAlert);
                        return;
                    }
                    else
                    {
                        await ShowLoader();
                        Volunteers inputModel = new Volunteers();
                        inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                        inputModel.oldPassword = CurrentPassword;
                        inputModel.VolUserPassword = NewPassword;
                        var serviceResult = await UserServices.ChangeUserPassword(inputModel);
                        if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                        {
                            if (serviceResult.Body != null)
                            {
                                var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                                //if (serviceBody != null && !serviceBody.Flag)
                                //{
                                //    await DisplayAlertAsync(AppResources.Currentpasswordnotmatch);
                                //}
                                //else
                                if (serviceBody != null && serviceBody.Flag)
                                {
                                    await DisplayAlertAsync(AppResources.Passwordchangesuccessfully);
                                    SettingsService.LoggedInUserPassword = NewPassword;
                                    SettingsService.VolunteersUserData.VolUserPassword = NewPassword;
                                    await _navigationService.GoBackAsync();
                                }
                            }
                        }
                        await ClosePopup();
                    }
                }


                //if (string.IsNullOrEmpty(CurrentPassword))
                //{
                //    await DisplayAlertAsync(AppResources.EnterCurrentPasswordAlert);
                //    return;
                //}
                //else
                //if (string.IsNullOrEmpty(NewPassword))
                //{
                //    await DisplayAlertAsync(AppResources.EnterNewPasswordAlert);
                //    return;
                //}
                //else if (string.IsNullOrEmpty(ConfirmNewpassword))
                //{
                //    await DisplayAlertAsync(AppResources.EnterConfirmNewPasswordAlert);
                //    return;
                //}
                //else if (NewPassword != ConfirmNewpassword)
                //{
                //    await DisplayAlertAsync(AppResources.EnterConfirmPasswordNotMatchdAlert);
                //    return;
                //}
               
                
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
            isCurrentPassword = true;
        }
    }
}


