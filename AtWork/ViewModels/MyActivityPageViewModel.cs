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
using static AtWork.Models.ActivityModel;

namespace AtWork.ViewModels
{
    public class MyActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public MyActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.MyActivitesText;
            MyActivitycollectionlist.Add(new ActivityItems());
            MyActivitycollectionlist.Add(new ActivityItems());
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ObservableCollection<ActivityItems> _MyActivitycollectionlist = new ObservableCollection<ActivityItems>();
        bool _IsRefreshingMyActivities = false;
        private ObservableCollection<ActivityListModel> _MyUpcomingActivitylist = new ObservableCollection<ActivityListModel>();
        private ObservableCollection<ActivityListModel> _MyPastActivitylist = new ObservableCollection<ActivityListModel>();
        #endregion

        #region Public Properties
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ObservableCollection<ActivityItems> MyActivitycollectionlist
        {
            get { return _MyActivitycollectionlist; }
            set { SetProperty(ref _MyActivitycollectionlist, value); }
        }

        public bool IsRefreshingMyActivities
        {
            get { return _IsRefreshingMyActivities; }
            set { SetProperty(ref _IsRefreshingMyActivities, value); }
        }

        public ObservableCollection<ActivityListModel> MyUpcomingActivitylist
        {
            get { return _MyUpcomingActivitylist; }
            set { SetProperty(ref _MyUpcomingActivitylist, value); }
        }

        public ObservableCollection<ActivityListModel> MyPastActivitylist
        {
            get { return _MyPastActivitylist; }
            set { SetProperty(ref _MyPastActivitylist, value); }
        }
        #endregion

        #region Commands
        //public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        public DelegateCommand GotoCreateActivityCommand { get { return new DelegateCommand(async () => await GotoCreateActivity()); } }
        public DelegateCommand MyActivityRefreshCommand { get { return new DelegateCommand(async () => await ExecuteMyActivityRefreshCommand()); } }
        public DelegateCommand<ActivityListModel> UpcomingActivitySelectedCommand { get { return new DelegateCommand<ActivityListModel>(async (obj) => await GotoActivityDetails(obj)); } }
        public DelegateCommand<ActivityListModel> PastActivitySelectedCommand { get { return new DelegateCommand<ActivityListModel>(async (obj) => await GoToFeedBackPage(obj)); } }
        public DelegateCommand<ActivityListModel> JoinedMemberCommand { get { return new DelegateCommand<ActivityListModel>(async (obj) => await JoinedMember(obj)); } }
        #endregion

        #region private methods

