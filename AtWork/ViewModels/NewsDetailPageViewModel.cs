using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class NewsDetailPageViewModel : ViewModelBase
    {
        int SelectedNewsId;
        #region Constructor
        public NewsDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NewsDetailBack = AppResources.BackButtonText;
            DetailHeaderOptionIsVisible = true;

            HeaderDetailsTitle = AppResources.PostText;
        }
        #endregion

        #region Private Properties
        bool _isPostLiked = false;
        bool _AttachmentIsVisible = false;
        private string _NewsAttachmentTitle = string.Empty;
        private string _NewsLikeCount = string.Empty;
        private string _NewsUserTime = string.Empty;
        private string _NewsUserName = string.Empty;
        private string _commentText = string.Empty;
        #endregion

        #region Public Properties
        private string _NewsTitle = string.Empty;
        public string NewsTitle
        {
            get { return _NewsTitle; }
            set { SetProperty(ref _NewsTitle, value); }
        }
        private string _NewsDescription = string.Empty;
        public string NewsDescription
        {
            get { return _NewsDescription; }
            set { SetProperty(ref _NewsDescription, value); }
        }

        public string NewsAttachmentTitle
        {
            get { return _NewsAttachmentTitle; }
            set { SetProperty(ref _NewsAttachmentTitle, value); }
        }
        public string NewsLikeCount
        {
            get { return _NewsLikeCount; }
            set { SetProperty(ref _NewsLikeCount, value); }
        }
        public string NewsUserTime
        {
            get { return _NewsUserTime; }
            set { SetProperty(ref _NewsUserTime, value); }
        }
        public string NewsUserName
        {
            get { return _NewsUserName; }
            set { SetProperty(ref _NewsUserName, value); }
        }

        public bool AttachmentIsVisible
        {
            get { return _AttachmentIsVisible; }
            set { SetProperty(ref _AttachmentIsVisible, value); }
        }

        private ObservableCollection<CarouselModel> _NewsImageCarouselList = new ObservableCollection<CarouselModel>();
        public ObservableCollection<CarouselModel> NewsImageCarouselList
        {
            get { return _NewsImageCarouselList; }
            set { SetProperty(ref _NewsImageCarouselList, value); }
        }

        private ObservableCollection<CarouselModel> _PostCommentList = new ObservableCollection<CarouselModel>();
        public ObservableCollection<CarouselModel> PostCommentList
        {
            get { return _PostCommentList; }
            set { SetProperty(ref _PostCommentList, value); }
        }

        private bool _SendButtonIsVisible = false;
        public bool SendButtonIsVisible
        {
            get { return _SendButtonIsVisible; }
            set { SetProperty(ref _SendButtonIsVisible, value); }
        }

        private ImageSource _LikeImage = "heartoutline";
        public ImageSource LikeImage
        {
            get { return _LikeImage; }
            set { SetProperty(ref _LikeImage, value); }
        }
        private ImageSource _PublishImageSource= "earth";
        public ImageSource PublishImageSource
        {
            get { return _PublishImageSource; }
            set { SetProperty(ref _PublishImageSource, value); }
        }

        private ImageSource _NewsUserProfileImage;
        public ImageSource NewsUserProfileImage
        {
            get { return _NewsUserProfileImage; }
            set { SetProperty(ref _NewsUserProfileImage, value); }
        }

        private bool _PublishImageIsVisible = false;
        public bool PublishImageIsVisible
        {
            get { return _PublishImageIsVisible; }
            set { SetProperty(ref _PublishImageIsVisible, value); }
        }

        private Color _LikeCountTextColor = (Color)App.Current.Resources["BlackColor"];
        public Color LikeCountTextColor
        {
            get { return _LikeCountTextColor; }
            set { SetProperty(ref _LikeCountTextColor, value); }
        }
        public string CommentText
        {
            get { return _commentText; }
            set { SetProperty(ref _commentText, value); }
        }
        News NewsDetailModel { get; set; }
        #endregion

        #region Commands
        public DelegateCommand LikeNewsPostCommand { get { return new DelegateCommand(async () => await LikeNewsPost()); } }
        public DelegateCommand OnOpenSwipeViewClicked { get { return new DelegateCommand(async () => await OnOpenSwipeView()); } }
        public DelegateCommand OnCloseSwipeViewClicked { get { return new DelegateCommand(async () => await OnCloseSwipeView()); } }
        public DelegateCommand<CarouselModel> DeleteCommentCommand { get { return new DelegateCommand<CarouselModel>(async (obj) => await DeleteComment(obj)); } }
        public DelegateCommand<CarouselModel> EditCommentCommand { get { return new DelegateCommand<CarouselModel>(async (obj) => await EditComment(obj)); } }
        public DelegateCommand SendCommentCommand { get { return new DelegateCommand(async () => await AddComment()); } }
        #endregion

        #region private methods
        async Task LikeNewsPost()
        {
            try
            {
                if (!_isPostLiked)
                {
                    _isPostLiked = true;
                    LikeImage = "heartoutline";
                    LikeCountTextColor = (Color)App.Current.Resources["DarkBrownColor"];
                }
                else
                {
                    _isPostLiked = false;
                    LikeImage = "heartfill";
                    LikeCountTextColor = (Color)App.Current.Resources["WhiteColor"];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task OnOpenSwipeView()
        {
            try
            {
                //swipeView.Open(OpenSwipeItem.LeftItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task EditComment(CarouselModel comment)
        {
            try
            {
                return;
                NewsComment newsComment = new NewsComment
                {
                    comDate = DateTime.Now,
                    comContent = CommentText,
                    coUniqueID = SettingsService.LoggedInUserData?.coUniqueID,
                    newsUniqueID = NewsDetailModel?.newsUniqueID,
                    comByID = SettingsService.VolunteersUserData?.volUniqueID
                };
                var serviceResult = await NewsService.EditComment(newsComment);
                var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    await DisplayAlertAsync(serviceResultBody.Message);
                    CommentText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task DeleteComment(CarouselModel comment)
        {
            try
            {
                var result = await App.Current.MainPage.DisplayAlert(AppResources.Delete, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                if (result)
                {
                    return;
                    var serviceResult = await NewsService.DeleteComment(string.Empty);
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                    //var serviceResult = await NewsService.AddComment(newsComment);
                    //LanguageService.Init(selectedItem.RadioButtomItem);
                    //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task AddComment()
        {
            try
            {
                NewsComment newsComment = new NewsComment
                {
                    comDate = DateTime.Now,
                    comContent = CommentText,
                    coUniqueID = SettingsService.LoggedInUserData?.coUniqueID,
                    newsUniqueID = NewsDetailModel?.newsUniqueID,
                    comByID = SettingsService.VolunteersUserData?.volUniqueID
                };
                var serviceResult = await NewsService.AddComment(newsComment);
                var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    await DisplayAlertAsync(serviceResultBody.Message);
                    CommentText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                await ClosePopup();
            }
        }
        async Task OnCloseSwipeView()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task LoadNewsDetails()
        {
            try
            {
                await ShowLoader();

                var serviceResult = await NewsService.NewsDetail("/" +SelectedNewsId.ToString());
                var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);

                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        NewsDetailModel = serviceResultBody?.Data?.News;

                        if (serviceResultBody.Data.Volunteers != null)
                        {
                            NewsUserProfileImage = ImageSource.FromUri(new Uri(string.Format("http://app.atwork.ai/{0}", serviceResultBody.Data.Volunteers.volPicture)));
                            NewsUserName = serviceResultBody.Data.Volunteers.volFirstName + " " + serviceResultBody.Data.Volunteers.volLastName;

                            NewsUserTime = serviceResultBody.Data.Day;
                            NewsLikeCount = serviceResultBody.Data.Comments_Likes.ToString();

                            _isPostLiked = false; // serviceResultBody.Data.NewsLiked;
                            if (!_isPostLiked)
                            {
                                _isPostLiked = true;
                                LikeImage = "heartoutline";
                                LikeCountTextColor = (Color)App.Current.Resources["DarkBrownColor"];
                            }
                            else
                            {
                                _isPostLiked = false;
                                LikeImage = "heartfill";
                                LikeCountTextColor = (Color)App.Current.Resources["WhiteColor"];
                            }
                            if (serviceResultBody.Data.News != null)
                            {

                                NewsTitle = serviceResultBody.Data.News.newsTitle;
                                NewsDescription = serviceResultBody.Data.News.newsContent;
                                var NewsPrivicy = serviceResultBody?.Data?.News?.newsPrivacy;
                                if (NewsPrivicy != null && NewsPrivicy.ToLower() == "everyone")
                                {
                                    PublishImageSource = "earth";
                                }
                                else if (NewsPrivicy != null && NewsPrivicy.ToLower() == "group")
                                {
                                    PublishImageSource = "ActivityPeopleIcon";
                                }
                                else
                                {
                                    PublishImageIsVisible = false;
                                }

                                if (serviceResultBody.Data.News.newsFile != null && serviceResultBody.Data.News.newsFile != string.Empty) //serviceResultBody.Data.
                                {
                                    AttachmentIsVisible = true;
                                    NewsAttachmentTitle = serviceResultBody.Data.News.newsFile;
                                }
                            }
                        }
                        //var tempCList = new ObservableCollection<CarouselModel>();
                        //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
                        //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
                        //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
                        //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
                        //NewsImageCarouselList = tempCList;
                    }
                    else
                    {
                        await DisplayAlertAsync(TextResources.InvalidUserNameorPaddword);
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await ClosePopup();
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

            SelectedNewsId = parameters.GetValue<int>("SelectedNewsID");

            var tempCList = new ObservableCollection<CarouselModel>();
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            NewsImageCarouselList = tempCList;

            var tempCmtList = new ObservableCollection<CarouselModel>();
            tempCmtList.Add(new CarouselModel() { NewsImage = "bg" });
            tempCmtList.Add(new CarouselModel() { NewsImage = "bg" });
            PostCommentList = tempCmtList;

            LoadNewsDetails();
        }
    }
}
