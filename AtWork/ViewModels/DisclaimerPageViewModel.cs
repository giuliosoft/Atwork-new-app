using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class DisclaimerPageViewModel : ViewModelBase
    {
        #region Constructor
        public DisclaimerPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DisclaimerLabel = "Disclaimer";
            Disclaimerbtn = true;
            Termsconditionbtn = false;
        }
        #endregion

        #region Private Properties
        public bool isEnableDisclamerButton = false;
        #endregion
        #region Private Properties
        private string _DisclaimerLabel = string.Empty;
        private bool _Disclaimerbtn = true;
        private bool _Termsconditionbtn = false;
        private ImageSource _UserCompanyLogo = string.Empty;
        #endregion

        #region Public Properties
        public string DisclaimerLabel
        {
            get { return _DisclaimerLabel; }
            set { SetProperty(ref _DisclaimerLabel, value); }
        }
        public bool Disclaimerbtn
        {
            get { return _Disclaimerbtn; }
            set { SetProperty(ref _Disclaimerbtn, value); }
        }
        public bool Termsconditionbtn
        {
            get { return _Termsconditionbtn; }
            set { SetProperty(ref _Termsconditionbtn, value); }
        }

        public ImageSource UserCompanyLogo
        {
            get
            {
                return ImageSource.FromUri(new Uri(ConfigService.BaseCompanyLogoURL + SessionService.tempClaimProfileData.coLogo));
            }
            set { SetProperty(ref _UserCompanyLogo, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand BackButtonCommand { get { return new DelegateCommand(async () => await BackButton()); } }
        public DelegateCommand ContinueSetupCommand { get { return new DelegateCommand(async () => await TermsandCondition()); } }
        public DelegateCommand GoToAuthenticationIdCommand { get { return new DelegateCommand(async () => await GoToAuthenticationId()); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task BackButton()
        {
            try
            {
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task TermsandCondition()
        {
            try
            {
                if (isEnableDisclamerButton)
                {
                    if (SessionService.isFromClaimProfile)
                    {
                        SessionService.isFromClaimProfile = false;
                        await _navigationService.NavigateAsync(nameof(AuthentificationIDPage), null);
                    }
                    else
                    {
                        DisclaimerLabel = "Terms and condition";
                        Disclaimerbtn = false;
                        Termsconditionbtn = true;
                        await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GoToAuthenticationId()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(AuthentificationIDPage), null);
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
            DisclaimerLabel = "Disclaimer";
        }
    }
}

