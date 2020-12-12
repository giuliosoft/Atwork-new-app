using System;
using System.Collections.Generic;
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
using static AtWork.Models.NotificationModel;

namespace AtWork.ViewModels
{
    public class ManageNotificationActivePageViewModel : ViewModelBase
    {
        #region Constructor
        Activity_Notification_Setting NotificationActivity = new Activity_Notification_Setting();
        public ManageNotificationActivePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            HeaderDetailsTitle = AppResources.txtActive;
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = SaveSettingCommand;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize18"];
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        private bool _SomeoneRegister;
        public bool SomeoneRegister
        {
            get { return _SomeoneRegister; }
            set { SetProperty(ref _SomeoneRegister, value); }
        }
        private bool _SomeoneCancelled;
        public bool SomeoneCancelled
        {
            get { return _SomeoneCancelled; }
            set { SetProperty(ref _SomeoneCancelled, value); }
        }


        private bool _ActivityCancelled;
        public bool ActivityCancelled
        {
            get { return _ActivityCancelled; }
            set { SetProperty(ref _ActivityCancelled, value); }
        }

        private bool _ActivityReminder;
        public bool ActivityReminder
        {
            get { return _ActivityReminder; }
            set { SetProperty(ref _ActivityReminder, value); }
        }

        private bool _FeedbackReminder;
        public bool FeedbackReminder
        {
            get { return _FeedbackReminder; }
            set { SetProperty(ref _FeedbackReminder, value); }
        }

        private bool _StatusUpdate;
        public bool StatusUpdate
        {
            get { return _StatusUpdate; }
            set { SetProperty(ref _StatusUpdate, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> SaveSettingCommand { get { return new DelegateCommand<string>(async (obj) => await SaveSetting(obj)); } }
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
                await _navigationService.NavigateAsync(nameof(ManageNotificationConnectPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task SaveSetting(string str)
        {
            try
            {
                NotificationActivity = new Activity_Notification_Setting();
                NotificationActivity.volUniqueId = SettingsService.VolunteersUserData.volUniqueID;
                NotificationActivity.Active_IsYGTSomeoneRegister = SomeoneRegister;
                NotificationActivity.Active_IsYGTSomeoneCancelled = SomeoneCancelled;
                NotificationActivity.Active_IsAllActActivityCancelled = ActivityCancelled;
                NotificationActivity.Active_IsAllActActivityReminder = ActivityReminder;
                NotificationActivity.Active_IsAllActFeedbackReminder = FeedbackReminder;
                NotificationActivity.Active_IsPetitionsStatusUpdate = StatusUpdate;

                await ShowLoader();
                var serviceResult = await NotificationService.SaveActivityNotificationSetting(NotificationActivity);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    await _navigationService.GoBackAsync();
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        #region public methods
        async void LoadData()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await NotificationService.GetActivityNotificationSetting(SettingsService.VolunteersUserData.volUniqueID);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<NotificationActivityResponseModel>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        NotificationActivity = new Activity_Notification_Setting();
                        List<Activity_Notification_Setting> Data = new List<Activity_Notification_Setting>();
                        Data = serviceResultBody.Data;
                        if (Data != null && Data.Count > 0)
                        {
                            NotificationActivity = Data[0];

                            SomeoneRegister = NotificationActivity.Active_IsYGTSomeoneRegister;
                            SomeoneCancelled = NotificationActivity.Active_IsYGTSomeoneCancelled;
                            ActivityCancelled = NotificationActivity.Active_IsAllActActivityCancelled;
                            ActivityReminder = NotificationActivity.Active_IsAllActActivityReminder;
                            FeedbackReminder = NotificationActivity.Active_IsAllActFeedbackReminder;
                            StatusUpdate = NotificationActivity.Active_IsPetitionsStatusUpdate;
                        }
                    }
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
        }
    }
}

