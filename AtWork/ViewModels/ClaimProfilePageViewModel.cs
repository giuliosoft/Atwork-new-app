using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

namespace AtWork.ViewModels
{
    public class ClaimProfilePageViewModel : ViewModelBase
    {
        #region Constructor
        public ClaimProfilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private string _UserEmail = string.Empty;
        private string _UserPassword = string.Empty;
        private string _UserName = string.Empty;
        private string _UserSurname = string.Empty;
        private ImageSource _UserCompanyLogo = string.Empty;
        #endregion

        #region Public Properties
        public string UserEmail
        {
            get { return _UserEmail; }
            set { SetProperty(ref _UserEmail, value); }
        }

        public string UserPassword
        {
            get { return _UserPassword; }
            set { SetProperty(ref _UserPassword, value); }
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

        public ImageSource UserCompanyLogo
        {
            get { return _UserCompanyLogo; }
            set { SetProperty(ref _UserCompanyLogo, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForCancelCommand { get { return new DelegateCommand(async () => await GoForCancel()); } }
        public DelegateCommand GoForFindAndClaimAccountCommand { get { return new DelegateCommand(async () => await GoForFindAndClaimAccount()); } }
        public DelegateCommand GoForPasswordCommand { get { return new DelegateCommand(async () => await GoForPassword()); } }
        #endregion

        #region private methods
        async Task GoForCancel()
        {
            try
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}", null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GoForFindAndClaimAccount()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ClaimEditProfilePage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GoForPassword()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreatePasswordPage), null);
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
            try
            {
                if (SessionService.tempVolunteerData != null && SessionService.tempClaimProfileData != null)
                {
                    UserName = SessionService.tempVolunteerData.volFirstName;
                    UserSurname = SessionService.tempVolunteerData.volLastName;
                    UserEmail = SessionService.tempVolunteerData.volEmail;
                    if (!string.IsNullOrEmpty(SessionService.tempClaimProfileData.coLogo))
                    {
                        UserCompanyLogo = ImageSource.FromUri(new Uri(ConfigService.BaseCompanyLogoURL + SessionService.tempClaimProfileData.coLogo));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
