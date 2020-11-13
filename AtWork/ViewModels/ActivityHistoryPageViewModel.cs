using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AtWork.Models.ActivityModel;
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
        private string _ActivityDate = string.Empty;
        private string _ActivityCount = string.Empty;
        private string _ActivityHours = string.Empty;
        private ObservableCollection<ActivityMonthYear> _Activitycollectionlist = new ObservableCollection<ActivityMonthYear>();
        private string SelectedActivityCategoryID = string.Empty;
        private ActivitiesHistory _ActivityHistoryDetails = new ActivitiesHistory();
        ObservableCollection<HoursActivityCount> UserActivityList = new ObservableCollection<HoursActivityCount>();
        ObservableCollection<HoursActivityCount> UserHoursList = new ObservableCollection<HoursActivityCount>();
        //Volunteers _volunteers = SessionService.volunteers;
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
        public string ActivityDate
        {
            get { return _ActivityDate; }
            set { SetProperty(ref _ActivityDate, value); }
        }
        public string ActivityCount
        {
            get { return _ActivityCount; }
            set { SetProperty(ref _ActivityCount, value); }
        }
        public string ActivityHours
        {
            get { return _ActivityHours; }
            set { SetProperty(ref _ActivityHours, value); }
        }
        public ObservableCollection<ActivityMonthYear> Activitycollectionlist
        {
            get { return _Activitycollectionlist; }
            set { SetProperty(ref _Activitycollectionlist, value); }
        }
        //public Volunteers volunteers
        //{
        //    get { return _volunteers; }
        //    set { SetProperty(ref _volunteers, value); }
        //}
        //private ObservableCollection<FeedBackUIModel> _interestList = new ObservableCollection<FeedBackUIModel>();
        //public ObservableCollection<FeedBackUIModel> interestList
        //{
        //    get { return _interestList; }
        //    set { SetProperty(ref _interestList, value); }
        //}
        private ObservableCollection<ActivitiesDisplay> _ActivityHistorylist = new ObservableCollection<ActivitiesDisplay>();
        public ObservableCollection<ActivitiesDisplay> ActivityHistorylist
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

        public ActivitiesHistory ActivityHistoryDetails
        {
            get { return _ActivityHistoryDetails; }
            set { SetProperty(ref _ActivityHistoryDetails, value); }
        }
        #endregion

        #region Commands
        //public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<ActivityMonthYear> ActivityCategorySelectedCommand { get { return new DelegateCommand<ActivityMonthYear>(async (obj) => await ActivityCategorySelected(obj)); } }
        public DelegateCommand NewsLoadMoreItemsCommand { get { return new DelegateCommand(async () => await NewsLoadMoreItems()); } }
        public DelegateCommand ShowActivityDetailCommand { get { return new DelegateCommand(async () => await ShowActivityDetail()); } }
        public DelegateCommand ShowHoursDetailCommand { get { return new DelegateCommand(async () => await ShowHoursDetail()); } }
        #endregion

        #region private methods
        //async Task GoForLogin()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.CommanException(ex);
        //    }
        //}
        async Task ShowActivityDetail()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
                    UserActivityHoursList = UserActivityList;
                });

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
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
                    UserActivityHoursList = UserHoursList;
                });

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

        async Task ActivityCategorySelected(ActivityMonthYear selectedCategory)
        {
            try
            {
                PageNo = 1;
                ActivityHistorylist.Clear();

                ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
                HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
                ActivityTextColor = Color.White;
                HoursTextColor = (Color)App.Current.Resources["AccentColor"];

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
                await LoadActivityDetail(selectedCategory.Month , selectedCategory.Year);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task LoadActivityDetail(string month = null,string year = null)
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
                //bool isClearlist = false;
                string url = string.Empty;
               
                url = PageNo.ToString();
                if (!string.IsNullOrEmpty(year))
                {
                    //isClearlist = true;
                    url += string.Format("/{0}", year);
                }
                if (!string.IsNullOrEmpty(month))
                {
                    //isClearlist = true;
                    url += string.Format("/{0}", month);
                }
                //if (isClearlist)
                //{
                //    ActivityHistorylist.Clear();
                //}
                
                await ShowLoader();
                var serviceResult = await ActivityService.GetActivityHistoryDetails(url);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<ActivityHistoryResponse>(serviceResult?.Body);
                    if (serviceResultBody != null)
                    {
                        ActivityHistoryDetails = serviceResultBody.Data1;
                        if (serviceResultBody.Data != null)
                        {
                            if (serviceResultBody.Data.Count < 10)
                                RemainingItemsThreshold = -1;
                            else
                                RemainingItemsThreshold = 0;
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                if (PageNo == 1)
                                {
                                    ActivityHistorylist.Clear();
                                }
                                var tempList = new ObservableCollection<ActivitiesDisplay>(ActivityHistorylist);
                                ObservableCollection<ActivitiesDisplay> myCollection = new ObservableCollection<ActivitiesDisplay>(serviceResultBody.Data as List<ActivitiesDisplay>);
                                myCollection.All((nArg) =>
                                {
                                    tempList.Add(nArg);
                                    return true;
                                });
                                ActivityHistorylist = new ObservableCollection<ActivitiesDisplay>(tempList);
                            });
                        }
                        if (serviceResultBody.Data1 != null)
                        {
                            //ActivityHistoryDetails = serviceResultBody.Data1;
                           
                            ActivityHours = serviceResultBody.Data1.CategorywiseHourCount;
                            UserActivityList = new ObservableCollection<HoursActivityCount>();
                            UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
                            UserHoursList = new ObservableCollection<HoursActivityCount>();
                            if (!string.IsNullOrEmpty(serviceResultBody.Data1?.CategoryActivityCount))
                            {
                                string CategoryActivityAndCount = serviceResultBody.Data1?.CategoryActivityCount;
                                List<string> CategoryActivityAndCountList = new List<string>();
                                if (!string.IsNullOrEmpty(CategoryActivityAndCount))
                                {
                                    if (CategoryActivityAndCount.Contains(","))
                                    {
                                        CategoryActivityAndCountList = CategoryActivityAndCount.Split(',').ToList();
                                    }
                                    else
                                    {
                                        CategoryActivityAndCountList.Add(CategoryActivityAndCount);
                                    }
                                }

                                if (CategoryActivityAndCountList != null && CategoryActivityAndCountList.Count > 0)
                                {
                                    var tempCmtList = new ObservableCollection<HoursActivityCount>();
                                    CategoryActivityAndCountList.All((arg) =>
                                    {
                                        List<string> UserSingleDescriptionList = new List<string>();
                                        if (!string.IsNullOrEmpty(arg))
                                        {
                                            if (arg.Contains(":"))
                                            {
                                                UserSingleDescriptionList = arg.Split(':').ToList();
                                            }
                                            else
                                            {
                                                UserSingleDescriptionList.Add(arg);
                                            }
                                            if (UserSingleDescriptionList.Count == 2)
                                            {
                                                //if (!UserSingleDescriptionList[0].Contains("gettogether"))
                                                    tempCmtList.Add(new HoursActivityCount() { Text = UserSingleDescriptionList[0].ToUpper(), Count = UserSingleDescriptionList[1] });
                                            }
                                        }

                                        return true;
                                    });
                                    UserActivityList = tempCmtList;
                                    UserActivityHoursList = tempCmtList;
                                }
                            }
                            if (!string.IsNullOrEmpty(serviceResultBody.Data1?.CategorywiseHourCount))
                            {
                                string CategoryHoursAndCount = serviceResultBody.Data1?.CategorywiseHourCount;
                                List<string> tempCategoryHoursList = new List<string>();
                                if (!string.IsNullOrEmpty(CategoryHoursAndCount))
                                {
                                    if (CategoryHoursAndCount.Contains(","))
                                    {
                                        tempCategoryHoursList = CategoryHoursAndCount.Split(',').ToList();
                                    }
                                    else
                                    {
                                        tempCategoryHoursList.Add(CategoryHoursAndCount);
                                    }
                                }

                                if (tempCategoryHoursList != null && tempCategoryHoursList.Count > 0)
                                {
                                    var tempCmtList = new ObservableCollection<HoursActivityCount>();
                                    tempCategoryHoursList.All((arg) =>
                                    {
                                        List<string> UserSingleDescriptionList = new List<string>();
                                        if (!string.IsNullOrEmpty(arg))
                                        {
                                            if (arg.Contains(":"))
                                            {
                                                UserSingleDescriptionList = arg.Split(':').ToList();
                                            }
                                            else
                                            {
                                                UserSingleDescriptionList.Add(arg);
                                            }
                                            if (UserSingleDescriptionList.Count == 2)
                                            {
                                                //if (!UserSingleDescriptionList[0].Contains("gettogether"))
                                                    tempCmtList.Add(new HoursActivityCount() { Text = UserSingleDescriptionList[0].ToUpper(), Count = UserSingleDescriptionList[1] });
                                            }
                                        }

                                        return true;
                                    });
                                    UserHoursList = tempCmtList;
                                }
                            }

                            //ActivityCount = UserActivityList.Count.ToString();
                            //ActivityHours = UserHoursList.Count.ToString();


                            //if (string.IsNullOrEmpty(serviceResultBody?.Data1?.CategorywiseHourCount) || UserActivityList.Count == 0)
                            //{
                            //    ActivityCount = "0";
                            //}
                            //else
                            //{
                            //    ActivityCount = serviceResultBody?.Data1?.TotalActivitieCount;
                            //}

                            //if (string.IsNullOrEmpty(serviceResultBody?.Data1?.CategorywiseHourCount) || UserHoursList.Count == 0)
                            //{
                            //    ActivityHours = "0";
                            //}
                            //else
                            //{
                            //    ActivityHours = serviceResultBody?.Data1?.TotalActivitieHour;
                            //}
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task NewsLoadMoreItems()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                PageNo++;
                await LoadActivityDetail();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
           
        }
        //async Task ShowActivityDetail()
        //{
        //    try
        //    {
        //        UserActivityHoursList.Clear();
        //        //ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
        //        //HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
        //        ObservableCollection<HoursActivityCount> temp = new ObservableCollection<HoursActivityCount>();
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Activity Count" });
        //        UserActivityHoursList = temp;
        //        ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
        //        HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];

        //        ActivityTextColor = Color.White;
        //        HoursTextColor = (Color)App.Current.Resources["AccentColor"];
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.CommanException(ex);
        //    }
        //}
        //async Task ShowHoursDetail()
        //{
        //    try
        //    {
        //        UserActivityHoursList.Clear();
        //        ObservableCollection<HoursActivityCount> temp = new ObservableCollection<HoursActivityCount>();
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
        //        temp.Add(new HoursActivityCount() { Count = "10", Text = "Hours Count" });
        //        UserActivityHoursList = temp;
        //        ActivityBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
        //        HoursBGColor = (Color)App.Current.Resources["AccentColor"];
        //        HoursTextColor = Color.White;
        //        ActivityTextColor = (Color)App.Current.Resources["AccentColor"];
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.CommanException(ex);
        //    }

        //}
       
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

            //string[] AllMonthName = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var names = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(DateTime.Today.Month).Reverse().ToList();
            //var names = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }.Take(DateTime.Today.Month).ToList();
            Activitycollectionlist.Add(new ActivityMonthYear() { title = AppResources.AllTime, UnderlineIsVisible = true });
            foreach (var item in names)
            {
                Activitycollectionlist.Add(new ActivityMonthYear() { title = item, UnderlineIsVisible = false, Month = item, Year = DateTime.Now.Year.ToString() });
            }
            Activitycollectionlist.Add(new ActivityMonthYear() { title = (DateTime.Now.Year - 1).ToString(), UnderlineIsVisible = false, Year = (DateTime.Now.Year - 1).ToString() });
            Activitycollectionlist.Add(new ActivityMonthYear() { title = (DateTime.Now.Year - 2).ToString(), UnderlineIsVisible = false, Year = (DateTime.Now.Year - 2).ToString() });

            await LoadActivityDetail();
        }
    }
  
    public class ActivityMonthYear : BindableBase
    {
        public string title { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        private bool _UnderlineIsVisible;
        public bool UnderlineIsVisible
        {
            get { return _UnderlineIsVisible; }
            set { SetProperty(ref _UnderlineIsVisible, value); }
        }

    }

}


