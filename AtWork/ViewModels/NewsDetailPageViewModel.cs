using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class NewsDetailPageViewModel : ViewModelBase
    {
        #region Constructor
        public NewsDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NewsDetailBack = "Back";
        }
        #endregion

        #region Private Properties
        bool _isPostLiked = false;
        #endregion

        #region Public Properties
        private string _ProductDetail = string.Empty;
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
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

        private Color _LikeCountTextColor = (Color)App.Current.Resources["BlackColor"];
        public Color LikeCountTextColor
        {
            get { return _LikeCountTextColor; }
            set { SetProperty(ref _LikeCountTextColor, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand LikeNewsPostCommand { get { return new DelegateCommand(async () => await LikeNewsPost()); } }
        public DelegateCommand OnOpenSwipeViewClicked { get { return new DelegateCommand(async () => await OnOpenSwipeView()); } }
        public DelegateCommand OnCloseSwipeViewClicked { get { return new DelegateCommand(async () => await OnCloseSwipeView()); } }
        public DelegateCommand DeleteCommentCommand { get { return new DelegateCommand(async () => await DeleteComment()); } }
        public DelegateCommand EditCommentCommand { get { return new DelegateCommand(async () => await EditComment()); } }
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
        async Task EditComment()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task DeleteComment()
        {
            try
            {
                var result = await App.Current.MainPage.DisplayAlert(AppResources.Delete, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                if (result)
                {
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
        }
    }
}
