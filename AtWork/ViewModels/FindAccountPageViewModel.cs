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
    public class FindAccountPageViewModel : ViewModelBase
    {
        #region Constructor
        public FindAccountPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.BackButtonText;
        }
        #endregion

        #region Private Properties
        private string _CompanyEmail = string.Empty;
        private string _CompanyID = string.Empty;
        #endregion

        #region Public Properties
        public string CompanyID
        {
            get { return _CompanyID; }
            set { SetProperty(ref _CompanyID, value); }
        }

        public string CompanyEmail
        {
            get { return _CompanyEmail; }
            set { SetProperty(ref _CompanyEmail, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForSubmitCommand { get { return new DelegateCommand(async () => await GoForSubmit()); } }
        #endregion

        #region private methods
        async Task GoForSubmit()
        {
            try
            {
                if (string.IsNullOrEmpty(CompanyEmail) && string.IsNullOrEmpty(CompanyID))
                {
                    await DisplayAlertAsync(AppResources.ClaimInputAlertText);
                    return;
                }
                if (!await CheckConnectivity())
                {
                    return;
                }
                string input = string.Empty;
                if (!string.IsNullOrEmpty(CompanyEmail))
                {
                    input = CompanyEmail;
                }
                else if (!string.IsNullOrEmpty(CompanyID))
                {
                    input = CompanyID;
                }
                await ShowLoader();
                var serviceResult = await UserServices.ClaimUserProfile(input);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<LoginResponce>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            if (serviceBody.Data != null)
                            {
                                LoginOutputModel tempData = new LoginOutputModel();
                                tempData = serviceBody.Data;
                                SessionService.tempClaimProfileData = tempData;
                                LayoutService.ConvertThemeAsPerClaimProfileSettings();
                            }
                            if (serviceBody.Data1 != null)
                            {
                                Volunteers tempUserVol = new Volunteers();
                                tempUserVol = serviceBody.Data1;
                                SettingsService.LoggedInUserEmail = tempUserVol.volUserName;
                                SettingsService.LoggedInUserPassword = tempUserVol.VolUserPassword;
                                SessionService.tempVolunteerData = tempUserVol;
                                await ChangeLanguage(tempUserVol?.volLanguage);
                            }
                            await _navigationService.NavigateAsync(nameof(ClaimProfilePage), null);
                        }
                        else if(serviceBody != null && !serviceBody.Flag && serviceBody.Data == null && serviceBody.Data1 == null)
                        {
                            await DisplayAlertAsync(AppResources.InvalidClainEmailOrId);
                        }
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
        #endregion

        #region public methods

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
