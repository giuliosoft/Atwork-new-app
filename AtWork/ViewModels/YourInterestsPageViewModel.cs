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
    public class YourInterestsPageViewModel : ViewModelBase
    {
        #region Constructor
        public YourInterestsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.Cancel;
            HeaderDetailsTitle = AppResources.MyInterests;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = SaveCommand;

            IsFromWelcomeSetup = SessionService.IsWelcomeSetup;
            if (SessionService.IsWelcomeSetup)
            {
                HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
                SessionService.CurrentTab = 3;
                AddNewsCancelImage = AppResources.BackButtonText;
                ShowHeadingText = true;
            }
            else
            {
                HeaderView = (ControlTemplate)App.Current.Resources["AddNewsPostHeader_Template"];
                ShowHeadingText = false;
            }
        }
        #endregion

        #region Private Properties
        private string _commentText = string.Empty;
        private bool _SendButtonIsVisible = false;
        private bool _ShowHeadingText;
        private ControlTemplate _Header;
        private bool IsFromWelcomeSetup = false;
        #endregion

        #region Public Properties
        public bool SendButtonIsVisible
        {
            get { return _SendButtonIsVisible; }
            set { SetProperty(ref _SendButtonIsVisible, value); }
        }
        public string CommentText
        {
            get { return _commentText; }
            set { SetProperty(ref _commentText, value); }
        }
        public ControlTemplate HeaderView
        {
            get { return _Header; }
            set { SetProperty(ref _Header, value); }
        }
        private ObservableCollection<FeedBackUIModel> _feedBackList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> FeedBackList
        {
            get { return _feedBackList; }
            set { SetProperty(ref _feedBackList, value); }
        }
        private ObservableCollection<FeedBackUIModel> _FeedBackListImproveList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> FeedBackListImproveList
        {
            get { return _FeedBackListImproveList; }
            set { SetProperty(ref _FeedBackListImproveList, value); }
        }
        public bool ShowHeadingText
        {
            get { return _ShowHeadingText; }
            set { SetProperty(ref _ShowHeadingText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<FeedBackUIModel> ActivityFeelingSelectedCommand { get { return new DelegateCommand<FeedBackUIModel>((obj) => ActivityFeelingSelected(obj)); } }
        public DelegateCommand SendCommentCommand { get { return new DelegateCommand(async () => await AddComment()); } }
        public DelegateCommand DoneCommand { get { return new DelegateCommand(async () => await DoneButton()); } }

        public DelegateCommand<string> SaveCommand { get { return new DelegateCommand<string>(async (obj) => await SaveUserInterestsDetail(obj)); } }
        #endregion

        #region private methods
        async Task AddComment()
        {
            try
            {
                FeedBackUIModel feedBackUIModel = new FeedBackUIModel();
                feedBackUIModel.Title = CommentText;
                FeedBackList.Add(feedBackUIModel);
                CommentText = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
            finally
            {
                await ClosePopup();
            }
        }
        void ActivityFeelingSelected(FeedBackUIModel selectedTab)
        {
            try
            {
                var retVal = true;
                var tempInterestList = FeedBackList;
                FeedBackList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        tempInterestList.Remove(item);
                        retVal = false;
                    }
                    return retVal;
                });
                FeedBackList = new ObservableCollection<FeedBackUIModel>(tempInterestList);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task DoneButton()
        {
            try
            {
                await SaveUserInterestsDetail("");
                await _navigationService.NavigateAsync(nameof(WelcomeSetupDonePage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task GetInterests()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetInterestsDetails();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        if (!string.IsNullOrEmpty(serviceResultBody.Data as string))
                        {
                            string strinterests = serviceResultBody.Data as string;
                            List<string> lstInterests = new List<string>();
                            if (!string.IsNullOrEmpty(strinterests))
                            {
                                if (strinterests.Contains(","))
                                {
                                    lstInterests = strinterests.Split(',').ToList();
                                }
                                else
                                {
                                    lstInterests.Add(strinterests);
                                }
                            }
                            if (lstInterests != null && lstInterests.Count > 0)
                            {
                                FeedBackList = new ObservableCollection<FeedBackUIModel>();
                                lstInterests.All((arg) =>
                                {
                                    FeedBackList.Add(new FeedBackUIModel() { Title = arg.Trim(), IsSelected = false });
                                    return true;
                                });
                            }
                        }
                    }
                }
                await ClosePopup();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task SaveUserInterestsDetail(string str)
        {
            try
            {
                string selectedInterests = string.Empty;
                foreach (var item in FeedBackList)
                {
                    if (string.IsNullOrEmpty(selectedInterests))
                    {
                        selectedInterests = item.Title;
                    }
                    else
                    {
                        selectedInterests = selectedInterests + ", " + item.Title;
                    }
                }

                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!IsFromWelcomeSetup)
                    await ShowLoader();
                Volunteers Input = new Volunteers();
                Input.volInterests = selectedInterests;
                if (IsFromWelcomeSetup)
                {
                    Input.volOnBoardStatus = "complete";
                    Input.volStatus = "active";
                }
                var serviceResult = await UserServices.UpdateInterestsDetail(Input);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        if (IsFromWelcomeSetup)
                        {
                            Volunteers volunteersTemp = new Volunteers();
                            volunteersTemp = SettingsService.VolunteersUserData;
                            volunteersTemp.volOnBoardStatus = "complete";
                            volunteersTemp.volStatus = "active";
                            SettingsService.VolunteersUserData = volunteersTemp;
                        }
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            if (!IsFromWelcomeSetup)
                                await _navigationService.GoBackAsync();
                        }
                    }
                }
                if (!IsFromWelcomeSetup)
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
            if (!SessionService.IsWelcomeSetup)
                await GetInterests();
        }
    }
}
