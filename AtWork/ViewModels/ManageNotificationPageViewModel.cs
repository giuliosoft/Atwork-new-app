using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ManageNotificationPageViewModel : ViewModelBase
    {
        #region Constructor
        public ManageNotificationPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            HeaderDetailsTitle = AppResources.ManageNotificationText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];

            //NextClickPageName = nameof(AddNewsPostPage);
            //AddNewsCancelImage = AppResources.Cancel;
            //AddNewsNextImage = AppResources.NextButtonText;
            //HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _isPauseNotification = false;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public bool isPauseNotification
        {
            get { return _isPauseNotification; }
            set { SetProperty(ref _isPauseNotification, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand ConnectCommand { get { return new DelegateCommand(async () => await OpenConnectPage()); } }
        public DelegateCommand ActiveCommand { get { return new DelegateCommand(async () => await OpenActivePage()); } }
       
        #endregion

        #region private methods
        async Task OpenConnectPage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ManageNotificationConnectPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task OpenActivePage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ManageNotificationActivePage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        #region public methods
        public async Task OpenNotificationDialog()
        {
            try
            {
                if (isPauseNotification)
                {
                    NotificationPopup notificationPopup = new NotificationPopup();
                    NotificationPopupViewModel notificationPopupViewModel = new NotificationPopupViewModel(_navigationService, _facadeService);
                    notificationPopupViewModel.ClosePopupEvent += async (object sender, bool SelectedObj) =>
                    {
                        isPauseNotification = false;
                    };
                    notificationPopup.BindingContext = notificationPopupViewModel;
                    await PopupNavigationService.ShowPopup(notificationPopup, true);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}

