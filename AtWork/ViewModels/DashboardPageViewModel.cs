using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

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

            NewsGreenbg = (Color)App.Current.Resources["DarkGreenColor"];
            ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];

            FooterNavigationCommand = DashboardFooterNavigationCommand;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _NewsViewIsVisible = true;
        private bool _ActivityViewIsVisible = false;
        private ObservableCollection<ActivityItems> _Activitylist = new ObservableCollection<ActivityItems>();
        private ObservableCollection<CarouselModel> _NewsImageCarouselList = new ObservableCollection<CarouselModel>();
        private ObservableCollection<NewsModel> _NewsList = new ObservableCollection<NewsModel>();
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

        public ObservableCollection<CarouselModel> NewsImageCarouselList
        {
            get { return _NewsImageCarouselList; }
            set { SetProperty(ref _NewsImageCarouselList, value); }
        }

        public ObservableCollection<NewsModel> NewsList
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
        public DelegateCommand<NewsModel> NewsPostSelectedCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await GotoNewsPostDetailPage(obj)); } }
        public DelegateCommand<NewsModel> EditNewsPostCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await EditNewsPost(obj)); } }
        public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        public DelegateCommand<NewsModel> NewsShowOptionCommand { get { return new DelegateCommand<NewsModel>(async (obj) => await NewsShowOption(obj)); } }
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
                await _navigationService.NavigateAsync(nameof(AddNewsPostPage));
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
                    NewsGreenbg = (Color)App.Current.Resources["DarkGreenColor"];
                    ActivitiesGreenbg = (Color)App.Current.Resources["LightBrownColor"];
                }
                else if (selectedTab == TextResources.ActivityTabText)
                {
                    NewsViewIsVisible = false;
                    ActivityViewIsVisible = true;

                    NextCustomLabelIsVisible = true;
                    NextOptionText = AppResources.MyActivitiesHeaderText;
                    NewsGreenbg = (Color)App.Current.Resources["LightBrownColor"];
                    ActivitiesGreenbg = (Color)App.Current.Resources["DarkGreenColor"];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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

        async Task NewsShowOption(NewsModel selectedNewsPost)
        {
            try
            {
                await NewsOption();
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

            var tempCList = new List<CarouselModel>();
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            tempCList.Add(new CarouselModel() { NewsImage = "bg" });
            //NewsImageCarouselList = tempCList;

            var tempNews = new ObservableCollection<NewsModel>();
            for (var i = 0; i < 3; i++)
            {
                tempNews.Add(new NewsModel() { NewsImageCarouselList = tempCList });
            }
            NewsList = tempNews;
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
