using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #endregion
        #region Public Properties
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
                SessionService.CurrentTab = 0;
                SessionService.IsWelcomeSetup = false;
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}", null);
            }
            catch (Exception ex)
            {

            }
        }
        async Task GetUserDetail()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetUserDetails(SettingsService.VolunteersUserData.volUniqueID);
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
            await GetUserDetail();
            
            
        }
    }
}
