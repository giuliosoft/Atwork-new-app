using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        public DelegateCommand<FeedBackUIModel> ActivityFeelingSelectedCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await ActivityFeelingSelected(obj)); } }
        public DelegateCommand SendCommentCommand { get { return new DelegateCommand(async () => await AddComment()); } }
        public DelegateCommand DoneCommand { get { return new DelegateCommand(async () => await DoneButton()); } }
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
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                await ClosePopup();
            }
        }
        async Task ActivityFeelingSelected(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackList.All((item) =>
                {
                    if (item == selectedTab)
                        FeedBackList.Remove(item);
                    return true;
                });

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
                await _navigationService.NavigateAsync(nameof(WelcomeSetupDonePage));
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
            FeedBackList = new ObservableCollection<FeedBackUIModel>();
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.HappyText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.CuriousText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.FullfilledText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.TiredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.BoredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ExcitedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = "Long text Text one", IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ProudText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SuprisedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SatisfiedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.InterestedText, IsSelected = false });
        }
    }
}
