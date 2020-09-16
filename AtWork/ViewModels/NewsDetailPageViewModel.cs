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
using static AtWork.Models.CommentsModel;
using static AtWork.Models.CommentsModel.NewsComment;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class NewsDetailPageViewModel : ViewModelBase
    {
        int SelectedNewsId;
        int LikeId;
        #region Constructor
        public NewsDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NewsDetailBack = AppResources.BackButtonText;
            NewsOptionCommand = ShowNewsOptionCommand;
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
        private string _sendButtonText = AppResources.SendText;
        private int NewsLikeCountNo = 0;
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

        private ObservableCollection<NewsComment> _PostCommentList = new ObservableCollection<NewsComment>();
        public ObservableCollection<NewsComment> PostCommentList
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
        private ImageSource _PublishImageSource = "earth";
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
        public string SendButtonText
        {
            get { return _sendButtonText; }
            set { SetProperty(ref _sendButtonText, value); }
        }
        News NewsDetailModel { get; set; }
        NewsComment EditDeleteSelectedComment { get; set; }
        #endregion

        #region Commands
        public DelegateCommand LikeNewsPostCommand { get { return new DelegateCommand(async () => await LikeNewsPost()); } }
        public DelegateCommand OnOpenSwipeViewClicked { get { return new DelegateCommand(async () => await OnOpenSwipeView()); } }
        public DelegateCommand OnCloseSwipeViewClicked { get { return new DelegateCommand(async () => await OnCloseSwipeView()); } }
        public DelegateCommand<NewsComment> DeleteCommentCommand { get { return new DelegateCommand<NewsComment>(async (obj) => await DeleteComment(obj)); } }
        public DelegateCommand<NewsComment> EditCommentCommand { get { return new DelegateCommand<NewsComment>(async (obj) => await EditComment(obj)); } }
        public DelegateCommand SendCommentCommand { get { return new DelegateCommand(async () => await AddComment()); } }
        public DelegateCommand ShowNewsOptionCommand { get { return new DelegateCommand(async () => await ShowNewsOption()); } }
        public DelegateCommand<NewsComment> CommentLikeCommand { get { return new DelegateCommand<NewsComment>(async (obj) => await CommentLike(obj)); } }
        //CommentLikeCommand
        #endregion

        #region private methods
        async Task ShowNewsOption()
        {
            try
            {
                NewsOptionPopup newsOptionPopup = new NewsOptionPopup();
                NewsOptionPopupViewModel newsOptionPopupViewModel = new NewsOptionPopupViewModel(_navigationService, _facadeService);
                newsOptionPopupViewModel.EditNewsEvent += async (object sender, object SelectedObj) =>
                {
                    try
                    {
                        await ShowLoader();
                        if (NewsDetailModel != null)
                        {
                            SessionService.isEditingNews = true;
                            SessionService.NewsPostInputData.newsTitle = NewsDetailModel.newsTitle;
                            SessionService.NewsPostInputData.newsContent = NewsDetailModel.newsContent;
                            SessionService.NewsPostInputData.newsUniqueID = NewsDetailModel.newsUniqueID;
                            SessionService.NewsPostInputData.volUniqueID = NewsDetailModel.volUniqueID;
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
                        var result = await App.Current.MainPage.DisplayAlert(AppResources.DeletePostAlert, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                        if (result)
                        {
                            await ShowLoader();
                            var serviceResult = await NewsService.DeleteNewsPost(NewsDetailModel.id);
                            if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                            {
                                SessionService.DeletedNewsPost = NewsDetailModel.id.ToString();
                                await BackClick();
                            }
                            await ClosePopup();
                        }
                    }
                    catch (Exception ex)
                    {
                        await ClosePopup();
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

        async Task LikeNewsPost()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                NewsLikes newsLikes = new NewsLikes();
                newsLikes.likeByID = SettingsService.VolunteersUserData.volUniqueID.ToString();
                newsLikes.newsId = SelectedNewsId;
                newsLikes.likeDate = DateTime.Now;
                if (!_isPostLiked)
                {
                    LikeImage = "heartoutline";
                    LikeCountTextColor = (Color)App.Current.Resources["DarkBrownColor"];
                    newsLikes.Id = LikeId;
                    await ShowLoader();
                    var result = await NewsService.UnLikeNewsFeed(newsLikes);
                    var serviceResultBody = JsonConvert.DeserializeObject<NewUnLikeRespnce>(result?.Body);
                    await ClosePopup();
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        _isPostLiked = true;
                        //LikeId = serviceResultBody.Data;
                        NewsLikeCount = serviceResultBody.Data1.ToString();
                        SessionService.LikeNewsID = SelectedNewsId;
                        SessionService.LikeNewsCount = Convert.ToInt32(NewsLikeCount);
                    }
                }
                else
                {
                    LikeImage = "heartfill";
                    LikeCountTextColor = (Color)App.Current.Resources["WhiteColor"];
                    await ShowLoader();
                    var result = await NewsService.LikeNewsFeed(newsLikes);
                    await ClosePopup();
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsLikeRespnce>(result?.Body);
                    if (serviceResultBody != null)
                    {
                        _isPostLiked = false;
                        LikeId = serviceResultBody.Data;
                        NewsLikeCount = serviceResultBody.Data1.ToString();
                        SessionService.LikeNewsID = SelectedNewsId;
                        SessionService.LikeNewsCount = Convert.ToInt32(NewsLikeCount);
                    }
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
        async Task EditComment(NewsComment comment)
        {
            try
            {
                EditDeleteSelectedComment = comment;
                SendButtonText = AppResources.Update;
                CommentText = comment.comContent;
                SendButtonIsVisible = true;
                MessagingCenter.Send<NewsDetailPage>(new NewsDetailPage(), "CommentEdit");
                //NewsComment newsComment = new NewsComment
                //{
                //    comDate = DateTime.Now,
                //    comContent = CommentText,
                //    coUniqueID = SettingsService.LoggedInUserData?.coUniqueID,
                //    newsUniqueID = NewsDetailModel?.newsUniqueID,
                //    comByID = SettingsService.VolunteersUserData?.volUniqueID
                //};
                //var serviceResult = await NewsService.EditComment(newsComment);
                //var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                //if (serviceResultBody != null && serviceResultBody.Flag)
                //{
                //    await DisplayAlertAsync(serviceResultBody.Message);
                //    CommentText = string.Empty;
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task DeleteComment(NewsComment comment)
        {
            try
            {
                var result = await App.Current.MainPage.DisplayAlert(AppResources.Delete, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                if (result)
                {
                    await ShowLoader();
                    var serviceResult = await NewsService.DeleteComment(comment.Id);
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        //await DisplayAlertAsync(serviceResultBody.Message);
                        await GetComments();
                    }
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
            finally
            {
                await ClosePopup();
            }
        }
        async Task AddComment()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                NewsComment newsComment = new NewsComment
                {
                    comContent = CommentText,
                    coUniqueID = SettingsService.LoggedInUserData?.coUniqueID,
                    newsUniqueID = NewsDetailModel?.newsUniqueID,
                    comByID = SettingsService.VolunteersUserData?.volUniqueID,
                    comDate = DateTime.Now
                };
                if (SendButtonText == AppResources.SendText)
                {

                    var serviceResult = await NewsService.AddComment(newsComment);
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        //await DisplayAlertAsync(serviceResultBody.Message);
                        CommentText = string.Empty;
                    }

                    // PostCommentList.Add();
                }
                else if (SendButtonText == AppResources.Update)
                {
                    newsComment.Id = EditDeleteSelectedComment.Id;
                    var serviceResult = await NewsService.EditComment(newsComment);
                    var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        //await DisplayAlertAsync(serviceResultBody.Message);
                        CommentText = string.Empty;
                    }
                    SendButtonText = AppResources.SendText;
                }
                await GetComments();
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
        async Task CommentLike(NewsComment comment)
        {
            try
            {
                News_Comments_Likes news_Comments_Likes = new News_Comments_Likes
                {
                    newsCommentId = comment.Id,
                    likeByID = SettingsService.VolunteersUserData?.volUniqueID,
                    likeDate = DateTime.Now
                };
                await ShowLoader();
                if (!comment.LikeByLoginUser)
                {
                    var serviceResult = await NewsService.AddNewsCommentLike(news_Comments_Likes);
                    var serviceResultBody = JsonConvert.DeserializeObject<CommentLikeResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        comment.LikeId = serviceResultBody.Data;
                        comment.LikeCount++;
                    }
                }
                else
                {
                    news_Comments_Likes.Id = comment.LikeId;
                    NewsService.DeleteNewsCommentLike(news_Comments_Likes);
                    comment.LikeCount--;
                }
                comment.LikeByLoginUser = !comment.LikeByLoginUser;
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
        async Task LoadNewsDetails()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();

                var serviceResult = await NewsService.NewsDetail(SelectedNewsId.ToString(),SettingsService.VolunteersUserData?.volUniqueID);
                var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);

                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        NewsDetailModel = serviceResultBody?.Data?.News;

                        if (serviceResultBody.Data.Volunteers != null)
                        {
                            DetailHeaderOptionIsVisible = serviceResultBody.Data.Volunteers?.volUniqueID == SettingsService.VolunteersUserData?.volUniqueID;
                            NewsUserProfileImage = !string.IsNullOrEmpty(serviceResultBody.Data.Volunteers?.volPicture) ? ImageSource.FromUri(new Uri(ConfigService.BaseProfileImageURL + serviceResultBody.Data.Volunteers?.volPicture)) : string.Empty;
                            NewsUserName = serviceResultBody.Data.Volunteers.volFirstName + " " + serviceResultBody.Data.Volunteers.volLastName;
                        }
                        NewsUserTime = serviceResultBody.Data.Day;
                        NewsLikeCount = serviceResultBody.Data.LikeCount.ToString();
                        NewsLikeCountNo = serviceResultBody.Data.LikeCount;
                        LikeId = serviceResultBody.Data.LikeId;

                        _isPostLiked = serviceResultBody.Data.LikeByLoginUser;
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
                            if (NewsPrivicy != null && NewsPrivicy.ToLower() == "everyone") // everyone Or mygroup
                            {
                                PublishImageSource = "earth";
                            }
                            else
                            {
                                PublishImageSource = "ActivityPeopleIcon";
                            }

                            var tempCList = new ObservableCollection<CarouselModel>();
                            var newsImage = serviceResultBody?.Data?.News?.newsImage;
                            if (newsImage != null && newsImage != string.Empty)
                            {
                                var splittedList = serviceResultBody?.Data?.News?.newsImage.Split(',').ToList();
                                if (splittedList != null && splittedList.Count > 0)
                                {
                                    splittedList.All((x) =>
                                    {

                                        tempCList.Add(new CarouselModel() { NewsImage = ConfigService.BaseNewsImageURL + x });
                                        return true;

                                    });
                                }
                            }
                            else
                            {
                                tempCList.Add(new CarouselModel() { NewsImage = "noimage" });
                            }
                            NewsImageCarouselList = tempCList;

                            if (serviceResultBody.Data.News.newsFile != null && serviceResultBody.Data.News.newsFile != string.Empty) //serviceResultBody.Data.
                            {
                                AttachmentIsVisible = true;
                                NewsAttachmentTitle = serviceResultBody.Data.News.newsFile;
                            }
                        }

                        await GetComments();
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
        async Task GetComments()
        {
            try
            {
                var serviceResultComment = await NewsService.GetNewsCommentListByID(NewsDetailModel?.newsUniqueID, SettingsService.VolunteersUserData?.volUniqueID);
                var serviceResultBodyComment = JsonConvert.DeserializeObject<NewsCommentResponce>(serviceResultComment.Body);
                if (serviceResultBodyComment != null && serviceResultBodyComment.Flag)
                {

                    var tempCmtList = new ObservableCollection<NewsComment>();
                    if (serviceResultBodyComment.Data != null && serviceResultBodyComment.Data.Count > 0)
                    {
                        serviceResultBodyComment.Data.All((x) =>
                        {
                            tempCmtList.Add(x);
                            return true;
                        });
                    }
                    //tempCmtList.Add(new NewsComment() { comByID = SettingsService.VolunteersUserData?.volUniqueID, });
                    //tempCmtList.Add(new NewsComment() { comByID = SettingsService.VolunteersUserData?.volUniqueID });
                    //tempCmtList.Add(new NewsComment() { comByID = null });
                    PostCommentList = tempCmtList;
                }
            }
            catch (Exception ex)
            {

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

            //var tempCList = new ObservableCollection<CarouselModel>();
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //NewsImageCarouselList = tempCList;

            //var tempCmtList = new ObservableCollection<NewsComment>();
            //tempCmtList.Add(new NewsComment() { comByID = SettingsService.VolunteersUserData?.volUniqueID, });
            //tempCmtList.Add(new NewsComment() { comByID = SettingsService.VolunteersUserData?.volUniqueID });
            //tempCmtList.Add(new NewsComment() { comByID = null });
            //PostCommentList = tempCmtList;

            await LoadNewsDetails();
        }
    }
}
