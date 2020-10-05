using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using AtWork.Popups;
using AtWork.Helpers;
using Newtonsoft.Json;
using static AtWork.Models.ActivityModel;
using AtWork.Models;
using AtWork.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Calendars;
using System.Collections.Generic;
using Plugin.Calendars.Abstractions;
using System.Linq;
using System.Globalization;
using System.Web;

namespace AtWork.ViewModels
{
    public class ActivityDetailPageViewModel : ViewModelBase
    {
        #region Constructor
        public ActivityDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            NewsOptionCommand = ShowNewsOptionCommand;
            HeaderDetailsTitle = AppResources.ActivityText;
            if (IsFromMyActivity)
            {
                JoinActivity = false;
                UnSubscribeActivity = true;
            }
            else
            {
                JoinActivity = true;
                UnSubscribeActivity = false;
            }
            _settingHelperService = DependencyService.Get<ISettingsHelper>();
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private string _Location = string.Empty;
        private string _ActivityTime = string.Empty;
        private ActivityListModel _activityDetails = new ActivityListModel();
        private bool _IsShowCategotyType = false;
        private ObservableCollection<ActivityTagModel> _ActivityTagList = new ObservableCollection<ActivityTagModel>();
        private ObservableCollection<ActivityCarouselListModel> _ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
        string SelectedActivityID = string.Empty;
        ISettingsHelper _settingHelperService;
        private string _OrganizationAddress = string.Empty;
        public ObservableCollection<EmojiDisplayModel> _EmojiList;
        #endregion

        #region Public Properties
        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }
        public string ActivityTime
        {
            get { return _ActivityTime; }
            set { SetProperty(ref _ActivityTime, value); }
        }
        public ActivityListModel ActivityDetails
        {
            get { return _activityDetails; }
            set { SetProperty(ref _activityDetails, value); }
        }

        public bool IsShowCategotyType
        {
            get { return _IsShowCategotyType; }
            set { SetProperty(ref _IsShowCategotyType, value); }
        }

        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ObservableCollection<ActivityTagModel> ActivityTagList
        {
            get { return _ActivityTagList; }
            set { SetProperty(ref _ActivityTagList, value); }
        }

        public ObservableCollection<ActivityCarouselListModel> ActivityCarouselList
        {
            get { return _ActivityCarouselList; }
            set { SetProperty(ref _ActivityCarouselList, value); }
        }

        public string OrganizationAddress
        {
            get { return _OrganizationAddress; }
            set { SetProperty(ref _OrganizationAddress, value); }
        }

