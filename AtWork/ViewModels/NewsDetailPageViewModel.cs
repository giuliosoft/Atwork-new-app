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
        #region Constructor
        public NewsDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NewsDetailBack = "Back";
            DetailHeaderOptionIsVisible = true;

            HeaderDetailsTitle = AppResources.PostText;
        }
        #endregion

        #region Private Properties
        bool _isPostLiked = false;
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
        private ImageSource _PublishImage = "earth";
        public ImageSource PublishImage
        {
            get { return _PublishImage; }
            set { SetProperty(ref _PublishImage, value); }
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
        NewsDetailModel NewsDetailModel { get; set; }
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
                
                var serviceResult = await NewsService.NewsDetail("/334");
                var serviceResultBody = JsonConvert.DeserializeObject<NewsResponce>(serviceResult.Body);

                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        NewsDetailModel = serviceResultBody.Data;
                        NewsTitle = serviceResultBody.Data.newsTitle;
                        NewsDescription = serviceResultBody.Data.newsContent;
                        if (serviceResultBody.Data.newsPrivacy?.ToLower() == "everyone")
                        {
                            PublishImage = "earth";
                        }
                        else
                        {
                            PublishImage = "ActivityPeopleIcon";
                        }
                        //await _navigationService.NavigateAsync(nameof(NewsPage));

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
