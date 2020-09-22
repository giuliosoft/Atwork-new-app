using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class PostNewsPageViewModel : ViewModelBase
    {
        bool isActivity = false;
        #region Constructor
        public PostNewsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(PostNewsPage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.PublishText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private Color _PublishGroupColor = (Color)App.Current.Resources["AccentColor"];
        private Color _PublishPublicColor = Color.Transparent;
        private string NewsPrivacy = "mygroup";
        private Color _GroupTextColor = Color.White;
        private Color _PublicTextColor = (Color)App.Current.Resources["AccentColor"];
        private string _PostTitle = string.Empty;
        private string _PostToGroup = string.Empty;
        private string _GroupMember = string.Empty;
        private string _PostToEverybodyText = string.Empty;
        private bool _IsShowOption = false;
        #endregion

        #region Public Properties        
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
        public string PostTitle
        {
            get { return _PostTitle; }
            set { SetProperty(ref _PostTitle, value); }
        }
        public string PostToGroup
        {
            get { return _PostToGroup; }
            set { SetProperty(ref _PostToGroup, value); }
        }
        public string GroupMember
        {
            get { return _GroupMember; }
            set { SetProperty(ref _GroupMember, value); }
        }
        public string PostToEverybodyText
        {
            get { return _PostToEverybodyText; }
            set { SetProperty(ref _PostToEverybodyText, value); }
        }
        public bool IsShowOption
        {
            get { return _IsShowOption; }
            set { SetProperty(ref _IsShowOption, value); }
        }

        public Color PublishGroupColor
        {
            get { return _PublishGroupColor; }
            set { SetProperty(ref _PublishGroupColor, value); }
        }

        public Color PublishPublicColor
        {
            get { return _PublishPublicColor; }
            set { SetProperty(ref _PublishPublicColor, value); }
        }

        public Color GroupTextColor
        {
            get { return _GroupTextColor; }
            set { SetProperty(ref _GroupTextColor, value); }
        }

        public Color PublicTextColor
        {
            get { return _PublicTextColor; }
            set { SetProperty(ref _PublicTextColor, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand PostToEverybodyCommand { get { return new DelegateCommand(async () => await PostToEverybody()); } }
        public DelegateCommand PostToYourGroupCommand { get { return new DelegateCommand(async () => await PostToYourGroup()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
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

        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                if (isActivity)
                {
                    BaseResponse<string> serviceResult = null;
                    ActivityListModel input = new ActivityListModel();
                    input.coUniqueID = SettingsService.LoggedInUserData?.coUniqueID;
                    input.volUniqueID = SettingsService.VolunteersUserData?.volUniqueID.ToString();
                    input.proTitle = SessionService.ActivityPostInputData.proTitle;
                    input.proDescription = SessionService.ActivityPostInputData.proDescription;
                    input.proAddress1 = SessionService.ActivityPostInputData.proAddress1;
                    input.proCity = SessionService.ActivityPostInputData.proCity;
                    input.proCountry = SessionService.ActivityPostInputData.proCountry;
                    input.proAddActivity_StartTime = SessionService.ActivityPostInputData.proAddActivity_StartTime;
                    input.proAddActivityDate = SessionService.ActivityPostInputData.proAddActivityDate;
                    input.Emoji = SessionService.SelectedEmojiForActivity;
                    input.proPublishedDate = DateTime.Now;
                    input.proStatus = "Ongoing";
                    input.proCostCoveredEmployee = SessionService.ActivityPostInputData.proCostCoveredEmployee;
                    input.proBackgroundImage = SessionService.SelectedDefaultImageForActivity;
                    input.proCategoryName = "#gettogether";
                    if (NewsPrivacy.ToLower() == "everyone")
                    {
                        input.proAudience = "Post to everybody";
                    }
                    else
                    {
                        input.proAudience = "Post to my group";
                    }
                    serviceResult = await ActivityService.PostActivityFeedEdit(input, SessionService.NewsPostImageFiles);
                    if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                    {
                        SessionService.ActivityPostInputData = new ActivityListModel();
                        SessionService.NewsPostImageFiles = new List<string>();
                        SessionService.IsShowActivitiesIntial = true;
                        await _navigationService.NavigateAsync(nameof(DashboardPage));
                    }
                }
                else
                {
                    SessionService.NewsPostInputData.coUniqueID = SettingsService.LoggedInUserData.coUniqueID;
                    NewsDetailModel_Input inputModel = new NewsDetailModel_Input();

                    inputModel.coUniqueID = SessionService.NewsPostInputData.coUniqueID;
                    inputModel.newsTitle = SessionService.NewsPostInputData.newsTitle;
                    inputModel.newsContent = SessionService.NewsPostInputData.newsContent;
                    inputModel.newsDateTime = DateTime.Now;
                    inputModel.newsPostedTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(SessionService.NewsPostAttachmentFileName))
                    {
                        inputModel.newsFileOriginal = SessionService.NewsPostAttachmentFileName;
                    }
                    else
                    {
                        inputModel.newsFileOriginal = SessionService.NewsPostInputData.newsFileOriginal;
                    }
                    inputModel.newsFile = SessionService.NewsPostInputData.newsFile;
                    inputModel.newsImage = "";
                    inputModel.newsOrigin = "employee";
                    inputModel.newsStatus = "Active";
                    inputModel.newsPrivacy = NewsPrivacy;
                    if (!string.IsNullOrEmpty(SessionService.NewsPostAttachmentFilePath))
                        SessionService.NewsPostImageFiles.Add(SessionService.NewsPostAttachmentFilePath);

                    BaseResponse<string> serviceResult = null;
                    if (SessionService.isEditingNews)
                    {
                        inputModel.newsUniqueID = SessionService.NewsPostInputData.newsUniqueID;
                        inputModel.volUniqueID = SessionService.NewsPostInputData.volUniqueID;
                        if (SessionService.NewsPostCarouselImages != null)
                        {
                            inputModel.newsImage = String.Join(",", SessionService.NewsPostCarouselImages.Where((x) => !string.IsNullOrEmpty(x)).Select(x => x.Replace(ConfigService.BaseNewsImageURL, "")).ToList());
                        }
                        serviceResult = await NewsService.PostNewsFeedEdit(inputModel, SessionService.NewsPostImageFiles);
                    }
                    else
                    {
                        inputModel.newsUniqueID = null;
                        inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                        serviceResult = await NewsService.PostNewsFeed(inputModel, SessionService.NewsPostImageFiles);
                    }

                    if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                    {
                        SessionService.NewsPostInputData = new NewsDetailModel_Input();
                        SessionService.NewsPostAttachmentFileName = string.Empty;
                        SessionService.NewsPostAttachmentFilePath = string.Empty;
                        SessionService.NewsPostImageFiles = new List<string>();
                        if (SessionService.isEditingNews)
                        {
                            SessionService.isEditingNews = false;
                            SessionService.IsNeedToRefreshNews = true;
                        }
                        await _navigationService.NavigateAsync(nameof(DashboardPage));
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
        }

        async Task PostToEverybody()
        {
            try
            {
                NewsPrivacy = "Everyone";
                PublishGroupColor = Color.Transparent;
                PublishPublicColor = (Color)App.Current.Resources["AccentColor"];
                GroupTextColor = (Color)App.Current.Resources["AccentColor"];
                PublicTextColor = Color.White;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task PostToYourGroup()
        {
            try
            {
                NewsPrivacy = "mygroup";
                PublishGroupColor = (Color)App.Current.Resources["AccentColor"];
                PublishPublicColor = Color.Transparent;
                PublicTextColor = (Color)App.Current.Resources["AccentColor"];
                GroupTextColor = Color.White;
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
                isActivity = parameters.GetValue<bool>("isFromActivity");
                if (isActivity)
                {
                    PostTitle = AppResources.InvitePeopleToYourActivity;
                    PostToGroup = AppResources.Inviteyourgroup;
                    GroupMember = string.Format("{0} {1}", SessionService.GroupMemberCount, AppResources.GroupMemberPost);// "21 people will see your post";
                }
                else
                {
                    PostTitle = AppResources.ChooseWhoCanSeeYourPost;
                    PostToGroup = AppResources.Posttoyourgroup;
                    GroupMember = string.Format("{0} {1}", SessionService.GroupMemberCount, AppResources.GroupMemberPost);
                }
                    PostToEverybodyText = AppResources.PostToEverybody;
                IsShowOption = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
