using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        #endregion



        #region Commands
        public string Name { get; } = SettingsService.VolunteersUserData.FullName;
        public DelegateCommand SettingCommand { get { return new DelegateCommand(async () => await Setting()); } }
        public DelegateCommand LogoutCommand { get { return new DelegateCommand(async () => await Logout()); } }
        #endregion

        #region private methods

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
                            }

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
                Debug.WriteLine(ex.Message);
            }
        }
    }
    public class UserDetails
    {
        public string UserDescriptionTitle { get; set; }
        public string UserDescriptionValue { get; set; }

    }
}
