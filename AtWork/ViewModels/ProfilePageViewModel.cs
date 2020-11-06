using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        
        private ObservableCollection<FeedBackUIModel> _interestList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> interestList
        {
            get { return _interestList; }
            set { SetProperty(ref _interestList, value); }
        }
        private ObservableCollection<UserDetails> _UserDetailsList = new ObservableCollection<UserDetails>();
        public ObservableCollection<UserDetails> UserDetailsList
        {
            get { return _UserDetailsList; }
            set { SetProperty(ref _UserDetailsList, value); }
        }
        private ObservableCollection<UserDetails> _UserPersonalDetails = new ObservableCollection<UserDetails>();
        public ObservableCollection<UserDetails> UserPersonalDetails
        {
            get { return _UserPersonalDetails; }
            set { SetProperty(ref _UserPersonalDetails, value); }
        }
        #endregion



        #region Commands
        public string Name { get; } = SettingsService.VolunteersUserData.FullName;
        public DelegateCommand SettingCommand { get { return new DelegateCommand(async () => await Setting()); } }
        public DelegateCommand LogoutCommand { get { return new DelegateCommand(async () => await Logout()); } }
        public DelegateCommand OpenMemberListCommand { get { return new DelegateCommand(async () => await OpenMemberList()); } }
        public DelegateCommand OpenFullActivityCommand { get { return new DelegateCommand(async () => await FullActivityDetails()); } }
        public DelegateCommand OpenEmailCommand { get { return new DelegateCommand(async () => await OpenEmail()); } }
        public DelegateCommand OpenCallDialerCommand { get { return new DelegateCommand(async () => await OpenCallDialer()); } }
        #endregion

        #region private methods

        async Task OpenEmail()
        {
            try
            {
                DependencyService.Get<PhoneCallEmail>().ComposerEmail("abc@gmail.com");
            }
            catch (Exception ex)
            {

            }
        }
        async Task OpenCallDialer()
        {
            try
            {
                DependencyService.Get<PhoneCallEmail>().MakeQuickCall("123456789");
            }
            catch (Exception ex)
            {

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

            }
        }
        async Task OpenMemberList()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(MemberListPage));
            }
            catch (Exception ex)
            {

            }
        }
        async Task FullActivityDetails()
        {
            try
            {
                //await _navigationService.NavigateAsync(nameof(ActivityHistoryPage));
            }
            catch (Exception ex)
            {

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
                        if (!string.IsNullOrEmpty(volunteers?.classes))
                        {
                            string UserDescription = volunteers?.classes;
                            List<string> UserDescriptionList = new List<string>();
                            if (!string.IsNullOrEmpty(UserDescription))
                            {
                                if (UserDescription.Contains(","))
                                {
                                    UserDescriptionList = UserDescription.Split(',').ToList();
                                }
                                else
                                {
                                    UserDescriptionList.Add(UserDescription);
                                }
                            }

                            if (UserDescriptionList != null && UserDescriptionList.Count > 0)
                            {
                                var tempCmtList = new ObservableCollection<UserDetails>();
                                UserDescriptionList.All((arg) =>
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
                                            tempCmtList.Add(new UserDetails() { UserDescriptionTitle = UserSingleDescriptionList[0].ToUpper(), UserDescriptionValue = UserSingleDescriptionList[1] });
                                        }
                                    }

                                    return true;
                                });
                                UserDetailsList = tempCmtList;
                                isShowBoxview = true;
                                ObservableCollection<UserDetails> temp = new ObservableCollection<UserDetails>();
                                temp.Add(new UserDetails() { UserDescriptionTitle = "BIRTHDAY", UserDescriptionValue = "February 12" });
                                temp.Add(new UserDetails() { UserDescriptionTitle = "LOCATION", UserDescriptionValue = "Zurich" });
                                temp.Add(new UserDetails() { UserDescriptionTitle = "START DATE", UserDescriptionValue = "September 1 2017" });
                                temp.Add(new UserDetails() { UserDescriptionTitle = "EMPLOYEE ID", UserDescriptionValue = "L00110022" });
                                temp.Add(new UserDetails() { UserDescriptionTitle = "CUSTOM FIELD", UserDescriptionValue = "Entry goed here" });
                                temp.Add(new UserDetails() { UserDescriptionTitle = "CONTACT ME", UserDescriptionValue = "carron@atwork.com" });
                                UserPersonalDetails = temp;
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
        public string UserDescriptionTitle { get; set; }
        public string UserDescriptionValue { get; set; }

    }
}
