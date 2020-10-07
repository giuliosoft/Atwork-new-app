using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _CurrentPassword = string.Empty;
        private string _NewPassword = string.Empty;
        private string _ConfirmNewpassword = string.Empty;
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
        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveDetail(obj)); } }
        #endregion

        #region private methods
        async Task SaveDetail(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(CurrentPassword))
                {
                    await DisplayAlertAsync(AppResources.EnterCurrentPasswordAlert);
                    return;
                }
                else if (string.IsNullOrEmpty(NewPassword))
                {
                    await DisplayAlertAsync(AppResources.EnterNewPasswordAlert);
                    return;
                }
                else if (string.IsNullOrEmpty(ConfirmNewpassword))
                {
                    await DisplayAlertAsync(AppResources.EnterConfirmNewPasswordAlert);
                    return;
                }
                else if (NewPassword != ConfirmNewpassword)
                {
                    await DisplayAlertAsync(AppResources.EnterConfirmPasswordNotMatchdAlert);
                    return;
                }
               
                if (!await CheckConnectivity())
                {
                    return;
                }
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
                        if (serviceBody != null && !serviceBody.Flag)
                        {
                            await DisplayAlertAsync(AppResources.Currentpasswordnotmatch);
                        }
                        else if (serviceBody != null && serviceBody.Flag)
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


