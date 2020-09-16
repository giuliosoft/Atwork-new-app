using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class DashboardPageViewModel : ViewModelBase
    {
        #region Constructor
        public DashboardPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.AllCategoriesText });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.CorporateVolunteeringText, categoryId = TextResources.CorpVolID });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.SportsText, categoryId = TextResources.SportsID });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.EducationsText, categoryId = TextResources.EducationID });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.CultureText, categoryId = TextResources.CultureID });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.CompanyEventsText, categoryId = TextResources.CompEventsID });
            Activitycollectionlist.Add(new ActivityItems() { title = AppResources.GetTogetherText, categoryId = TextResources.GetTogetherID });

            //NewsGreenbg = (Color)App.Current.Resources["AccentColor"];
            //ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];
            FooterNavigationCommand = DashboardFooterNavigationCommand;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            ProfileTapCommand = DashboardProfileTapCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _NewsViewIsVisible = true;
        private bool _ActivityViewIsVisible = false;
        private int PageNo = 1;
        private int ActivityPageNo = 1;
        private ObservableCollection<ActivityListModel> _Activitylist = new ObservableCollection<ActivityListModel>();
        private ObservableCollection<CarouselModel> _NewsImageCarouselList = new ObservableCollection<CarouselModel>();
        private ObservableCollection<NewsListData_Model> _NewsList = new ObservableCollection<NewsListData_Model>();
        private ObservableCollection<ActivityItems> _Activitycollectionlist = new ObservableCollection<ActivityItems>();
        bool _isRefreshing = false;
        bool _isBusy = false;
        private int _remainingItemsThreshold = 0;
        bool _IsRefreshingActivities = false;
        private int _remainingActivityItemsThreshold = 0;
        bool _isBusyInActivityBinding = false;
        bool _isRefreshingNewsFirstTime = true;
        bool _isRefreshingActivityFirstTime = true;
        private string SelectedActivityCategoryID = string.Empty;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public bool NewsViewIsVisible
        {
            get { return _NewsViewIsVisible; }
            set { SetProperty(ref _NewsViewIsVisible, value); }
        }

        public bool ActivityViewIsVisible
        {
            get { return _ActivityViewIsVisible; }
            set { SetProperty(ref _ActivityViewIsVisible, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public int RemainingItemsThreshold
        {
            get { return _remainingItemsThreshold; }
            set { SetProperty(ref _remainingItemsThreshold, value); }
        }

        public ObservableCollection<CarouselModel> NewsImageCarouselList
        {
            get { return _NewsImageCarouselList; }
            set { SetProperty(ref _NewsImageCarouselList, value); }
        }

        public ObservableCollection<NewsListData_Model> NewsList
        {
            get { return _NewsList; }
            set { SetProperty(ref _NewsList, value); }
        }

        public ObservableCollection<ActivityListModel> Activitylist
        {
            get { return _Activitylist; }
            set { SetProperty(ref _Activitylist, value); }
        }

        public ObservableCollection<ActivityItems> Activitycollectionlist
        {
            get { return _Activitycollectionlist; }
            set { SetProperty(ref _Activitycollectionlist, value); }
        }

        public bool IsRefreshingActivities
        {
            get { return _IsRefreshingActivities; }
            set { SetProperty(ref _IsRefreshingActivities, value); }
        }

        public int RemainingActivityItemsThreshold
        {
            get { return _remainingActivityItemsThreshold; }
            set { SetProperty(ref _remainingActivityItemsThreshold, value); }
        }

        public bool IsBusyInActivityBinding
        {
            get { return _isBusyInActivityBinding; }
            set { SetProperty(ref _isBusyInActivityBinding, value); }
        }
        private static ImageSource _UserProfileImage = string.Empty;
        public ImageSource UserProfileImageHeader
        {
            get { return _UserProfileImage; }
            set { SetProperty(ref _UserProfileImage, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<string> DashboardFooterNavigationCommand { get { return new DelegateCommand<string>(async (obj) => await DashboardFooterNavigation(obj)); } }
        public DelegateCommand<NewsListData_Model> NewsPostSelectedCommand { get { return new DelegateCommand<NewsListData_Model>(async (obj) => await GotoNewsPostDetailPage(obj)); } }
        public DelegateCommand<ActivityListModel> ActivityPostSelectedCommand { get { return new DelegateCommand<ActivityListModel>(async (obj) => await GotoActivityPostDetailPage(obj)); } }
        public DelegateCommand<NewsModel> EditNewsPostCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await EditNewsPost(obj)); } }
        public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        //public DelegateCommand<News> NewsShowOptionCommand { get { return new DelegateCommand<News>(async (obj) => await NewsShowOption(obj)); } }
        public DelegateCommand<NewsListData_Model> NewsShowOptionCommand { get { return new DelegateCommand<NewsListData_Model>(async (obj) => await NewsShowOption(obj)); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
        public DelegateCommand NewsLoadMoreItemsCommand { get { return new DelegateCommand(async () => await NewsLoadMoreItems()); } }
        public DelegateCommand ActivityLoadMoreItemsCommand { get { return new DelegateCommand(async () => await ActivityLoadMoreItems()); } }
        public DelegateCommand RefreshCommand { get { return new DelegateCommand(async () => await ExecuteRefreshCommand()); } }
        public DelegateCommand ActivityRefreshCommand { get { return new DelegateCommand(async () => await ExecuteActivityRefreshCommand()); } }
        public DelegateCommand<string> ActivityPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await ActivityPostProceed(obj)); } }
        public DelegateCommand<ActivityListModel> JoinedMemberCommand { get { return new DelegateCommand<ActivityListModel>(async (obj) => await JoinedMember(obj)); } }
        public DelegateCommand<ActivityItems> ActivityCategorySelectedCommand { get { return new DelegateCommand<ActivityItems>(async (obj) => await ActivityCategorySelected(obj)); } }
        public DelegateCommand DashboardProfileTapCommand { get { return new DelegateCommand(async () => await ProfileTapped()); } }
        #endregion

        #region private methods
        async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            PageNo = 1;
            await GetNewsListDetails_New(true);
            IsRefreshing = false;
        }

        async Task ExecuteActivityRefreshCommand()
        {
            IsRefreshingActivities = true;
            ActivityPageNo = 1;
            await GetActivityList(isPullToRefresh: true);
            Activitycollectionlist.All((categories) =>
            {
                if (categories.title == AppResources.AllCategoriesText)
                {
                    categories.UnderlineIsVisible = true;
                }
                else
                {
                    categories.UnderlineIsVisible = false;
                }
                return true;
            });
            IsRefreshingActivities = false;
        }

        async Task NewsLoadMoreItems()
        {
            if (IsBusy)
            {
                return;
            }
            PageNo++;
            await GetNewsListDetails_New();
        }

        async Task ActivityLoadMoreItems()
        {
            if (IsBusyInActivityBinding)
            {
                return;
            }
            ActivityPageNo++;
            //await GetActivityList();
        }

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

        async Task ActivityCategorySelected(ActivityItems selectedCategory)
        {
            try
            {
                SelectedActivityCategoryID = selectedCategory.categoryId;
                Activitycollectionlist.All((categories) =>
                {
                    if (selectedCategory.title == categories.title)
                    {
                        categories.UnderlineIsVisible = true;
                    }
                    else
                    {
                        categories.UnderlineIsVisible = false;
                    }
                    return true;
                });
                await GetActivityList(SelectedActivityCategoryID);
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
                await _navigationService.NavigateAsync(nameof(AddNewsPostPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task ActivityPostProceed(string selectedTab)
        {
            try
            {
                IsFromMyActivity = true;
                await _navigationService.NavigateAsync(nameof(MyActivityPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task JoinedMember(ActivityListModel selectedActivityPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ActivityID", selectedActivityPost.proUniqueID);
                await _navigationService.NavigateAsync(nameof(MemberListPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task ProfileTapped()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ProfilePage));
            }
            catch (Exception ex)
            {
            }
        }

        async Task DashboardFooterNavigation(string selectedTab)
        {
            try
            {
                if (selectedTab == TextResources.NewsTabText)
                {
                    NewsViewIsVisible = true;
                    ActivityViewIsVisible = false;

                    if (_isRefreshingNewsFirstTime || SessionService.IsNeedToRefreshNews)
                    {
                        if (_isRefreshingNewsFirstTime) { _isRefreshingNewsFirstTime = false; };
                        if (SessionService.IsNeedToRefreshNews) { SessionService.IsNeedToRefreshNews = false; };
                        await GetNewsListDetails_New();
                    }

                    NextOptionText = "+";
                    NextCustomLabelIsVisible = false;
                    //NewsGreenbg = (Color)App.Current.Resources["AccentColor"];
                    //ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];

                    HeaderNextNavigationCommand = NewsPostProceedCommand;
                }
                else if (selectedTab == TextResources.ActivityTabText)
                {
                    NewsViewIsVisible = false;
                    ActivityViewIsVisible = true;

                    if (_isRefreshingActivityFirstTime)
                    {
                        _isRefreshingActivityFirstTime = false;
                        await GetActivityList();
                    }

                    NextCustomLabelIsVisible = true;
                    NextOptionText = AppResources.MyActivitiesHeaderText;
                    //NewsGreenbg = (Color)App.Current.Resources["LightBrownColor"];
                    //ActivitiesGreenbg = (Color)App.Current.Resources["AccentColor"];

                    HeaderNextNavigationCommand = ActivityPostProceedCommand;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GotoNewsPostDetailPage(NewsListData_Model selectedNewsPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("SelectedNewsID", selectedNewsPost.news.id);
                await _navigationService.NavigateAsync(nameof(NewsDetailPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GotoActivityPostDetailPage(ActivityListModel selectedActivityPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("SelectedActivityID", selectedActivityPost.proUniqueID);
                navigationParams.Add("SelectedActivityImages", selectedActivityPost.ActivityCarouselList);
                await _navigationService.NavigateAsync(nameof(ActivityDetailPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task EditNewsPost(NewsModel selectedNewsPost)
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(NewsDetailPage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GotoActivityDetails()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ActivityDetailPage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task NewsShowOption(NewsListData_Model selectedNewsPost)
        {
            try
            {
                //await NewsOption();
                NewsOptionPopup newsOptionPopup = new NewsOptionPopup();
                NewsOptionPopupViewModel newsOptionPopupViewModel = new NewsOptionPopupViewModel(_navigationService, _facadeService);
                newsOptionPopupViewModel.EditNewsEvent += async (object sender, object SelectedObj) =>
                {
                    try
                    {
                        await ShowLoader();
                        SessionService.isEditingNews = true;
                        SessionService.NewsPostInputData.newsTitle = selectedNewsPost.newsTitle;
                        SessionService.NewsPostInputData.newsContent = selectedNewsPost.newsDescription;
                        SessionService.NewsPostInputData.newsUniqueID = selectedNewsPost.news.newsUniqueID;
                        SessionService.NewsPostInputData.volUniqueID = selectedNewsPost.Volunteers.volUniqueID;
                        if (selectedNewsPost.NewsCarouselList != null && selectedNewsPost.NewsCarouselList.Count > 0)
                        {
                            var tempList = new List<string>();
                            selectedNewsPost.NewsCarouselList.All((arg) =>
                            {
                                tempList.Add(arg.NewsImageUrl);
                                return true;
                            });
                            SessionService.NewsPostCarouselImages = tempList;
                        }

                        await _navigationService.NavigateAsync(nameof(AddNewsPostPage), null);
                        await ClosePopup();
                    }
                    catch (Exception ex)
                    {
                        await ClosePopup();
                        Debug.WriteLine(ex.Message);
                    }
                };

                newsOptionPopupViewModel.DeleteNewsEvent += async (object sender, object SelectedObj) =>
                {
                    try
                    {
                        if (!await CheckConnectivity())
                        {
                            return;
                        }
                        var result = await App.Current.MainPage.DisplayAlert(AppResources.Delete, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                        if (result)
                        {
                            await ShowLoader();
                            var serviceResult = await NewsService.DeleteNewsPost(selectedNewsPost.news.id);
                            if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                            {
                                NewsList.Remove(selectedNewsPost);
                                //await GetNewsListDetails_New();
                            }
                            await ClosePopup();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                };
                newsOptionPopup.BindingContext = newsOptionPopupViewModel;
                await PopupNavigationService.ShowPopup(newsOptionPopup, true);
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GetNewsListDetails_New(bool isPullToRefresh = false)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!isPullToRefresh)
                    await ShowLoader();
                var serviceResult = await NewsService.GetNewsList(SettingsService.LoggedInUserData.coUniqueID + "/" + PageNo);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsListResponse>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        if (serviceResultBody.Data != null && serviceResultBody.Data.Count < 5)
                            RemainingItemsThreshold = -1;
                        else
                            RemainingItemsThreshold = 0;
                        if (serviceResultBody.Data != null)
                        {
                            var newsListData = serviceResultBody.Data;
                            if (PageNo == 1)
                            {
                                NewsList.Clear();
                            }
                            var tempList = new ObservableCollection<NewsListData_Model>(NewsList);
                            newsListData.All((nArg) =>
                            {
                                var tempData = new NewsListData_Model();
                                tempData.LikeCount = nArg.LikeCount;
                                tempData.CommentsCount = nArg.CommentsCount;
                                tempData.NewsCreatedTime = nArg.Day;
                                tempData.news = nArg.news;
                                tempData.Volunteers = nArg.Volunteers;
                                tempData.userName = nArg.Volunteers != null ? nArg.Volunteers.volFirstName + " " + nArg.Volunteers.volLastName : string.Empty;
                                tempData.newsPostUserProfilePic = !string.IsNullOrEmpty(nArg.Volunteers?.volPicture) ? ConfigService.BaseProfileImageURL + nArg.Volunteers?.volPicture : string.Empty;
                                tempData.newsTitle = nArg.news.newsTitle;
                                tempData.newsDescription = nArg.news?.newsContent;

                                if (nArg.news?.newsPrivacy.ToLower() == "everyone")
                                    tempData.newsPostPublishType = "earth";
                                else
                                    tempData.newsPostPublishType = "ActivityPeopleIcon";

                                if (!string.IsNullOrEmpty(nArg.news?.newsImage))
                                {
                                    string imgStr = nArg.news?.newsImage;
                                    List<string> nimgUrlList = new List<string>();
                                    if (!string.IsNullOrEmpty(imgStr))
                                    {
                                        if (imgStr.Contains(","))
                                        {
                                            nimgUrlList = imgStr.Split(',').ToList();
                                        }
                                        else
                                        {
                                            nimgUrlList.Add(imgStr);
                                        }
                                    }
                                    if (nimgUrlList != null && nimgUrlList.Count > 0)
                                    {
                                        tempData.NewsCarouselList = new ObservableCollection<NewsCarouselListModel>();
                                        nimgUrlList.All((arg) =>
                                        {
                                            string imageUri = ConfigService.BaseNewsImageURL + arg;
                                            tempData.NewsCarouselList.Add(new NewsCarouselListModel() { NewsImage = ImageSource.FromUri(new Uri(imageUri)), NewsImageUrl = imageUri }); ;
                                            return true;
                                        });
                                    }
                                }
                                else
                                {
                                    tempData.NewsCarouselList = new ObservableCollection<NewsCarouselListModel>();
                                    tempData.NewsCarouselList.Add(new NewsCarouselListModel() { NewsImage = "noimage" });
                                }
                                tempList.Add(tempData);
                                return true;
                            });
                            NewsList = new ObservableCollection<NewsListData_Model>(tempList);
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
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetActivityList(string categoryId = "", bool isPullToRefresh = false)
        {
            try
            {
                if (IsBusyInActivityBinding)
                {
                    return;
                }
                IsBusyInActivityBinding = true;
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!isPullToRefresh)
                    await ShowLoader();
                BaseResponse<string> serviceResult = null;
                if (string.IsNullOrEmpty(categoryId))
                {
                    serviceResult = await ActivityService.GetActivityList(SettingsService.LoggedInUserData.coUniqueID);
                }
                else
                {
                    serviceResult = await ActivityService.GetActivityList(SettingsService.LoggedInUserData.coUniqueID, SelectedActivityCategoryID);
                }
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<ActivityResponse>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        var tempList = new ObservableCollection<ActivityListModel>();
                        if (serviceResultBody.Data != null)
                        {
                            tempList = new ObservableCollection<ActivityListModel>(serviceResultBody.Data);
                        }
                        tempList.All((arg) =>
                        {
                            if (!string.IsNullOrEmpty(arg.proBackgroundImage))
                            {
                                string imgStr = arg.proBackgroundImage;
                                List<string> nimgUrlList = new List<string>();
                                if (!string.IsNullOrEmpty(imgStr))
                                {
                                    if (imgStr.Contains(","))
                                    {
                                        nimgUrlList = imgStr.Split(',').ToList();
                                    }
                                    else
                                    {
                                        nimgUrlList.Add(imgStr);
                                    }
                                }
                                if (nimgUrlList != null && nimgUrlList.Count > 0)
                                {
                                    arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                    nimgUrlList.All((Aarg) =>
                                    {
                                        string imageUri = ConfigService.BaseActivityImageURL + Aarg.Trim();
                                        arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = ImageSource.FromUri(new Uri(imageUri)), ActivityImageUrl = imageUri }); ;
                                        return true;
                                    });
                                }
                            }
                            else
                            {
                                arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = "noimage" });
                            }
                            return true;
                        });
                        Activitylist = new ObservableCollection<ActivityListModel>(tempList);
                        if (serviceResultBody.Data1 != null && serviceResultBody.Data1.Count > 0)
                        {
                            string imgStr = serviceResultBody.Data1[0].proBackgroundImage;
                            List<string> lstImageName = new List<string>();
                            if (!string.IsNullOrEmpty(imgStr))
                            {
                                if (imgStr.Contains(","))
                                {
                                    lstImageName = imgStr.Trim().Split(',').ToList();
                                }
                                else
                                {
                                    lstImageName.Add(imgStr.Trim());
                                }
                            }
                            SessionService.CreateActivityOurImages = lstImageName;
                        }
                        //if (!string.IsNullOrEmpty(categoryId) && tempList == null || tempList.Count < 1)
                        //{
                        //    //await DisplayAlertAsync(AppResources.CategoryDataAlertText);
                        //}
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusyInActivityBinding = false;
            }
        }
        async Task GetGroupMemberListCount()
        {
            try
            {
                BaseResponse<string> serviceResult = null;
                serviceResult = await ActivityService.GetGroupMemberListCount(SettingsService.LoggedInUserData.coUniqueID);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsLikeRespnce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag && serviceResultBody.Data != null)
                    {
                        SessionService.GroupMemberCount = serviceResultBody.Data;
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
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                LayoutService.ConvertThemeAsPerSettings();
                if (SessionService.isEditingNews)
                {
                    SessionService.isEditingNews = false;
                    SessionService.NewsPostInputData = new NewsDetailModel_Input();
                    SessionService.NewsPostAttachmentFileName = string.Empty;
                    SessionService.NewsPostAttachmentFilePath = string.Empty;
                    SessionService.NewsPostImageFiles = new List<string>();
                }
                if (!string.IsNullOrEmpty(SessionService.DeletedNewsPost))
                {
                    int NewsId = Convert.ToInt32(SessionService.DeletedNewsPost);
                    if (NewsId > 0)
                    {
                        var newsItem = NewsList.Where(x => x.news.id == NewsId).FirstOrDefault();
                        NewsList.Remove(newsItem);
                        SessionService.DeletedNewsPost = string.Empty;
                    }
                }
                if (SessionService.IsShowActivitiesIntial)
                {
                    SessionService.IsShowActivitiesIntial = false;
                    MessagingCenter.Send<object>(this, "GetActivityTabSelected");
                    DashboardFooterNavigationCommand.Execute(TextResources.ActivityTabText);
                }
                else if (_isRefreshingNewsFirstTime || SessionService.IsNeedToRefreshNews)
                {
                    if (_isRefreshingNewsFirstTime) { _isRefreshingNewsFirstTime = false; };
                    if (SessionService.IsNeedToRefreshNews) { SessionService.IsNeedToRefreshNews = false; };
                    UserProfileImageHeader = ImageSource.FromUri(new Uri(ConfigService.BaseProfileImageURL + SettingsService.UserProfile));
                    await GetNewsListDetails_New();
                }
                if (SessionService.LikeNewsCount != null && SessionService.LikeNewsID != null)
                {
                    //int LikeNewsCount = Convert.ToInt32(SessionService.LikeNewsCount);
                    var newsItem = NewsList.Where(x => x.news.id == SessionService.LikeNewsID).FirstOrDefault();
                    newsItem.LikeCount = SessionService.LikeNewsCount.Value;
                    SessionService.LikeNewsID = null;
                    SessionService.LikeNewsCount = null;
                }
                await GetGroupMemberListCount();
                //await GetActivityList();
                IsFromMyActivity = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class ActivityItems : BindableBase
    {
        public string categoryId { get; set; }
        public string title { get; set; }

        private bool _UnderlineIsVisible;
        public bool UnderlineIsVisible
        {
            get { return _UnderlineIsVisible; }
            set { SetProperty(ref _UnderlineIsVisible, value); }
        }
    }

    public class ActivityModel
    {
        public ImageSource ActivityImage { get; set; }
    }
}
