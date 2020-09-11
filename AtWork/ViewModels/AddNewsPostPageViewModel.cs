using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using static AtWork.Models.NewsModel;

namespace AtWork.ViewModels
{
    public class AddNewsPostPageViewModel : ViewModelBase
    {
        #region Constructor
        public AddNewsPostPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(AddNewsPostPage);
            AddNewsCancelImage = AppResources.Cancel;
            AddNewsNextImage = AppResources.NextButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        private string _ProductDetail = string.Empty;
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
        private string _NewsTitle = string.Empty;
        public string NewsTitle
        {
            get { return _NewsTitle; }
            set { SetProperty(ref _NewsTitle, value); }
        }
        private string _NewsDescription = string.Empty;
        public string NewsDescription
        {
            get { return _NewsDescription; }
            set { SetProperty(ref _NewsDescription, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
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
                if (string.IsNullOrEmpty(NewsTitle) || string.IsNullOrEmpty(NewsDescription))
                {
                    await DisplayAlertAsync(AppResources.PostDataAlertText);
                    return;
                }

                await ShowLoader();
                SessionService.NewsPostInputData.newsTitle = NewsTitle;
                SessionService.NewsPostInputData.newsContent = NewsDescription;
                await _navigationService.NavigateAsync(nameof(AddNewsPostImagePage));
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

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (SessionService.isEditingNews)
            {
                NewsTitle = SessionService.NewsPostInputData.newsTitle;
                NewsDescription = SessionService.NewsPostInputData.newsContent;
            }
        }
    }
}
