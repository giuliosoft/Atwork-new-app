using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.UserModel;

namespace AtWork.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        #region Constructor
        public ProfilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.ProfileText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
        }
        #endregion

        #region Private Properties
        Volunteers _volunteers = new Volunteers();
        private bool _isShowSettingLogout = false;
        private bool _isShowBoxview = false;
        private bool _isVisibleBirthDate = false;
        private bool _isVisibleLocation = false;
        private bool _isVisibleStartDate = false;
        private bool _isVisibleEMPID = false;
        private bool _isVisibleCustomField = false;
        private bool _isVisibleContactMe = false;
        private bool _showActivityDetail = true;
        private bool _showHoursDetail = false;
        ObservableCollection<HoursActivityCount> UserActivityList = new ObservableCollection<HoursActivityCount>();
        ObservableCollection<HoursActivityCount> UserHoursList = new ObservableCollection<HoursActivityCount>();
        #endregion
        #region Public Properties
        public Volunteers volunteers
        {
            get { return _volunteers; }
            set { SetProperty(ref _volunteers, value); }
        }
        public bool isShowSettingLogout
        {
            get { return _isShowSettingLogout; }
            set { SetProperty(ref _isShowSettingLogout, value); }
        }
        public bool isShowBoxview
        {
            get { return _isShowBoxview; }
            set { SetProperty(ref _isShowBoxview, value); }
        }
        public bool isVisibleBirthDate
        {
            get { return _isVisibleBirthDate; }
            set { SetProperty(ref _isVisibleBirthDate, value); }
        }
        public bool isVisibleLocation
        {
            get { return _isVisibleLocation; }
            set { SetProperty(ref _isVisibleLocation, value); }
        }
        public bool isVisibleStartDate
        {
            get { return _isVisibleStartDate; }
            set { SetProperty(ref _isVisibleStartDate, value); }
        }
        public bool isVisibleEMPID
        {
            get { return _isVisibleEMPID; }
            set { SetProperty(ref _isVisibleEMPID, value); }
        }
        public bool isVisibleCustomField
        {
            get { return _isVisibleCustomField; }
            set { SetProperty(ref _isVisibleCustomField, value); }
        }
        public bool isVisibleContactMe
        {
            get { return _isVisibleContactMe; }
            set { SetProperty(ref _isVisibleContactMe, value); }
        }
        public bool showHoursDetail
        {
            get { return _showHoursDetail; }
            set { SetProperty(ref _showHoursDetail, value); }
        }
        public bool showActivityDetail
        {
            get { return _showActivityDetail; }
            set { SetProperty(ref _showActivityDetail, value); }
        }
        
        private ObservableCollection<FeedBackUIModel> _interestList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> interestList
        {
            get { return _interestList; }
            set { SetProperty(ref _interestList, value); }
        }
        private ObservableCollection<VolunteerClasses> _UserGroupList = new ObservableCollection<VolunteerClasses>();
        public ObservableCollection<VolunteerClasses> UserGroupList
        {
            get { return _UserGroupList; }
            set { SetProperty(ref _UserGroupList, value); }
        }
        private ObservableCollection<HoursActivityCount> _UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
        public ObservableCollection<HoursActivityCount> UserActivityHoursList
        {
            get { return _UserActivityHoursList; }
            set { SetProperty(ref _UserActivityHoursList, value); }
        }
        //public ObservableCollection<HoursActivityCount> UserActivityList
        //{
        //    get { return _UserActivityList; }
        //    set { SetProperty(ref _UserActivityList, value); }
        //}
        //public ObservableCollection<HoursActivityCount> UserHoursList
        //{
        //    get { return _UserHoursList; }
        //    set { SetProperty(ref _UserHoursList, value); }
        //}
        #endregion



        #region Commands
        public string Name { get; } = SettingsService.VolunteersUserData.FullName;
        public DelegateCommand SettingCommand { get { return new DelegateCommand(async () => await Setting()); } }
        public DelegateCommand LogoutCommand { get { return new DelegateCommand(async () => await Logout()); } }
        //public DelegateCommand OpenGroupMemberListCommand { get { return new DelegateCommand(async () => await OpenMemberList()); } }
        public DelegateCommand<VolunteerClasses> OpenGroupMemberListCommand { get { return new DelegateCommand<VolunteerClasses>(async (obj) => await OpenMemberList(obj)); } }

        public DelegateCommand OpenFullActivityCommand { get { return new DelegateCommand(async () => await FullActivityDetails()); } }
        public DelegateCommand OpenEmailCommand { get { return new DelegateCommand(async () => await OpenEmail()); } }
        public DelegateCommand OpenCallDialerCommand { get { return new DelegateCommand(async () => await OpenCallDialer()); } }
        public DelegateCommand ShowActivityCommand { get { return new DelegateCommand(async () => await ShowActivityDetail()); } }
        public DelegateCommand ShowHoursCommand { get { return new DelegateCommand(async () => await ShowHoursDetail()); } }
        #endregion

        #region private methods

        async Task ShowActivityDetail()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    UserActivityHoursList = new ObservableCollection<HoursActivityCount>();
                    UserActivityHoursList = UserActivityList;
                });

                //UserActivityHoursList.Clear();
                //UserActivityHoursList = UserActivityList;

                //ActivityBGColor = (Color)App.Current.Resources["AccentColor"];
                //HoursBGColor = (Color)App.Current.Resources["PosterWhiteColor"];

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

                //UserActivityHoursList.Clear();
                //UserActivityHoursList = UserHoursList;

                //ActivityBGColor = (Color)App.Current.Resources["PosterWhiteColor"];
                //HoursBGColor = (Color)App.Current.Resources["AccentColor"];
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }

        }
        async Task OpenEmail()
        {
            try
            {
                //1
                //try
                //{
                //    var message = new EmailMessage
                //    {
                //        To = volunteers?.volEmail.tol,
                //        //Cc = ccRecipients,
                //        //Bcc = bccRecipients
                //    };
                //    await Email.ComposeAsync(message);
                //}
                //catch (FeatureNotSupportedException fbsEx)
                //{
                //    // Email is not supported on this device
                //}
                //catch (Exception ex)
                //{
                //    // Some other exception occurred
                //}

                //2
                //Device.OpenUri(new Uri("mailto:"+volunteers?.volEmail));

                //3
                DependencyService.Get<PhoneCallEmail>().ComposerEmail(volunteers?.volEmail, AppResources.EmailNotSupportedAlert);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task OpenCallDialer()
        { 
            try
            {
                PhoneDialer.Open(volunteers?.volPhone);
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlertAsync(AppResources.PhoneDialerAlert);
                Console.WriteLine(AppResources.PhoneDialerAlert);
                ExceptionHelper.CommanException(ex);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task Setting()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(SettingsPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task OpenMemberList(VolunteerClasses userDetails)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ClassUniqueID", userDetails.classUniqueID);
                navigationParams.Add("ClassValue", userDetails.classValue);
                await _navigationService.NavigateAsync(nameof(MemberListPage), navigationParams);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task FullActivityDetails()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ActivityHistoryPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
       
        async Task Logout()
        {
            try
            {
                var result = await App.Current.MainPage.DisplayAlert(string.Empty, AppResources.LogoutAlert, AppResources.LogoutText, AppResources.Cancel);
                if (result)
                {
                    SessionService.CurrentTab = 0;
                    SessionService.IsWelcomeSetup = false;
                    SessionService.Logout();
                    await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}", null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task GetUserDetail(string volunteerID)
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetUserDetails(volunteerID);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<ProfileResponce>(serviceResult.Body);
                    if (serviceResultBody != null)
                    {
                        volunteers = serviceResultBody?.Data;
                        SessionService.volunteers = volunteers;
                        if (volunteers?.VolunteerClasses != null && volunteers?.VolunteerClasses.Count > 0)
                        {
                            ObservableCollection<VolunteerClasses> myCollection = new ObservableCollection<VolunteerClasses>(volunteers.VolunteerClasses as List<VolunteerClasses>);
                            UserGroupList = myCollection;
                            isShowBoxview = true;
                            var tempList = new ObservableCollection<VolunteerClasses>();
                            volunteers.VolunteerClasses.All((arg) =>
                            {
                                var tempData = new VolunteerClasses();
                                tempData.classUniqueID = arg.classUniqueID;
                                tempData.classDescription = arg.classDescription;
                                tempData.classValue= arg.classValue;
                                if (arg.grpFilter.ToLower() == "yes")
                                {
                                    tempData.isMyGroup = true;
                                    tempData.isGroup = false;
                                }
                                else
                                {
                                    tempData.isMyGroup = false;
                                    tempData.isGroup = true;
                                }
                                tempList.Add(tempData);
                                return true;
                            });
                            UserGroupList = tempList;
                        }
                        if (!string.IsNullOrEmpty(volunteers?.volInterests))
                        {
                            string imgStr = volunteers?.volInterests;
                            List<string> VolInterestList = new List<string>();
                            if (!string.IsNullOrEmpty(imgStr))
                            {
                                if (imgStr.Contains(","))
                                {
                                    VolInterestList = imgStr.Split(',').ToList();
                                }
                                else
                                {
                                    VolInterestList.Add(imgStr);
                                }
                            }
                            if (VolInterestList != null && VolInterestList.Count > 0)
                            {
                                interestList = new ObservableCollection<FeedBackUIModel>();
                                VolInterestList.All((arg) =>
                                {
                                    interestList.Add(new FeedBackUIModel() { Title = arg });
                                    return true;
                                });
                            }
                        }
                        //if (isVisibleBirthDate)
                        //{
                        //    isVisibleBirthDate = true;
                        //}
                        //if (!string.IsNullOrEmpty(volunteers?.classes))
                        //{
                        //    string UserDescription = volunteers?.classes;
                        //    List<string> UserDescriptionList = new List<string>();
                        //    if (!string.IsNullOrEmpty(UserDescription))
                        //    {
                        //        if (UserDescription.Contains(","))
                        //        {
                        //            UserDescriptionList = UserDescription.Split(',').ToList();
                        //        }
                        //        else
                        //        {
                        //            UserDescriptionList.Add(UserDescription);
                        //        }
                        //    }

                        //    if (UserDescriptionList != null && UserDescriptionList.Count > 0)
                        //    {
                        //        var tempCmtList = new ObservableCollection<UserDetails>();
                        //        UserDescriptionList.All((arg) =>
                        //        {
                        //            List<string> UserSingleDescriptionList = new List<string>();
                        //            if (!string.IsNullOrEmpty(arg))
                        //            {
                        //                if (arg.Contains(":"))
                        //                {
                        //                    UserSingleDescriptionList = arg.Split(':').ToList();
                        //                }
                        //                else
                        //                {
                        //                    UserSingleDescriptionList.Add(arg);
                        //                }
                        //                if (UserSingleDescriptionList.Count == 2)
                        //                {
                        //                    tempCmtList.Add(new UserDetails() { ClassID = UserSingleDescriptionList[0],   UserDescriptionTitle = UserSingleDescriptionList[1].ToUpper(), UserDescriptionValue = UserSingleDescriptionList[2] });
                        //                }
                        //            }
                        //            return true;
                        //        });
                        //        //UserGroupList = tempCmtList;
                        //isShowBoxview = true;
                        //        ObservableCollection<UserDetails> temp = new ObservableCollection<UserDetails>();
                        //        //temp.Add(new UserDetails() { UserDescriptionTitle = "BIRTHDAY", UserDescriptionValue = "February 12" });
                        //        //temp.Add(new UserDetails() { UserDescriptionTitle = "LOCATION", UserDescriptionValue = "Zurich" });
                        //        //temp.Add(new UserDetails() { UserDescriptionTitle = "START DATE", UserDescriptionValue = "September 1 2017" });
                        //        //temp.Add(new UserDetails() { UserDescriptionTitle = "EMPLOYEE ID", UserDescriptionValue = "L00110022" });
                        //        //temp.Add(new UserDetails() { UserDescriptionTitle = "CUSTOM FIELD", UserDescriptionValue = "Entry goed here" });
                        //        //UserPersonalDetails = temp;
                        //    }
                        //}

                        if (!string.IsNullOrEmpty(volunteers?.CategoryActivityCount))
                        {
                            string CategoryActivityAndCount = volunteers?.CategoryActivityCount;
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
                        if (!string.IsNullOrEmpty(volunteers?.CategorywiseHourCount))
                        {
                            string CategoryHoursAndCount = volunteers?.CategorywiseHourCount;
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

                        if (SettingsService.VolunteersUserData.volUniqueID == volunteerID)
                        {
                            await ChangeLanguage(volunteers?.volLanguage, true);
                        }
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
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
            try
            {
                string VolunteerId = parameters.GetValue<string>("VolId");
                if (!string.IsNullOrEmpty(VolunteerId))
                {
                    if (SettingsService.VolunteersUserData.volUniqueID == VolunteerId)
                    {
                        isShowSettingLogout = true;
                    }
                    else
                    {
                        isShowSettingLogout = false;
                    }
                    await GetUserDetail(VolunteerId);
                }
                else
                {
                    isShowSettingLogout = true;
                    await GetUserDetail(SettingsService.VolunteersUserData.volUniqueID);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
    public class UserDetails
    {
        public string ClassID { get; set; }
        public string UserDescriptionTitle { get; set; }
        public string UserDescriptionValue { get; set; }

    }
    

    public class HoursActivityCount
    {
        public string Count { get; set; }
        public string Text { get; set; }

    }
}
