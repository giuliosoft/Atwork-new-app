using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class PostNewsPageViewModel : ViewModelBase
    {
        #region Constructor
        public PostNewsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(PostNewsPage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.PublishText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private Color _PublishGroupColor = (Color)App.Current.Resources["AccentColor"];
        private Color _PublishPublicColor = Color.Transparent;
        private string NewsPrivacy = string.Empty;
        private Color _GroupTextColor = Color.White;
        private Color _PublicTextColor = (Color)App.Current.Resources["AccentColor"];
        #endregion

        #region Public Properties        
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }

        public Color PublishGroupColor
        {
            get { return _PublishGroupColor; }
            set { SetProperty(ref _PublishGroupColor, value); }
        }

        public Color PublishPublicColor
        {
            get { return _PublishPublicColor; }
            set { SetProperty(ref _PublishPublicColor, value); }
        }

        public Color GroupTextColor
        {
            get { return _GroupTextColor; }
            set { SetProperty(ref _GroupTextColor, value); }
        }

        public Color PublicTextColor
        {
            get { return _PublicTextColor; }
            set { SetProperty(ref _PublicTextColor, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand PostToEverybodyCommand { get { return new DelegateCommand(async () => await PostToEverybody()); } }
        public DelegateCommand PostToYourGroupCommand { get { return new DelegateCommand(async () => await PostToYourGroup()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
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

        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();

                SessionService.NewsPostInputData.coUniqueID = SettingsService.LoggedInUserData.coUniqueID;
                NewsDetailModel_Input inputModel = new NewsDetailModel_Input();

                inputModel.coUniqueID = SessionService.NewsPostInputData.coUniqueID;
                inputModel.newsTitle = SessionService.NewsPostInputData.newsTitle;
                inputModel.newsContent = SessionService.NewsPostInputData.newsContent;
                inputModel.newsDateTime = DateTime.Now;
                inputModel.newsPostedTime = DateTime.Now;
                inputModel.newsFileOriginal = SessionService.NewsPostAttachmentFileName;
                inputModel.newsFile = "";
                inputModel.newsImage = "";
                inputModel.newsOrigin = "employee";
                inputModel.newsStatus = "Active";
                inputModel.newsPrivacy = NewsPrivacy;
                if (!string.IsNullOrEmpty(SessionService.NewsPostAttachmentFilePath))
                    SessionService.NewsPostImageFiles.Add(SessionService.NewsPostAttachmentFilePath);

                BaseResponse<string> serviceResult = null;
                if (SessionService.isEditingNews)
                {
                    inputModel.newsUniqueID = SessionService.NewsPostInputData.newsUniqueID;
                    inputModel.volUniqueID = SessionService.NewsPostInputData.volUniqueID;
                    if (SessionService.NewsPostCarouselImages != null)
                    {
                        inputModel.newsImage = String.Join(",", SessionService.NewsPostCarouselImages.Where((x) => !string.IsNullOrEmpty(x)).Select(x => x.Replace(ConfigService.BaseNewsImageURL, "")).ToList());
                    }
                    serviceResult = await NewsService.PostNewsFeedEdit(inputModel, SessionService.NewsPostImageFiles);
                }
                else
                {
                    inputModel.newsUniqueID = null;
                    inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                    serviceResult = await NewsService.PostNewsFeed(inputModel, SessionService.NewsPostImageFiles);
                }

                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    SessionService.NewsPostInputData = new NewsDetailModel_Input();
                    SessionService.NewsPostAttachmentFileName = string.Empty;
                    SessionService.NewsPostAttachmentFilePath = string.Empty;
                    SessionService.NewsPostImageFiles = new List<string>();
                    if (SessionService.isEditingNews)
                    {
                        SessionService.isEditingNews = false;
                        SessionService.IsNeedToRefreshNews = true;
                    }
                    await _navigationService.NavigateAsync(nameof(DashboardPage));
                }
                await ClosePopup();

            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
        }

        async Task PostToEverybody()
        {
            try
            {
                NewsPrivacy = "Everyone";
                PublishGroupColor = Color.Transparent;
                PublishPublicColor = (Color)App.Current.Resources["AccentColor"];
                GroupTextColor = (Color)App.Current.Resources["AccentColor"];
                PublicTextColor = Color.White;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task PostToYourGroup()
        {
            try
            {
                NewsPrivacy = "mygroup";
                PublishGroupColor = (Color)App.Current.Resources["AccentColor"];
                PublishPublicColor = Color.Transparent;
                PublicTextColor = (Color)App.Current.Resources["AccentColor"];
                GroupTextColor = Color.White;
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
            //AddNewsCancelImage = "Back";
            //AddNewsNextImage = "Publish";
        }
    }
}
