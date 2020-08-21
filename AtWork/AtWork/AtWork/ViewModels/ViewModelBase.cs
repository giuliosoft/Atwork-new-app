using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
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

        public ViewModelBase(INavigationService navigationService, FacadeService facadeService)
        {
            _navigationService = navigationService;
            _facadeService = facadeService;

            AddNewsCancelImage = "AddNewsPostCancel";
            AddNewsNextImage = "AddNewsPostNext";
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
        private ImageSource _AddNewsCancelImage;
        private ImageSource _AddNewsNextImage;
        #endregion

        public bool IsVisiblePlayerBanner
        {
            get { return _isVisiblePlayerBanner; }
            set { SetProperty(ref _isVisiblePlayerBanner, value); }
        }

        private static ImageSource _UserProfileImage = "";
        public ImageSource UserProfileImage
        {
            get { return _UserProfileImage; }
            set { SetProperty(ref _UserProfileImage, value); }
        }
        public ImageSource AddNewsCancelImage
        {
            get { return _AddNewsCancelImage; }
            set { SetProperty(ref _AddNewsCancelImage, value); }
        }
        public ImageSource AddNewsNextImage
        {
            get { return _AddNewsNextImage; }
            set { SetProperty(ref _AddNewsNextImage, value); }
        }

        #region Commands

        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand ComingSoonCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand BackClickCommand { get { return new DelegateCommand(async () => await BackClick()); } }
        public DelegateCommand ClosePopupCommand { get { return new DelegateCommand(async () => await ClosePopup(true)); } }
        public DelegateCommand LeftCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand RightCommand { get { return new DelegateCommand(async () => await DisplayComingSoon()); } }
        public DelegateCommand AddNewsPostCommand { get { return new DelegateCommand(async () => await AddNewNewsPost()); } }
        public DelegateCommand AddNewsPostNextCommand { get { return new DelegateCommand(async () => await AddNewsPostNext()); } }

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
            AddNewsCancelImage = "AddNewsPostCancel";
            AddNewsNextImage = "AddNewsPostNext";
        }

        /// <summary>
        /// DisplayComingSoon
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task DisplayComingSoon()
        {
            await App.Current.MainPage.DisplayAlert(AppResources.AlertTitle, "Coming Soon...", AppResources.AlertOkText);
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
                await _navigationService.NavigateAsync(nameof(AddNewsPostPage), null);
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
                    //if (lastPage.GetType() == typeof(LoadingPopup))
                    //{
                    //    SessionService.isLoadingPopupOpen = false;
                    //}
                    await PopupNavigationService.Instance.PopAsync(animate);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        public async Task AddNewsPostNext()
        {
            await _navigationService.NavigateAsync(nameof(AddNewsPostImagePage));
            AddNewsCancelImage = "AddNewsPostBack";
        }
        #endregion
    }
}
