using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    public class ManageNotificationConnectPageViewModel : ViewModelBase
    {
        #region Constructor
        Connect_Notification_Setting NotificationConnect = new Connect_Notification_Setting();
        public ManageNotificationConnectPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            HeaderDetailsTitle = AppResources.txtConnect;
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = SaveSettingCommand;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize18"];
        }
        #endregion

        #region Private Properties
        public bool isTaponCompany;
        public bool isTaponGroup;
        public bool isTaponEveryone;
        #endregion

        #region Public Properties

        private bool _IsPostFromCompany;
        public bool IsPostFromCompany
        {
            get { return _IsPostFromCompany; }
            set { SetProperty(ref _IsPostFromCompany, value); }
        }

        private bool _IsPostFromGroup;
        public bool IsPostFromGroup
        {
            get { return _IsPostFromGroup; }
            set { SetProperty(ref _IsPostFromGroup, value); }
        }

        private bool _IsPostFromEveryone;
        public bool IsPostFromEveryone
        {
            get { return _IsPostFromEveryone; }
            set { SetProperty(ref _IsPostFromEveryone, value); }
        }

        private bool _IsLikesOnPosts;
        public bool IsLikesOnPosts
        {
            get { return _IsLikesOnPosts; }
            set { SetProperty(ref _IsLikesOnPosts, value); }
        }

        private bool _IsCommentsOnPosts;
        public bool IsCommentsOnPosts
        {
            get { return _IsCommentsOnPosts; }
            set { SetProperty(ref _IsCommentsOnPosts, value); }
        }

        private bool _IsLikesOnComments;
        public bool IsLikesOnComments
        {
            get { return _IsLikesOnComments; }
            set { SetProperty(ref _IsLikesOnComments, value); }
        }

        private bool _IsPostsYouComment;
        public bool IsPostsYouComment
        {
            get { return _IsPostsYouComment; }
            set { SetProperty(ref _IsPostsYouComment, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> SaveSettingCommand { get { return new DelegateCommand<string>(async (obj) => await SaveSetting(obj)); } }
        public DelegateCommand FromYourCompanyCommand { get { return new DelegateCommand(async () => await FromYourCompany()); } }
        public DelegateCommand FromYourGroupCommand { get { return new DelegateCommand(async () => await FromYourGroup()); } }
        public DelegateCommand FromEveryoneCommand { get { return new DelegateCommand(async () => await FromEveryone()); } }
        #endregion

        #region private methods
        async Task FromYourCompany()
        {
            try
            {
                //isTaponCompany = true;
                //isTaponGroup = false;
                //isTaponEveryone = false;

                //if (isTaponCompany)
                //{
                //    IsPostFromCompany = true;
                //    IsPostFromEveryone = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task FromYourGroup()
        {
            try
            {
                //isTaponCompany = false;
                //isTaponGroup = true;
                //isTaponEveryone = false;

                //if (isTaponGroup)
                //{
                //    IsPostFromEveryone = true;
                //    IsPostFromGroup = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task FromEveryone()
        {
            try
            {
                //isTaponCompany = false;
                //isTaponGroup = false;
                //isTaponEveryone = true;

                //if (isTaponEveryone)
                //{
                //    IsPostFromEveryone = true;
                //    IsPostFromCompany = true;
                //    IsPostFromGroup = true;
                //}
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

                NotificationConnect.Connect_IsPostFromCompany = IsPostFromCompany;
                NotificationConnect.Connect_IsPostFromGroup = IsPostFromGroup;
                NotificationConnect.Connect_IsPostFromEveryone = IsPostFromEveryone;

                NotificationConnect.Connect_IsLikesOnPosts = IsLikesOnPosts;
                NotificationConnect.Connect_IsCommentsOnPosts = IsCommentsOnPosts;

                NotificationConnect.Connect_IsLikesOnComments = IsLikesOnComments;
                NotificationConnect.Connect_IsPostsYouComment = IsPostsYouComment;
                NotificationConnect.volUniqueId = SettingsService.VolunteersUserData.volUniqueID;
                await ShowLoader();
                var serviceResult = await NotificationService.SaveConnectNotificationSetting(NotificationConnect);
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
        async void LoadData()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await NotificationService.GetConnectNotificationSetting(SettingsService.VolunteersUserData.volUniqueID);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<NotificationConnectResponseModel>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        NotificationConnect = new Connect_Notification_Setting();
                        List<Connect_Notification_Setting> Data = new List<Connect_Notification_Setting>();
                        Data = serviceResultBody.Data;
                        if (Data != null && Data.Count > 0)
                        {
                            NotificationConnect = Data[0];
                            IsPostFromCompany = NotificationConnect.Connect_IsPostFromCompany;
                            IsPostFromGroup = NotificationConnect.Connect_IsPostFromGroup;
                            IsPostFromEveryone = NotificationConnect.Connect_IsPostFromEveryone;

                            IsLikesOnPosts = NotificationConnect.Connect_IsLikesOnPosts;
                            IsCommentsOnPosts = NotificationConnect.Connect_IsCommentsOnPosts;

                            IsLikesOnComments = NotificationConnect.Connect_IsLikesOnComments;
                            IsPostsYouComment = NotificationConnect.Connect_IsPostsYouComment;
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

        #region public methods

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            NotificationConnect = new Connect_Notification_Setting();
            LoadData();
        }
    }
}


