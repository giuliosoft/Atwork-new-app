using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        #region Constructor
        public NewsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextOptionText = "+";
            NextCustomLabelIsVisible = false;
            //NewsGreenbg = (Color)App.Current.Resources["AccentColor"];
            //ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];
        }
        #endregion

        #region Private Properties

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

        private ObservableCollection<NewsModel> _NewsList = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> NewsList
        {
            get { return _NewsList; }
            set { SetProperty(ref _NewsList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(() => GoForLogin()); } }
        public DelegateCommand<NewsModel> NewsPostSelectedCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await GotoNewsPostDetailPage(obj)); } }
        public DelegateCommand<NewsModel> EditNewsPostCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await EditNewsPost(obj)); } }
        #endregion

        #region private methods
        void GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task GotoNewsPostDetailPage(NewsModel selectedNewsPost)
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(NewsDetailPage), null);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var tempCList = new List<CarouselModel>();
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            NewsImageCarouselList = new ObservableCollection<CarouselModel>(tempCList);

            var tempNews = new ObservableCollection<NewsModel>();
            for (var i = 0; i < 3; i++)
            {
                tempNews.Add(new NewsModel() { NewsImageCarouselList = tempCList });
            }
            NewsList = tempNews;
        }
    }

    public class CarouselModel
    {
        public ImageSource NewsImage { get; set; }
    }

    public class NewsModel
    {
        public List<CarouselModel> NewsImageCarouselList { get; set; }
    }
}
