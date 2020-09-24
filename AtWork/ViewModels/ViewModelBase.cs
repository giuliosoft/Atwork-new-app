using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using FFImageLoading.Forms;
using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService _navigationService { get; private set; }
        protected FacadeService _facadeService { get; private set; }

        #region Constructor
        public static bool IsFromMyActivity { get; set; } = false;
        public ViewModelBase(INavigationService navigationService, FacadeService facadeService)
        {
            _navigationService = navigationService;
            _facadeService = facadeService;
            //SessionService.AppNavigationService = _navigationService;
            LogoutCommand = null;
            CrossConnectivity.Current.ConnectivityChanged += async (sender, e) =>
            {
                if (!e.IsConnected)
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.AlertTitle, AppResources.NoInternetMsg, AppResources.AlertOkText);
                }
            };
        }
        #endregion

        #region Private Properties
        private bool _isVisiblePlayerBanner = false;
        private string _NextClickPageName = TextResources.AddPostTitlePageText;
        private string _AddNewsCancelImage;
        private string _BackCancelText;
        private string _NewsDetailBack;
        private string _AddNewsNextImage;
        string SelectedPage = string.Empty;
        private string _NextOptionText;
        private bool _DetailHeaderOptionIsVisible;
        private bool _NextCustomLabelIsVisible;
        //private Color _NewsGreenbg;
        //private Color _ActivitiesGreenbg;
        private DelegateCommand<string> _FooterNavigationCommand;
        private DelegateCommand<string> _HeaderNextNavigationCommand;
        private DelegateCommand _profileTapCommand;
        private string _HeaderDetailsTitle;
        private Color _NextTextColor = (Color)App.Current.Resources["WhiteColor"];
        private DelegateCommand _NewsOptionCommand;
        private bool _JoinActivity;
        private bool _UnSubscribeActivity;
        private double _headerDetailsTitleFontSize = (double)App.Current.Resources["FontSize20"];
        private Color _headerDetailBackgroundColor = (Color)App.Current.Resources["OffWhiteColor"];
        private DelegateCommand<string> _ClaimProfileBackCommand;
        #endregion
        public bool JoinActivity
        {
            get { return _JoinActivity; }
            set { SetProperty(ref _JoinActivity, value); }
        }
        public bool UnSubscribeActivity
        {
            get { return _UnSubscribeActivity; }
            set { SetProperty(ref _UnSubscribeActivity, value); }
        }
        public bool IsVisiblePlayerBanner
        {
            get { return _isVisiblePlayerBanner; }
            set { SetProperty(ref _isVisiblePlayerBanner, value); }
        }

        //private static ImageSource _UserProfileImage = "";
        public ImageSource UserProfileImage
        {
            get
            {
                if (SettingsService.VolunteersUserData == null || SettingsService.VolunteersUserData.volPicture == string.Empty)
                    return string.Empty;
                return ImageSource.FromUri(new Uri(ConfigService.BaseProfileImageURL + SettingsService.VolunteersUserData.volPicture));
            }
            //set { SetProperty(ref _UserProfileImage, value); }
        }
        public string AddNewsCancelImage
        {
            get { return _AddNewsCancelImage; }
            set { SetProperty(ref _AddNewsCancelImage, value); }
        }
        public string BackCancelText
        {
            get { return _BackCancelText; }
            set { SetProperty(ref _BackCancelText, value); }
        }
        public string AddNewsNextImage
        {
            get { return _AddNewsNextImage; }
            set { SetProperty(ref _AddNewsNextImage, value); }
        }
        public string NextClickPageName
        {
            get { return _NextClickPageName; }
            set { SetProperty(ref _NextClickPageName, value); }
        }
        public string NewsDetailBack
        {
            get { return _NewsDetailBack; }
            set { SetProperty(ref _NewsDetailBack, value); }
        }
        public string NextOptionText
        {
            get { return _NextOptionText; }
            set { SetProperty(ref _NextOptionText, value); }
        }
        public bool DetailHeaderOptionIsVisible
        {
            get { return _DetailHeaderOptionIsVisible; }
            set { SetProperty(ref _DetailHeaderOptionIsVisible, value); }
        }
        public bool NextCustomLabelIsVisible
        {
            get { return _NextCustomLabelIsVisible; }
            set { SetProperty(ref _NextCustomLabelIsVisible, value); }
        }
        //public Color NewsGreenbg
        //{
        //    get { return _NewsGreenbg; }
        //    set { SetProperty(ref _NewsGreenbg, value); }
        //}
        //public Color ActivitiesGreenbg
        //{
        //    get { return _ActivitiesGreenbg; }
        //    set { SetProperty(ref _ActivitiesGreenbg, value); }
        //}
        public DelegateCommand<string> FooterNavigationCommand
        {
            get { return _FooterNavigationCommand; }
            set { SetProperty(ref _FooterNavigationCommand, value); }
        }
        public DelegateCommand<string> HeaderNextNavigationCommand
        {
            get { return _HeaderNextNavigationCommand; }
            set { SetProperty(ref _HeaderNextNavigationCommand, value); }
        }
        public string HeaderDetailsTitle
        {
            get { return _HeaderDetailsTitle; }
            set { SetProperty(ref _HeaderDetailsTitle, value); }
        }

        public Color NextTextColor
        {
            get { return _NextTextColor; }
            set { SetProperty(ref _NextTextColor, value); }
        }

        public ImageSource CompanyLogo
        {
            get
            {
                if (SettingsService.LoggedInUserData.coLogo == string.Empty)
                    return string.Empty;
                return ImageSource.FromUri(new Uri(ConfigService.BaseCompanyLogoURL + SettingsService.LoggedInUserData.coLogo));
            }
            //set { SetProperty(ref _companyLogo, value); }
        }

        public DelegateCommand NewsOptionCommand
        {
            get { return _NewsOptionCommand; }
            set { SetProperty(ref _NewsOptionCommand, value); }
        }
        public DelegateCommand ProfileTapCommand
        {
            get { return _profileTapCommand; }
            set { SetProperty(ref _profileTapCommand, value); }
        }
        public double HeaderDetailsTitleFontSize
        {
            get { return _headerDetailsTitleFontSize; }
            set { SetProperty(ref _headerDetailsTitleFontSize, value); }
        }
        public Color HeaderDetailBackgroundColor
        {
            get { return _headerDetailBackgroundColor; }
            set { SetProperty(ref _headerDetailBackgroundColor, value); }
        }
        public DelegateCommand<string> ClaimProfileBackCommand
        {
            get { return _ClaimProfileBackCommand; }
            set { SetProperty(ref _ClaimProfileBackCommand, value); }
        }
        #region Commands

        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand ComingSoonCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand BackClickCommand { get { return new DelegateCommand(async () => await BackClick()); } }
        public DelegateCommand ClosePopupCommand { get { return new DelegateCommand(async () => await ClosePopup(true)); } }
        public DelegateCommand LeftCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand RightCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand AddNewsPostCommand { get { return new DelegateCommand(async () => await AddNewNewsPost()); } }
        public DelegateCommand<string> AddNewsPostNextCommand { get { return new DelegateCommand<string>(async (obj) => await AddNewsPostNext(obj)); } }
        //public DelegateCommand NewsOptionCommand { get { return new DelegateCommand(async () => await NewsOption()); } }
        public DelegateCommand GoToNewsPageCommand { get { return new DelegateCommand(async () => await GoToNewsPage()); } }
        public DelegateCommand GoToActivityPageCommand { get { return new DelegateCommand(async () => await GoToActivityPage()); } }
        public DelegateCommand HeaderBack { get { return new DelegateCommand(async () => await BackClick()); } }
        public DelegateCommand GreenHeaderViewBackCommand { get { return new DelegateCommand(async () => await GreenHeaderViewBack()); } }
        public DelegateCommand OpenProfileCommand { get { return new DelegateCommand(async () => await OpenProfile()); } }
        #endregion

        #region Methods

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        /// <summary>
        /// DisplayComingSoon
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task DisplayComingSoon()
        {
            await App.Current.MainPage.DisplayAlert(AppResources.AlertTitle, AppResources.ComingSoonAlertMsgText, AppResources.AlertOkText);
        }

        /// <summary>
        /// Navigate back to the previous screen. 
        /// </summary>
        /// <returns></returns>
        public async Task BackClick()
        {
            try
            {
                await _navigationService.GoBackAsync();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Navigate back to the previous screen. 
        /// </summary>
        /// <returns></returns>
        public async Task AddNewNewsPost()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(WelcomeSetupPage), null);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        private async Task GoToNewsPage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(NewsPage), null);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        private async Task GoToActivityPage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ActivityPage), null);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        public async Task GreenHeaderViewBack()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ClaimProfilePage), null);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        /// <summary>
        /// Destroy
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void Destroy() { }

        /// <summary>
        /// CheckConnectivity
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<bool> CheckConnectivity()
        {
            try
            {
                var isConnected = CrossConnectivity.Current.IsConnected;
                if (!isConnected)
                {
                    await DisplayAlertAsync(AppResources.NoInternetMsg);
                }
                return isConnected;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return false;
            }
        }

        /// <summary>
        /// DisplayAlertAsync
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task DisplayAlertAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertTitle, message, AppResources.AlertOkText);
            }
        }

        /// <summary>
        /// DisplayAlertAsync
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task DisplayAlertAsync(string titile, string message, string okText, string cancelText)
        {
            await App.Current.MainPage.DisplayAlert(titile, message, okText, cancelText);
        }

        /// <summary>
        /// ShowLoader
        /// </summary>
        /// <param name="animate">Pass true/false for animate the popup view</param>
        /// <returns></returns>
        public static async Task ShowPopup(PopupPage popup, bool animate = false)
        {
            try
            {
                await PopupNavigationService.Instance.PushAsync(popup, animate);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// ClosePopup
        /// </summary>
        /// <param name="animate">Pass true/false for animate the popup view</param>
        /// <returns></returns>
        public static async Task ClosePopup(bool animate = false)
        {
            try
            {
                if (PopupNavigationService.Instance.PopupStack != null && PopupNavigationService.Instance.PopupStack.Count > 0)
                {
                    var lastPage = PopupNavigationService.Instance.PopupStack.LastOrDefault();
                    if (lastPage.GetType() == typeof(LoadingPopup))
                    {
                        SessionService.isLoadingPopupOpen = false;
                    }
                    await PopupNavigationService.Instance.PopAsync(animate);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// ClosePopup
        /// </summary>
        /// <param name="selectedPageToNext">Page String</param>
        /// <returns></returns>
        public async Task AddNewsPostNext(string selectedPageToNext)
        {

            this.SelectedPage = selectedPageToNext;
            if (selectedPageToNext == nameof(AddNewsPostPage))
            {
                await _navigationService.NavigateAsync(nameof(AddNewsPostImagePage));
            }
            else if (selectedPageToNext == nameof(AddNewsPostImagePage))
            {
                //await _navigationService.NavigateAsync(nameof(AddNewsAttachFilePage));
            }
            else if (selectedPageToNext == nameof(AddNewsAttachFilePage))
            {
                await _navigationService.NavigateAsync(nameof(PostNewsPage));
            }
            else if (selectedPageToNext == nameof(PostNewsPage))
            {
                await _navigationService.NavigateAsync(nameof(DashboardPage));
            }
            else if (selectedPageToNext == nameof(CropImagePage))
            {
                SessionService.isImageCropped = true;
                await BackClick();
            }
        }

        public async Task ShowLoader(bool animate = false)
        {
            try
            {
                if (SessionService.isLoadingPopupOpen)
                {
                    return;
                }
                SessionService.isLoadingPopupOpen = true;
                await ShowPopup(new LoadingPopup(), animate);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task OpenProfile()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ProfilePage), null);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        public async Task OpenUserProfileAsync(string UserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserId))
                {
                    var nav = new NavigationParameters();
                    nav.Add("VolId", UserId);
                    await _navigationService.NavigateAsync(nameof(ProfilePage), nav);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //public async Task LogoutFromApp()
        //{
        //    SessionService.Logout();
        //    await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        //}
        #endregion
    }
}
