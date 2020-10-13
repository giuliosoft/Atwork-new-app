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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class CreatePasswordPageViewModel : ViewModelBase
    {
        public string ConfirmPassword;
        bool isCreatingPwd = true;
        #region Constructor
        public CreatePasswordPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            CreatePassowrdLabeltext = AppResources.CreatePasswordText;
            BackCancelText = AppResources.BackButtonText;
            Passwordmessage = true;
            Samepasswordworning = false;
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private string _CreatePassowrdLabeltext = string.Empty;
        private string _CreatePassowrdEntrytext = string.Empty;
        private bool _Passwordmessage = true;
        private bool _Samepasswordworning = false;
        //private ImageSource _UserCompanyLogo = string.Empty;
        #endregion

        #region Public Properties

        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }

        public string CreatePassowrdLabeltext
        {
            get { return _CreatePassowrdLabeltext; }
            set { SetProperty(ref _CreatePassowrdLabeltext, value); }
        }
        public string CreatePassowrdEntrytext
        {
            get { return _CreatePassowrdEntrytext; }
            set { SetProperty(ref _CreatePassowrdEntrytext, value); }
        }
        public bool Passwordmessage
        {
            get { return _Passwordmessage; }
            set { SetProperty(ref _Passwordmessage, value); }
        }
        public bool Samepasswordworning
        {
            get { return _Samepasswordworning; }
            set { SetProperty(ref _Samepasswordworning, value); }
        }
        public ImageSource UserCompanyLogo
        {
            get
            {
                if (string.IsNullOrEmpty(SessionService.tempClaimProfileData?.coLogo))
                    return string.Empty;
                return ImageSource.FromUri(new Uri(ConfigService.BaseCompanyLogoURL + SessionService.tempClaimProfileData.coLogo));
            }
            //set { SetProperty(ref _UserCompanyLogo, value); }
        }
        #endregion

        #region Commands        
        public DelegateCommand ConfirmPasswordCommand { get { return new DelegateCommand(async () => await ConfirmPasswordClick()); } }
        //public DelegateCommand<string> HeaderBackCommand { get { return new DelegateCommand<string>(async (obj) => await PageHeaderBack(obj)); } }
        public DelegateCommand BackButtonCommand { get { return new DelegateCommand(async () => await PageHeaderBack()); } }
        #endregion

        #region private methods
        async Task PageHeaderBack()
        {
            try
            {
                if (isCreatingPwd)
                {
                    await BackClick();
                }
                else
                {
                    ConfirmPassword = string.Empty;
                    CreatePassowrdEntrytext = string.Empty;
                    CreatePassowrdLabeltext = AppResources.CreatePasswordText;
                    Passwordmessage = true;
                    Samepasswordworning = false;
                    isCreatingPwd = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task ConfirmPasswordClick()
        {
            try
            {
                if (isCreatingPwd)
                {
                    if (!CommonUtility.PasswordIsValid(CreatePassowrdEntrytext))
                    {
                        Passwordmessage = true;
                        return;
                    }
                    isCreatingPwd = false;
                    if (!String.IsNullOrEmpty(CreatePassowrdEntrytext))
                    {
                        CreatePassowrdLabeltext = AppResources.ConfirmPasswordText;
                        ConfirmPassword = CreatePassowrdEntrytext;
                        CreatePassowrdEntrytext = null;
                        Passwordmessage = false;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(CreatePassowrdEntrytext))
                    {
                        //CreatePassowrdLabeltext = AppResources.ConfirmPasswordText;
                        //ConfirmPassword = CreatePassowrdEntrytext;
                        //CreatePassowrdEntrytext = null;
                        //Passwordmessage = false;
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (Samepasswordworning)
                            {
                                return;
                            }
                            if (!await CheckConnectivity())
                            {
                                return;
                            }
                            await ShowLoader();
                            if (SessionService.tempVolunteerData != null)
                            {
                                SessionService.tempVolunteerData.VolUserPassword = CreatePassowrdEntrytext;
                                SessionService.tempVolunteerData.oldPassword = string.Empty;
                            }
                            var serviceResult = await UserServices.ChangeUserPassword(SessionService.tempVolunteerData, false);
                            if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                            {
                                if (serviceResult.Body != null)
                                {
                                    var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                                    if (serviceBody != null && serviceBody.Flag)
                                    {
                                        SessionService.isFromClaimProfile = true;
                                        NavigationParameters parameter = new NavigationParameters();
                                        parameter.Add("isDisclaimer", true);
                                        await _navigationService.NavigateAsync(nameof(DisclaimerPage), parameter);
                                    }   
                                }
                            }
                            await ClosePopup();
                        }
                        catch (Exception ex)
                        {
                            await ClosePopup();
                            Debug.WriteLine(ex.Message);
                        }
                        //await _navigationService.NavigateAsync(nameof(AuthentificationIDPage), null);
                    }
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
            ConfirmPassword = string.Empty;
            CreatePassowrdEntrytext = string.Empty;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            CreatePassowrdLabeltext = AppResources.CreatePasswordText;
            isCreatingPwd = true;
        }
    }
}

