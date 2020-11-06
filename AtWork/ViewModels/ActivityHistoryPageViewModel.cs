using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

namespace AtWork.ViewModels
{
    public class ActivityHistoryPageViewModel : ViewModelBase
    {
        #region Constructor
        public ActivityHistoryPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.Activityhistory;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ObservableCollection<ActivityItems> _Activitycollectionlist = new ObservableCollection<ActivityItems>();
        private string SelectedActivityCategoryID = string.Empty;
        Volunteers _volunteers = SessionService.volunteers;
        private int _remainingItemsThreshold = 0;
        private int _remainingActivityItemsThreshold = 0;
        bool _isBusy = false;
        Color _ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
        Color _HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
        Color _ActivityTextColor = Color.White;
        Color _HoursTextColor = (Color)App.Current.Resources["AccentColor"];
        private int PageNo = 1;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public ObservableCollection<ActivityItems> Activitycollectionlist
        {
            get { return _Activitycollectionlist; }
            set { SetProperty(ref _Activitycollectionlist, value); }
        }
        public Volunteers volunteers
        {
            get { return _volunteers; }
            set { SetProperty(ref _volunteers, value); }
        }
        private ObservableCollection<FeedBackUIModel> _interestList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> interestList
        {
            get { return _interestList; }
            set { SetProperty(ref _interestList, value); }
        }
        private ObservableCollection<ActivityHistory> _ActivityHistorylist = new ObservableCollection<ActivityHistory>();
        public ObservableCollection<ActivityHistory> ActivityHistorylist
        {
            get { return _ActivityHistorylist; }
            set { SetProperty(ref _ActivityHistorylist, value); }
        }
        public int RemainingItemsThreshold
        {
            get { return _remainingItemsThreshold; }
            set { SetProperty(ref _remainingItemsThreshold, value); }
        }
        public int RemainingActivityItemsThreshold
        {
            get { return _remainingActivityItemsThreshold; }
            set { SetProperty(ref _remainingActivityItemsThreshold, value); }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        public Color ActivityBGColor
        {
            get { return _ActivityBGColor; }
            set { SetProperty(ref _ActivityBGColor, value); }
        }
        public Color HoursBGColor
        {
            get { return _HoursBGColor; }
            set { SetProperty(ref _HoursBGColor, value); }
        }
        public Color ActivityTextColor
        {
            get { return _ActivityTextColor; }
            set { SetProperty(ref _ActivityTextColor, value); }
        }
        public Color HoursTextColor
        {
            get { return _HoursTextColor; }
            set { SetProperty(ref _HoursTextColor, value); }
        }

        private ObservableCollection<HoursActivityCount> _UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
        public ObservableCollection<HoursActivityCount> UserActivityHoursList
        {
            get { return _UserActivityHoursList; }
            set { SetProperty(ref _UserActivityHoursList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<ActivityItems> ActivityCategorySelectedCommand { get { return new DelegateCommand<ActivityItems>(async (obj) => await ActivityCategorySelected(obj)); } }
        public DelegateCommand NewsLoadMoreItemsCommand { get { return new DelegateCommand(async () => await NewsLoadMoreItems()); } }
        public DelegateCommand ShowActivityDetailCommand { get { return new DelegateCommand(async () => await ShowActivityDetail()); } }
        public DelegateCommand ShowHoursDetailCommand { get { return new DelegateCommand(async () => await ShowHoursDetail()); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task ActivityCategorySelected(ActivityItems selectedCategory)
        {
            try
            {
                SelectedActivityCategoryID = selectedCategory.categoryId;
                Activitycollectionlist.All((categories)  =>
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
                //await GetActivityList(SelectedActivityCategoryID);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task LoadActivityDetail()
        {
            try
            {
                //if (ActivityHistorylist.Count < 10)
                //    RemainingItemsThreshold = -1;
                //else
                //    RemainingItemsThreshold = 0;


                //string[] AllMonthName = DateTimeFormatInfo.CurrentInfo.MonthNames;
                var names = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(DateTime.Today.Month).Reverse().ToList();
                //var names = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }.Take(DateTime.Today.Month).ToList();
                Activitycollectionlist.Add(new ActivityItems() { title = AppResources.AllTime, UnderlineIsVisible = true });
                foreach (var item in names)
                {
                    Activitycollectionlist.Add(new ActivityItems() { title = item, UnderlineIsVisible = false, categoryId = item });
                }
                Activitycollectionlist.Add(new ActivityItems() { title = (DateTime.Now.Year - 1).ToString(), UnderlineIsVisible = false, categoryId = (DateTime.Now.Year - 1).ToString() });
                Activitycollectionlist.Add(new ActivityItems() { title = (DateTime.Now.Year - 2).ToString(), UnderlineIsVisible = false, categoryId = (DateTime.Now.Year - 2).ToString() });
                

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task NewsLoadMoreItems()
        {
            try
            {
                //if (IsBusy)
                //{
                //    return;
                //}
                //PageNo++;
                //await LoadActivityDetail();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
           
        }
        async Task ShowActivityDetail()
        {
            try
            {
                UserActivityHoursList.Clear();
                //ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
                //HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
                ObservableCollection<HoursActivityCount> temp = new ObservableCollection<HoursActivityCount>();
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
                UserActivityHoursList = temp;
                ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
                HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];

                ActivityTextColor = Color.White;
                HoursTextColor = (Color)App.Current.Resources["AccentColor"];
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task ShowHoursDetail()
        {
            try
            {
                UserActivityHoursList.Clear();
                ObservableCollection<HoursActivityCount> temp = new ObservableCollection<HoursActivityCount>();
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
                temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
                UserActivityHoursList = temp;
                ActivityBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
                HoursBGColor = (Color)App.Current.Resources["AccentColor"];
                HoursTextColor = Color.White;
                ActivityTextColor = (Color)App.Current.Resources["AccentColor"];
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

            ObservableCollection<HoursActivityCount> temp = new ObservableCollection<HoursActivityCount>();
            temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
            temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
            temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
            temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
            temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
            UserActivityHoursList = temp;

            for (int i = 0; i < 5; i++)
            {
                interestList.Add(new FeedBackUIModel() { Title = i.ToString() });
            }
            for (int i = 0; i < 30; i++)
            {
                ActivityHistorylist.Add(new ActivityHistory() { ActivityTitle = "Activity title goes hear " + i.ToString(), ActivityDescription = "Description", ActivityDateTime = "10 Sep" });
            }
            await LoadActivityDetail();
        }
    }
    public class ActivityHistory
    {
        public string ActivityTitle { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityDateTime { get; set; }
    }
}