        async Task GotoActivityDetails(ActivityListModel selectedActivityPost)
        {
            try
            {
                IsFromMyActivity = true;
                if (!selectedActivityPost.IsPastActivity)
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("SelectedActivityID", selectedActivityPost.proUniqueID);
                    navigationParams.Add("SelectedActivityImages", selectedActivityPost.ActivityCarouselList);
                    await _navigationService.NavigateAsync(nameof(ActivityDetailPage), navigationParams);
                }
                else
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("SelectedPastActivity", selectedActivityPost);
                    await _navigationService.NavigateAsync(nameof(YourFeedbackPage), navigationParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GoToFeedBackPage(ActivityListModel selectedActivityPost)
        {
            try
            {
                //await _navigationService.NavigateAsync(nameof(YourFeedbackPage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task JoinedMember(ActivityListModel selectedActivityPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ActivityID", selectedActivityPost.proUniqueID);
                await _navigationService.NavigateAsync(nameof(MemberListPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task ExecuteMyActivityRefreshCommand()
        {
            IsRefreshingMyActivities = true;
            //ActivityPageNo = 1;
            await GetMyActivityList(isfromRefresh: true);
            IsRefreshingMyActivities = false;
        }

        async Task GetMyActivityList(bool isfromRefresh = false)
        {
            try
            {
                //if (IsBusyInActivityBinding)
                //{
                //    return;
                //}
                //IsBusyInActivityBinding = true;
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!isfromRefresh)
                    await ShowLoader();
                var serviceResult = await ActivityService.GetMyActivityList(SettingsService.VolunteersUserData.volUniqueID);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<ActivityResponse>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Flag)
                    {
                        var tempList = new ObservableCollection<ActivityListModel>();
                        var tempList1 = new ObservableCollection<ActivityListModel>();
                        if (serviceResultBody.Data != null && serviceResultBody.Data.Count > 0)
                        {
                            tempList = new ObservableCollection<ActivityListModel>(serviceResultBody.Data);
                            tempList.All((arg) =>
                            {
                                if (!string.IsNullOrEmpty(arg.proBackgroundImage))
                                {
                                    string imgStr = arg.proBackgroundImage;
                                    List<string> nimgUrlList = new List<string>();
                                    if (!string.IsNullOrEmpty(imgStr))
                                    {
                                        if (imgStr.Contains(","))
                                        {
                                            nimgUrlList = imgStr.Trim().Split(',').ToList();
                                        }
                                        else
                                        {
                                            nimgUrlList.Add(imgStr);
                                        }
                                    }
                                    if (nimgUrlList != null && nimgUrlList.Count > 0)
                                    {
                                        arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                        nimgUrlList.All((Aarg) =>
                                        {
                                            string imageUri = ConfigService.BaseActivityImageURL + Aarg;
                                            arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = ImageSource.FromUri(new Uri(imageUri)), ActivityImageUrl = imageUri }); ;
                                            return true;
                                        });
                                    }
                                }
                                else
                                {
                                    arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                    arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = "noimage" });
                                }
                                //string arg.Emoji = "em em-smiley,em em-kissing_heart,em em-smirk,em em-slightly_frowning_face,em em-peace_symbol,em em-sunglasses,em em-sunglasses";
                                List<string> emojiList = new List<string>();
                                if (!string.IsNullOrEmpty(arg.Emoji))
                                {
                                    if (arg.Emoji.Contains(","))
                                    {
                                        emojiList = arg.Emoji.Split(',').ToList();
                                    }
                                    else
                                    {
                                        emojiList.Add(arg.Emoji);
                                    }
                                }
                                emojiList = emojiList.Select(x => x.Trim()).ToList();
                                ObservableCollection<EmojiDisplayModel> EmojisList = CommonUtility.EmojisList();
                                if (emojiList != null && emojiList.Count > 0)
                                {
                                    arg.EmojiList = new ObservableCollection<EmojiDisplayModel>();
                                    emojiList.All((Arg) =>
                                    {
                                        string EmojiCode = EmojisList.Where(x => x.EmojiName == Arg).Select(x => x.EmojiCode).FirstOrDefault();
                                        arg.EmojiList.Add(new EmojiDisplayModel() { EmojiCode = EmojiCode });
                                        return true;
                                    });
                                }
                                return true;
                            });
                            MyUpcomingActivitylist = new ObservableCollection<ActivityListModel>(tempList);
                        }

                        if (serviceResultBody.Data1 != null && serviceResultBody.Data1.Count > 0)
                        {
                            tempList1 = new ObservableCollection<ActivityListModel>(serviceResultBody.Data1);
                            tempList1.All((arg) =>
                            {
                                arg.IsPastActivity = true;
                                if (!string.IsNullOrEmpty(arg.proBackgroundImage))
                                {
                                    string imgStr = arg.proBackgroundImage;
                                    List<string> nimgUrlList = new List<string>();
                                    if (!string.IsNullOrEmpty(imgStr))
                                    {
                                        if (imgStr.Contains(","))
                                        {
                                            nimgUrlList = imgStr.Split(',').ToList();
                                        }
                                        else
                                        {
                                            nimgUrlList.Add(imgStr);
                                        }
                                    }
                                    if (nimgUrlList != null && nimgUrlList.Count > 0)
                                    {
                                        arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                        nimgUrlList.All((Aarg) =>
                                        {
                                            string imageUri = ConfigService.BaseActivityImageURL + Aarg;
                                            arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = ImageSource.FromUri(new Uri(imageUri)), ActivityImageUrl = imageUri }); ;
                                            return true;
                                        });
                                    }
                                }
                                else
                                {
                                    arg.ActivityCarouselList = new ObservableCollection<ActivityCarouselListModel>();
                                    arg.ActivityCarouselList.Add(new ActivityCarouselListModel() { ActivityImage = "noimage" });
                                }
                                return true;
                            });
                            tempList1[0].ShowPastActivity = true;
                        };
                        if (tempList1 != null)
                        {
                            foreach (var item in tempList1)
                            {
                                MyUpcomingActivitylist.Add(item);
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
            //finally
            //{
            //    IsBusyInActivityBinding = false;
            //}
        }
        async Task GotoCreateActivity()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreateActivityPage), null);
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
            if (SessionService.isFromJoinActivity)
            {
                SessionService.isFromJoinActivity = false;
                SessionService.IsShowActivitiesIntial = true;
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await GetMyActivityList();
        }
    }
}