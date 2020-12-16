using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.NotificationModel;

namespace AtWork.ViewModels
{
    public class ManageNotificationPageViewModel : ViewModelBase
    {
        #region Constructor
        public ManageNotificationPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            HeaderDetailsTitle = AppResources.ManageNotificationText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize18"];

            //NextClickPageName = nameof(AddNewsPostPage);
            //AddNewsCancelImage = AppResources.Cancel;
            //AddNewsNextImage = AppResources.NextButtonText;
            //HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        //bool tempIsNotificationPause;
        private string _txtPausenotifications = AppResources.Pausenotifications;
        private string _txtPausenotificationsTime;
        private bool _isPauseNotification = false;
        private Color _txtColorPauseNotification = (Color)App.Current.Resources["AccentColor"];
        private Color _bgColorTextNotification = (Color)App.Current.Resources["OffWhiteColor"];
        Notification notification = new Notification();
        #endregion

        #region Public Properties        
        public string txtPausenotifications
        {
            get { return _txtPausenotifications; }
            set { SetProperty(ref _txtPausenotifications, value); }
        }
        public string txtPausenotificationsTime
        {
            get { return _txtPausenotificationsTime; }
            set { SetProperty(ref _txtPausenotificationsTime, value); }
        }
        public bool isPauseNotification
        {
            get { return _isPauseNotification; }
            set { SetProperty(ref _isPauseNotification, value); }
        }
        public Color bgColorTextNotification
        {
            get { return _bgColorTextNotification; }
            set { SetProperty(ref _bgColorTextNotification, value); }
        }
        public Color txtColorPauseNotification
        {
            get { return _txtColorPauseNotification; }
            set { SetProperty(ref _txtColorPauseNotification, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand ConnectCommand { get { return new DelegateCommand(async () => await OpenConnectPage()); } }
        public DelegateCommand ActiveCommand { get { return new DelegateCommand(async () => await OpenActivePage()); } }
        public DelegateCommand NotificationSwitchCommand { get { return new DelegateCommand(async () => await NotificationSwitch()); } }
       
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
        async Task NotificationSwitch()
        {
            try
            {
                await OpenNotificationDialog();
                //await _navigationService.NavigateAsync(nameof(ManageNotificationActivePage));
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

                
                notification = new Notification();
                notification.volUniqueId = SettingsService.VolunteersUserData.volUniqueID;
                if (!isPauseNotification)
                {
                    NotificationPopup notificationPopup = new NotificationPopup();
                    NotificationPopupViewModel notificationPopupViewModel = new NotificationPopupViewModel(_navigationService, _facadeService);
                    notificationPopupViewModel.ClosePopupEvent += async (object sender, bool SelectedObj) =>
                    {
                        //isPauseNotification = false;
                        //isPauseNotification = tempIsNotificationPause;
                    };
                    notificationPopupViewModel.SaveNotificationEvent += async (object sender, string SelectedTime) =>
                    {
                        int totalMin = 0;
                        
                        if (SelectedTime == AppResources.txtPauseFor30Min)
                        {
                            notification.PauseTimeMinute = 30;
                            notification.PauseTime = AppResources.txtPauseFor30Min;
                        }
                        else if(SelectedTime == AppResources.txtPauseFor1Hour)
                        {
                            notification.PauseTimeMinute = 60;
                            notification.PauseTime = AppResources.txtPauseFor1Hour;
                        }
                        else if (SelectedTime == AppResources.txtPauseFor1day)
                        {
                            notification.PauseTimeMinute = 60*24;
                            notification.PauseTime = AppResources.txtPauseFor1day;
                        }
                        else if (SelectedTime == AppResources.txtPauseFor1week)
                        {
                            notification.PauseTimeMinute = 60 * 24 * 7;
                            notification.PauseTime = AppResources.txtPauseFor1week;
                        }
                        else if (SelectedTime == AppResources.txtPauseForForever)
                        {
                            notification.IsForever = true;
                            notification.PauseTime = AppResources.txtPauseForForever;
                        }
                        notification.PauseNotificationStarttime = DateTime.Parse(DateTime.Now.ToString());
                        notification.PauseNotificationEndtime = DateTime.Parse(DateTime.Now.AddMinutes(notification.PauseTimeMinute).ToString());
                        notification.IsPaused = true;
                        
                        await ShowLoader();
                        var serviceResult = await NotificationService.SaveNotificationSetting(notification);
                        await ClosePopup();
                        if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                        {
                            var serviceResultBody = JsonConvert.DeserializeObject<NotificationSaveResponseModel>(serviceResult.Body);
                            if (serviceResultBody != null)
                            {
                                notification = serviceResultBody.Data;

                                isPauseNotification = true;
                                //txtPausenotificationsTime = "Until";
                                //notification.IsPaused = true;

                                if (notification.IsForever)
                                    txtPausenotificationsTime = AppResources.txtPauseForForever;
                                else
                                    txtPausenotificationsTime = notification?.FormattedDate;
                                if (notification.IsPaused)
                                {
                                    txtPausenotifications = AppResources.txtPaused;
                                    bgColorTextNotification = (Color)App.Current.Resources["AccentColor"];
                                    txtColorPauseNotification = (Color)App.Current.Resources["OffWhiteColor"];
                                }
                                else
                                {
                                    txtPausenotifications = AppResources.Pausenotifications;
                                    bgColorTextNotification = (Color)App.Current.Resources["OffWhiteColor"];
                                    txtColorPauseNotification = (Color)App.Current.Resources["AccentColor"];
                                }
                            }
                        }
                    };
                    notificationPopup.BindingContext = notificationPopupViewModel;
                    await PopupNavigationService.ShowPopup(notificationPopup, true);
                }
                else
                {
                    notification.IsPaused = false;
                    await ShowLoader();
                    var serviceResult = await NotificationService.SaveNotificationSetting(notification);
                    await ClosePopup();
                    //if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                    {
                        ToastMessagePopup ToastMessagePopup = new ToastMessagePopup();
                        ToastMessagePopupViewModel ToastMessagePopupViewModel = new ToastMessagePopupViewModel(_navigationService, _facadeService);
                        ToastMessagePopupViewModel.ToastText = AppResources.txtNotificationsAreNoLongerPaused;
                        ToastMessagePopup.BindingContext = ToastMessagePopupViewModel;
                        await PopupNavigationService.ShowPopup(ToastMessagePopup, true);

                        txtPausenotificationsTime = string.Empty;
                        isPauseNotification = false;
                        txtPausenotifications = AppResources.Pausenotifications;
                        bgColorTextNotification = (Color)App.Current.Resources["OffWhiteColor"];
                        txtColorPauseNotification = (Color)App.Current.Resources["AccentColor"];
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        public async void LoadData()
        {
            try
            {
                await ShowLoader();
                Notification input = new Notification();
                input.volUniqueId = SettingsService.VolunteersUserData.volUniqueID;
                input.PauseNotificationCurrentTime = DateTime.Parse(DateTime.Now.ToString());
                BaseResponse<string> serviceResult = await NotificationService.GetNotificationSetting_v1(input);
                //BaseResponse<string> serviceResult = await NotificationService.GetNotificationSetting(SettingsService.VolunteersUserData.volUniqueID);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<NotificationResponseModel>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag && serviceResultBody.Data != null)
                    {
                        List<Notification> list = new List<Notification>();
                        list = serviceResultBody.Data;
                        if (list != null && list.Count > 0)
                        {
                            notification = list[0];
                            isPauseNotification = notification.IsPaused;
                            if (notification.IsForever)
                                txtPausenotificationsTime = AppResources.txtPauseForForever;
                            else
                                txtPausenotificationsTime = notification?.FormattedDate;
                        }
                    }
                }
                if (isPauseNotification)
                {
                    txtPausenotifications = AppResources.txtPaused;
                    bgColorTextNotification = (Color)App.Current.Resources["AccentColor"];
                    txtColorPauseNotification = (Color)App.Current.Resources["OffWhiteColor"];
                }
                else
                {

                    txtPausenotifications = AppResources.Pausenotifications;
                    bgColorTextNotification = (Color)App.Current.Resources["OffWhiteColor"];
                    txtColorPauseNotification = (Color)App.Current.Resources["AccentColor"];

                    ToastMessagePopup ToastMessagePopup = new ToastMessagePopup();
                    ToastMessagePopupViewModel ToastMessagePopupViewModel = new ToastMessagePopupViewModel(_navigationService, _facadeService);
                    ToastMessagePopupViewModel.ToastText = AppResources.txtNotificationsAreNoLongerPaused;
                    ToastMessagePopup.BindingContext = ToastMessagePopupViewModel;
                    await PopupNavigationService.ShowPopup(ToastMessagePopup, true);
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
            LoadData();
            //tempIsNotificationPause = isPauseNotification;
        }
    }
}