        public ObservableCollection<EmojiDisplayModel> EmojiList
        {
            get { return _EmojiList; }
            set { SetProperty(ref _EmojiList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand GoToJoinActivityPopupCommand { get { return new DelegateCommand(async () => await GoToJoinActivityPopup()); } }
        public DelegateCommand GoToUnsubscribeActivityPopupCommand { get { return new DelegateCommand(async () => await GoToUnsubscribeActivityPopup()); } }
        public DelegateCommand GoToToastMessageCommand { get { return new DelegateCommand(async () => await GoToToastMessage()); } }
        public DelegateCommand LinkClickedCommand { get { return new DelegateCommand(async () => LinkClicked()); } }
        public DelegateCommand<string> JoinedMemberCommand { get { return new DelegateCommand<string>(async (obj) => await JoinedMember(obj)); } }
        public DelegateCommand ShowNewsOptionCommand { get { return new DelegateCommand(async () => await ShowNewsOption()); } }
        public DelegateCommand ActivityContactCommand { get { return new DelegateCommand(async () => await ContactThroughMail()); } }
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

        async Task LinkClicked()
        {
            try
            {
                if (ActivityDetails != null && !string.IsNullOrEmpty(ActivityDetails.proAddActivity_Website))
                {
                    var url = ActivityDetails.proAddActivity_Website.Substring(0, 4).ToLower() != "http" ? "http://" + ActivityDetails.proAddActivity_Website : ActivityDetails.proAddActivity_Website;
                    await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        async Task JoinedMember(string selectedActivityPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ActivityID", selectedActivityPost);
                await _navigationService.NavigateAsync(nameof(MemberListPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task GoToToastMessage()
        {
            try
            {
                ToastMessagePopup ToastMessagePopup = new ToastMessagePopup();
                ToastMessagePopupViewModel ToastMessagePopupViewModel = new ToastMessagePopupViewModel(_navigationService, _facadeService);
                if (ActivityDetails != null && !string.IsNullOrEmpty(ActivityDetails.proUniqueID))
                {
                    string copyTextUrl = string.Empty;
                    if (ConfigService.IsProduction)
                    {
                        copyTextUrl = string.Format("{0}{1}", "http://engage.atwork.ai/employee/Activity_Detail.aspx?uid=", ActivityDetails.proUniqueID);
                    }
                    else
                    {
                        copyTextUrl = string.Format("{0}{1}", "http://voluntycorporate.atlasics.com/employee/Activity_Detail.aspx?uid=", ActivityDetails.proUniqueID);
                    }
                    await Clipboard.SetTextAsync(copyTextUrl);
                    await Clipboard.GetTextAsync();
                    ToastMessagePopupViewModel.ToastText = AppResources.LinkCopiedText;
                }

                ToastMessagePopup.BindingContext = ToastMessagePopupViewModel;
                await PopupNavigationService.ShowPopup(ToastMessagePopup, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task ContactThroughMail()
        {
            try
            {
                if (!string.IsNullOrEmpty(ActivityDetails.Contact))
                {
                    await Launcher.OpenAsync(new Uri(String.Format("mailto:{0}?subject={1}", ActivityDetails.Contact, AppResources.AppTitle)));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(AppResources.EmailServiceAlert);
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GoToUnsubscribeActivityPopup()
        {
            try
            {
                UnSubscribeActivityPopup UnSubscribeActivityPopup = new UnSubscribeActivityPopup();
                UnSubscribeActivityPopupViewModel UnSubscribeActivityPopupViewModel = new UnSubscribeActivityPopupViewModel(_navigationService, _facadeService);
                UnSubscribeActivityPopupViewModel.ConfirmEvent += async (object sender, object SelectedObj) =>
                {
                    try
                    {
                        if (!await CheckConnectivity())
                        {
                            return;
                        }
                        JoinActivityInputModel inputModel = new JoinActivityInputModel();
                        inputModel.coUniqueID = ActivityDetails.coUniqueID;
                        inputModel.proUniqueID = ActivityDetails.proUniqueID;
                        inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                        inputModel.proStatus = TextResources.InActiveStatus;
                        DateTime dtVal = DateTime.Now;
                        bool parsed = DateTime.TryParse(ActivityDetails.proVolHourDates, out dtVal);
                        if (parsed)
                            inputModel.proVolHourDates = dtVal;
                        await ShowLoader();
                        var serviceResult = await ActivityService.UnSubscribeActivity(inputModel);
                        if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                        {
                            var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                            if (serviceBody != null && serviceBody.Flag)
                            {
                                SessionService.IsShowActivitiesIntial = true;
                                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                            }
                        }
                        await ClosePopup();
                    }
                    catch (Exception ex)
                    {
                        await ClosePopup();
                        Debug.WriteLine(ex.Message);
                    }
                };
                UnSubscribeActivityPopup.BindingContext = UnSubscribeActivityPopupViewModel;
                await PopupNavigationService.ShowPopup(UnSubscribeActivityPopup, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GoToJoinActivityPopup()
        {
            try
            {
                var tempDtList = new ObservableCollection<JoinActivityDatesModel>();
                if (!string.IsNullOrEmpty(ActivityDetails.StartDate))
                {
                    string dateStr = ActivityDetails.StartDate;
                    List<string> dateList = new List<string>();
                    if (!string.IsNullOrEmpty(dateStr))
                    {
                        if (dateStr.Contains(","))
                        {
                            dateList = dateStr.Split(',').ToList();
                        }
                        else
                        {
                            dateList.Add(dateStr);
                        }
                    }
                    if (dateList != null && dateList.Count > 0)
                    {
                        int i = 1;
                        dateList.All((dtArg) =>
                        {
                            DateTime dtVal = DateTime.Now;
                            bool parsed = DateTime.TryParse(dtArg, out dtVal);
                            //dtVal = Convert.ToDateTime(dtArg);
                            //string format = "yyyy-MM-dd";
                            //bool parsed = DateTime.TryParseExact(dtArg, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtVal);
                            if (parsed)
                            {
                                string dtStrVal = dtVal.ToString("d MMM yyyy");
                                tempDtList.Add(new JoinActivityDatesModel() { Id = i, DisplayDateString = dtStrVal, ActivityDate = dtVal, IsSelected = false });
                                i++;
                            }
                            return true;
                        });
                    }
                }

                if (ActivityDetails.DataType.Equals(TextResources.OnDemandCategoryText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(ActivityDetails.EndDate.Trim()))
                    {
                        DateTime endDateVal = DateTime.Now;
                        bool parsedEndDate = DateTime.TryParse(ActivityDetails.EndDate, out endDateVal);

                        if (parsedEndDate)
                        {
                            DateTime currDate = DateTime.Now.Date;
                            if (endDateVal > currDate)
                            {
                                JoinActivityInputModel inputModel = new JoinActivityInputModel();
                                //inputModel.ActivityID = ActivityDetails.id;
                                inputModel.coUniqueID = ActivityDetails.coUniqueID;
                                inputModel.proUniqueID = ActivityDetails.proUniqueID;
                                inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                                inputModel.proStatus = TextResources.ActiveStatus;

                                await CallJoinActivityService(inputModel);
                                await DisplayAlertAsync(AppResources.JoinActivitySuccessfulText);
                            }
                        }
                    }
                }
                else if (ActivityDetails.DataType.Equals(TextResources.RecurringCategoryText, StringComparison.InvariantCultureIgnoreCase) || ActivityDetails.DataType.Equals(TextResources.RegularCategoryText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (tempDtList != null && tempDtList.Count > 1)
                    {
                        JoinActivityPopup JoinActivityPopup = new JoinActivityPopup();
                        JoinActivityPopupViewModel joinactivityPopupViewModel = new JoinActivityPopupViewModel(_navigationService, _facadeService);
                        joinactivityPopupViewModel.SelectedActivityType = ActivityDetails.DataType;

                        if (tempDtList != null && tempDtList.Count > 0)
                        {
                            joinactivityPopupViewModel.ActivityJoinDates = new ObservableCollection<JoinActivityDatesModel>(tempDtList);
                        }

                        joinactivityPopupViewModel.JoinActivityEvent += async (object sender, List<JoinActivityDatesModel> SelectedDatesObj) =>
                        {
                            try
                            {
                                if (SelectedDatesObj != null && SelectedDatesObj.Count > 0)
                                {
                                    var hasPermission = await TakePermissionForCalendar();
                                    if (hasPermission)
                                    {
                                        List<string> selectedRecurringDates = new List<string>();
                                        try
                                        {
                                            var calendars = await CrossCalendars.Current.GetCalendarsAsync();
                                            //For simulator :
                                            //var defaultCalendar = calendars.Where((x) => x.AccountName == TextResources.DefaultCalendarText && x.CanEditEvents).FirstOrDefault();
                                            //For device :
                                            //var defaultCalendar = calendars.Where((x) => x.AccountName.Equals(TextResources.iCloudCalendarText, StringComparison.InvariantCultureIgnoreCase) && x.CanEditEvents).FirstOrDefault();

#if DEBUG
                                            var defaultCalendar = calendars.Where((x) => x.AccountName == TextResources.DefaultCalendarText && x.CanEditEvents).FirstOrDefault();
#else      
                                            var defaultCalendar = calendars.Where((x) => x.AccountName.Equals(TextResources.iCloudCalendarText, StringComparison.InvariantCultureIgnoreCase) && x.CanEditEvents).FirstOrDefault();
#endif


                                            foreach (var selDt in SelectedDatesObj)
                                            {
                                                var dtString = selDt.ActivityDate.ToString("yyyy-MM-dd");
                                                selectedRecurringDates.Add(dtString);
                                                DateTime dateToAddInCalendar = selDt.ActivityDate;

                                                //New for event creation based on date time from service :
                                                var DateStr = dateToAddInCalendar.ToString(("MM/dd/yyyy"));
                                                var startTimeStr = ActivityDetails.proAddActivity_StartTime;
                                                DateTime eventStartDateTime = Convert.ToDateTime(DateStr).Add(TimeSpan.Parse(startTimeStr));
                                                var endTimeStr = ActivityDetails.proAddActivity_EndTime;
                                                DateTime eventEndDateTime = Convert.ToDateTime(DateStr).Add(TimeSpan.Parse(endTimeStr));

                                                var calendarEvent = new CalendarEvent
                                                {
                                                    Name = AppResources.AtWorkActivityEventText,
                                                    Start = eventStartDateTime,
                                                    End = eventEndDateTime,
                                                    Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
                                                };
                                                await CrossCalendars.Current.AddOrUpdateEventAsync(defaultCalendar, calendarEvent);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine(ex.Message);
                                        }
                                        if (!await CheckConnectivity())
                                        {
                                            return;
                                        }

                                        JoinActivityInputModel inputModel = new JoinActivityInputModel();
                                        //inputModel.ActivityID = ActivityDetails.id;
                                        inputModel.coUniqueID = ActivityDetails.coUniqueID;
                                        inputModel.proUniqueID = ActivityDetails.proUniqueID;
                                        inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                                        inputModel.proStatus = TextResources.ActiveStatus;
                                        inputModel.proVolHourDates = ActivityDetails.proAddActivityDate;
                                        if (ActivityDetails.DataType.Equals(TextResources.RegularCategoryText, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            inputModel.proChosenDate = SelectedDatesObj.FirstOrDefault().ActivityDate;
                                        }
                                        else if (ActivityDetails.DataType.Equals(TextResources.RecurringCategoryText, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            string delim = ",";
                                            string commaSeparatedDates = String.Join(delim, selectedRecurringDates);
                                            inputModel.RecurringDates = commaSeparatedDates;
                                        }
                                        await CallJoinActivityService(inputModel);
                                    }
                                }
                                else
                                {
                                    await DisplayAlertAsync(AppResources.JoinActivityAlertText);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        };
                        JoinActivityPopup.BindingContext = joinactivityPopupViewModel;
                        await PopupNavigationService.ShowPopup(JoinActivityPopup, true);
                    }
                    else if (tempDtList != null && tempDtList.Count == 1)
                    {
                        var hasPermission = await TakePermissionForCalendar();
                        if (hasPermission)
                        {
                            try
                            {
                                var calendars = await CrossCalendars.Current.GetCalendarsAsync();
                                //For simulator :
                                //var defaultCalendar = calendars.Where((x) => x.AccountName == TextResources.DefaultCalendarText && x.CanEditEvents).FirstOrDefault();
                                //For device :
                                //var defaultCalendar = calendars.Where((x) => x.AccountName.Equals(TextResources.iCloudCalendarText, StringComparison.InvariantCultureIgnoreCase) && x.CanEditEvents).FirstOrDefault();

#if DEBUG
                                var defaultCalendar = calendars.Where((x) => x.AccountName == TextResources.DefaultCalendarText && x.CanEditEvents).FirstOrDefault();
#else      
                                var defaultCalendar = calendars.Where((x) => x.AccountName.Equals(TextResources.iCloudCalendarText, StringComparison.InvariantCultureIgnoreCase) && x.CanEditEvents).FirstOrDefault();
#endif

                                DateTime dateToAddInCalendar = tempDtList.FirstOrDefault().ActivityDate;

                                var DateStr = dateToAddInCalendar.ToString(("MM/dd/yyyy"));
                                var startTimeStr = ActivityDetails.proAddActivity_StartTime;
                                DateTime eventStartDateTime = Convert.ToDateTime(DateStr).Add(TimeSpan.Parse(startTimeStr));
                                var endTimeStr = ActivityDetails.proAddActivity_EndTime;
                                DateTime eventEndDateTime = Convert.ToDateTime(DateStr).Add(TimeSpan.Parse(endTimeStr));

                                var calendarEvent = new CalendarEvent
                                {
                                    Name = AppResources.AtWorkActivityEventText,
                                    Start = eventStartDateTime,
                                    End = eventEndDateTime,
                                    Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
                                };
                                await CrossCalendars.Current.AddOrUpdateEventAsync(defaultCalendar, calendarEvent);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                            if (!await CheckConnectivity())
                            {
                                return;
                            }
                            JoinActivityInputModel inputModel = new JoinActivityInputModel();
                            //inputModel.ActivityID = ActivityDetails.id;
                            inputModel.coUniqueID = ActivityDetails.coUniqueID;
                            inputModel.proUniqueID = ActivityDetails.proUniqueID;
                            inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                            inputModel.proStatus = TextResources.ActiveStatus;
                            inputModel.proVolHourDates = ActivityDetails.proAddActivityDate;
                            if (ActivityDetails.DataType.Equals(TextResources.RegularCategoryText, StringComparison.InvariantCultureIgnoreCase))
                            {
                                inputModel.proChosenDate = tempDtList.FirstOrDefault().ActivityDate;
                            }
                            else if (ActivityDetails.DataType.Equals(TextResources.RecurringCategoryText, StringComparison.InvariantCultureIgnoreCase))
                            {
                                inputModel.RecurringDates = tempDtList.FirstOrDefault().ActivityDate.ToString("yyyy-MM-dd");
                            }

                            await CallJoinActivityService(inputModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task LoadActivitiesDetails()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                var serviceResult = await ActivityService.GetActivityDetail(SelectedActivityID.ToString());// "procorp2019023511232400420207251552438");
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityListModel>(serviceResult.Body);

                if (serviceResultBody != null)
                {
                    ActivityDetails = serviceResultBody;
                    //Location = serviceResultBody?.proAddress1;// + ", " + serviceResultBody?.proCountry;
                    //ActivityTime = serviceResultBody?.proAddActivity_StartTime != null && serviceResultBody?.proAddActivity_StartTime != string.Empty && serviceResultBody?.proAddActivity_EndTime != null && serviceResultBody?.proAddActivity_EndTime != string.Empty
                    //    ? serviceResultBody.proAddActivity_StartTime + " to  " + serviceResultBody.proAddActivity_EndTime
                    //    : string.Empty;
                    DetailHeaderOptionIsVisible = ActivityDetails?.volUniqueID == SettingsService.VolunteersUserData?.volUniqueID;
                    if (serviceResultBody?.proCategoryName != null && serviceResultBody?.proCategoryName != string.Empty)
                    {
                        IsShowCategotyType = true;
                    }
                    if (!string.IsNullOrEmpty(ActivityDetails.Keyword))
                    {
                        var keywordStr = ActivityDetails.Keyword;
                        List<string> keywordList = new List<string>();

                        if (keywordStr.Contains(","))
                        {
                            keywordList = keywordStr.Split(',').ToList();
                        }
                        else
                        {
                            keywordList.Add(keywordStr);
                        }
                        keywordList = keywordList.Select(x => HttpUtility.HtmlDecode(x)).ToList();
                        ObservableCollection<ActivityTagModel> tempTags = new ObservableCollection<ActivityTagModel>();
                        keywordList.All((arg) =>
                        {
                            tempTags.Add(new ActivityTagModel() { ActivityTag = arg.Trim() });
                            return true;
                        });
                        ActivityTagList = tempTags;
                    }
                    if (!string.IsNullOrEmpty(ActivityDetails.Companie_Address1) && !string.IsNullOrEmpty(ActivityDetails.Companie_Address2))
                    {
                        OrganizationAddress = ActivityDetails.Companie_Address1 + ", " + ActivityDetails.Companie_Address2;
                    }
                    else
                    {
                        OrganizationAddress = ActivityDetails.Companie_Address1;
                    }
                    //string ActivityDetails.Emoji = "em em-smiley,em em-kissing_heart,em em-smirk,em em-slightly_frowning_face,em em-peace_symbol,em em-sunglasses,em em-sunglasses";
                    List<string> emojiList = new List<string>();
                    if (!string.IsNullOrEmpty(ActivityDetails.Emoji))
                    {
                        if (ActivityDetails.Emoji.Contains(","))
                        {
                            emojiList = ActivityDetails.Emoji.Trim().Split(',').ToList();
                        }
                        else
                        {
                            emojiList.Add(ActivityDetails.Emoji);
                        }
                    }
                    emojiList = emojiList.Select(x => x.Trim()).ToList();
                    ObservableCollection<EmojiDisplayModel> EmojisList = CommonUtility.EmojisList();
                    if (emojiList != null && emojiList.Count > 0)
                    {
                        EmojiList = new ObservableCollection<EmojiDisplayModel>();
                        emojiList.All((Arg) =>
                        {
                            string EmojiCode = EmojisList.Where(x => x.EmojiName == Arg).Select(x => x.EmojiCode).FirstOrDefault();
                            EmojiList.Add(new EmojiDisplayModel() { EmojiCode = EmojiCode });
                            return true;
                        });
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

        async Task CallJoinActivityService(JoinActivityInputModel inputModel)
        {
            try
            {
                await ShowLoader();
                var serviceResult = await ActivityService.JoinActivity(inputModel);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null)
                        {
                            if (serviceBody.Flag)
                            {
                                //SessionService.IsShowActivitiesIntial = true;
                                SessionService.isFromJoinActivity = true;
                                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}/{nameof(MyActivityPage)}", null);
                            }
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
        }
        #endregion

        #region public methods
        async Task<bool> TakePermissionForCalendar()
        {
            var retVal = false;
            try
            {
                var calendarWritePermissionStatus = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();
                if (calendarWritePermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    calendarWritePermissionStatus = await Permissions.RequestAsync<Permissions.CalendarWrite>();
                }

                var calendarReadPermissionStatus = await Permissions.CheckStatusAsync<Permissions.CalendarRead>();
                if (calendarReadPermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    calendarReadPermissionStatus = await Permissions.RequestAsync<Permissions.CalendarRead>();
                }

                if (calendarReadPermissionStatus == Xamarin.Essentials.PermissionStatus.Granted &&
                    calendarWritePermissionStatus == Xamarin.Essentials.PermissionStatus.Granted)
                {
                    retVal = true;
                }
                else
                {
                    var res = await App.Current.MainPage.DisplayAlert(AppResources.AlertTitle, AppResources.CalendarPermissionAlert, AppResources.AlertOkText, AppResources.Cancel);
                    if (res)
                    {
                        await _settingHelperService.OpenAppSettings();
                    }
                    //if (calendarWritePermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                    //{
                    //    await DisplayAlertAsync(AppResources.CalendarPermissionAlert);
                    //}
                    //else if (calendarReadPermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                    //{
                    //    await DisplayAlertAsync(AppResources.CalendarPermissionAlert);
                    //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await DisplayAlertAsync(AppResources.PermissionErrorText);
            }
            return retVal;
        }
        async Task ShowNewsOption()
        {
            try
            {
                NewsOptionPopup newsOptionPopup = new NewsOptionPopup();
                NewsOptionPopupViewModel newsOptionPopupViewModel = new NewsOptionPopupViewModel(_navigationService, _facadeService);
                newsOptionPopupViewModel.DeletePost = AppResources.DeleteActivity;
                newsOptionPopupViewModel.EditPost = AppResources.EditActivity;
                newsOptionPopupViewModel.EditNewsEvent += async (object sender, object SelectedObj) =>
                {
                    try
                    {
                        await ShowLoader();
                        SessionService.ActivityPostInputData = new ActivityListModel();
                        if (ActivityDetails != null)
                        {
                            SessionService.isEditingActivity = true;
                            SessionService.ActivityPostInputData = ActivityDetails;
                            if (ActivityDetails.proAudience.ToLower() == "post to everybody")
                            {
                                SessionService.SelectedItemPosttype = "everyone";
                            }
                            else
                            {
                                SessionService.SelectedItemPosttype = "mygroup";
                            }
                            if (ActivityCarouselList != null && ActivityCarouselList.Count > 0)
                            {
                                var tempList = new List<string>();
                                ActivityCarouselList.All((arg) =>
                                {
                                    tempList.Add(arg.ActivityImageUrl);
                                    return true;
                                });
                                SessionService.NewsPostCarouselImages = tempList;
                            }
                        }
                        await _navigationService.NavigateAsync(nameof(CreateActivityPage), null);
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
                            var serviceResult = await ActivityService.DeleteActivity(ActivityDetails.id);
                            if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                            {
                                SessionService.DeletedActivityPost = ActivityDetails.id.ToString();
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
                var activityId = parameters.GetValue<string>("SelectedActivityID");
                if (!string.IsNullOrEmpty(activityId))
                {
                    SelectedActivityID = activityId;
                }
                var cList = parameters.GetValue<ObservableCollection<ActivityCarouselListModel>>("SelectedActivityImages");
                if (cList != null)
                {
                    ActivityCarouselList = cList;
                }
                await LoadActivitiesDetails();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class ActivityTagModel
    {
        public string ActivityTag { get; set; }
    }
}
