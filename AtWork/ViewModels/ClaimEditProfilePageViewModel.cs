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
    public class ClaimEditProfilePageViewModel : ViewModelBase
    {
        #region Constructor
        public ClaimEditProfilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            BackCancelText = AppResources.BackButtonText;
        }
        #endregion

        #region Private Properties
        private string _UserEmail = string.Empty;
        private string _UserPassword = string.Empty;
        private string _UserName = string.Empty;
        private string _UserSurname = string.Empty;
        #endregion

        #region Public Properties
        public string UserEmail
        {
            get { return _UserEmail; }
            set { SetProperty(ref _UserEmail, value); }
        }

        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        public string UserSurname
        {
            get { return _UserSurname; }
            set { SetProperty(ref _UserSurname, value); }
        }
        #endregion

        #region Commands        
        public DelegateCommand SubmitProfileCorrectionsCommand { get { return new DelegateCommand(async () => await SubmitProfileCorrections()); } }
        //public DelegateCommand<string> HeaderBackCommand { get { return new DelegateCommand<string>(async (obj) => await PageHeaderBack(obj)); } }
        public DelegateCommand BackButtonCommand { get { return new DelegateCommand(async () => await PageHeaderBack()); } }
        #endregion

        #region private methods
        async Task PageHeaderBack()
        {
            try
            {
                await BackClick();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        private async Task SubmitProfileCorrections()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName.Trim()) || string.IsNullOrEmpty(UserSurname.Trim()) || string.IsNullOrEmpty(UserEmail.Trim()))
                {
                    await DisplayAlertAsync(AppResources.ProfileCorrectionInputAlertText);
                    return;
                }
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                ProfileCorrectionInputModel inputModel = new ProfileCorrectionInputModel();
                inputModel.newEmail = UserEmail;
                inputModel.newName = UserName;
                inputModel.newSurName = UserSurname;
                inputModel.volUniqueID = SessionService.tempVolunteerData.volUniqueID;

                var serviceResult = await UserServices.SubmitUserProfileCorrections(inputModel);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            await _navigationService.NavigateAsync(nameof(ClaimThankYouReqeuestPage), null);
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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                if (SessionService.tempVolunteerData != null && SessionService.tempClaimProfileData != null)
                {
                    UserName = SessionService.tempVolunteerData.volFirstName;
                    UserSurname = SessionService.tempVolunteerData.volLastName;
                    UserEmail = SessionService.tempVolunteerData.volEmail;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}

