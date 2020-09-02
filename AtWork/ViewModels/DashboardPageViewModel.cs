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
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class DashboardPageViewModel : ViewModelBase
    {
        #region Constructor
        public DashboardPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            Activitycollectionlist.Add(new ActivityItems() { title = "All categories" });
            Activitycollectionlist.Add(new ActivityItems() { title = "Corporate volunteering" });
            Activitycollectionlist.Add(new ActivityItems() { title = "Education" });

            Activitylist.Add(new ActivityItems() { title = "All categories" });
            Activitylist.Add(new ActivityItems() { title = "Corporate volunteering" });
            Activitylist.Add(new ActivityItems() { title = "Education" });
            //NewsGreenbg = (Color)App.Current.Resources["AccentColor"];
            //ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];
            FooterNavigationCommand = DashboardFooterNavigationCommand;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _NewsViewIsVisible = true;
        private bool _ActivityViewIsVisible = false;
        private int PageNo = 1;
        private ObservableCollection<ActivityItems> _Activitylist = new ObservableCollection<ActivityItems>();
        private ObservableCollection<CarouselModel> _NewsImageCarouselList = new ObservableCollection<CarouselModel>();
        private ObservableCollection<NewsListData_Model> _NewsList = new ObservableCollection<NewsListData_Model>();
        private ObservableCollection<ActivityItems> _Activitycollectionlist = new ObservableCollection<ActivityItems>();
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
        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        private int _remainingItemsThreshold = 0;
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

        public ObservableCollection<ActivityItems> Activitylist
        {
            get { return _Activitylist; }
            set { SetProperty(ref _Activitylist, value); }
        }

        public ObservableCollection<ActivityItems> Activitycollectionlist
        {
            get { return _Activitycollectionlist; }
            set { SetProperty(ref _Activitycollectionlist, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<string> DashboardFooterNavigationCommand { get { return new DelegateCommand<string>(async (obj) => await DashboardFooterNavigation(obj)); } }
        public DelegateCommand<NewsListData_Model> NewsPostSelectedCommand { get { return new DelegateCommand<NewsListData_Model>(async (obj) => await GotoNewsPostDetailPage(obj)); } }
        public DelegateCommand<NewsModel> EditNewsPostCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await EditNewsPost(obj)); } }
        public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        //public DelegateCommand<News> NewsShowOptionCommand { get { return new DelegateCommand<News>(async (obj) => await NewsShowOption(obj)); } }
        public DelegateCommand<NewsListData_Model> NewsShowOptionCommand { get { return new DelegateCommand<NewsListData_Model>(async (obj) => await NewsShowOption(obj)); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
        public DelegateCommand NewsLoadMoreItemsCommand { get { return new DelegateCommand(async () => await NewsLoadMoreItems()); } }
        public DelegateCommand RefreshCommand { get { return new DelegateCommand(async () => await ExecuteRefreshCommand()); } }
        public DelegateCommand<string> ActivityPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await ActivityPostProceed(obj)); } }
        #endregion

        #region private methods
        async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            PageNo = 1;
            await GetNewsListDetails_New();
            IsRefreshing = false;
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

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                        await ShowLoader();
                        var serviceResult = await NewsService.DeleteNewsPost(selectedNewsPost.news.id);
                        if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                        {
                            NewsList.Remove(selectedNewsPost);
                            //await GetNewsListDetails_New();
                        }
                        await ClosePopup();
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

        async Task GetNewsListDetails_New()
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
                                tempData.news = nArg.news;
                                tempData.Volunteers = nArg.Volunteers;
                                tempData.userName = nArg.Volunteers != null ? nArg.Volunteers.volFirstName + " " + nArg.Volunteers.volLastName : string.Empty;
                                tempData.newsPostUserProfilePic = !string.IsNullOrEmpty(nArg.Volunteers?.volPicture) ? ConfigService.BaseImageURL + nArg.Volunteers?.volPicture : string.Empty;
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
                                            string imageUri = ConfigService.BaseImageURL + arg;
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
                await GetNewsListDetails_New();
                if (SessionService.isEditingNews)
                {
                    SessionService.isEditingNews = false;
                    SessionService.NewsPostInputData = new NewsDetailModel_Input();
                    SessionService.NewsPostAttachmentFileName = string.Empty;
                    SessionService.NewsPostAttachmentFilePath = string.Empty;
                    SessionService.NewsPostImageFiles = new List<string>();
                }
                if (SessionService.DeletedNewsPost != null)
                {
                    int NewsId = Convert.ToInt32(SessionService.DeletedNewsPost);
                    if (NewsId > 0)
                    {
                        var newsItem =  NewsList.Where(x => x.news.id == NewsId).FirstOrDefault();
                        NewsList.Remove(newsItem);
                        SessionService.DeletedNewsPost = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class ActivityItems
    {
        public string title { get; set; }
    }

    public class ActivityModel
    {
        public ImageSource ActivityImage { get; set; }
    }
}
