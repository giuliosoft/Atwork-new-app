using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class CreateActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public CreateActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            //NextClickPageName = nameof(AddNewsPostPage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.NextButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private string _ActivityTitle = string.Empty;
        private string _ActivityDescription = string.Empty;
        private string _ActivityAddress = string.Empty;
        private string _ActivityCity = string.Empty;
        private string _ActivityCountry = string.Empty;
        private string _ActivityPrice = string.Empty;
        private DateTime _SelectedDate = DateTime.Now;
        private TimeSpan _SelectedTime = DateTime.Now.TimeOfDay;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public string ActivityTitle
        {
            get { return _ActivityTitle; }
            set { SetProperty(ref _ActivityTitle, value); }
        }
        
        public string ActivityDescription
        {
            get { return _ActivityDescription; }
            set { SetProperty(ref _ActivityDescription, value); }
        }
        public string ActivityAddress
        {
            get { return _ActivityAddress; }
            set { SetProperty(ref _ActivityAddress, value); }
        }
        public string ActivityCity
        {
            get { return _ActivityCity; }
            set { SetProperty(ref _ActivityCity, value); }
        }
        public string ActivityCountry
        {
            get { return _ActivityCountry; }
            set { SetProperty(ref _ActivityCountry, value); }
        }
        public string ActivityPrice
        {
            get { return _ActivityPrice; }
            set { SetProperty(ref _ActivityPrice, value); }
        }
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty(ref _SelectedDate, value); }
        }
        public TimeSpan SelectedTime
        {
            get { return _SelectedTime; }
            set { SetProperty(ref _SelectedTime, value); }
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
                if (string.IsNullOrEmpty(ActivityTitle) || string.IsNullOrEmpty(ActivityDescription))
                {
                    await DisplayAlertAsync(AppResources.ActivityDataAlertText);
                    return;
                }
                else if (string.IsNullOrEmpty(ActivityAddress))
                {
                    await DisplayAlertAsync(AppResources.ActivityDataAlertText);
                    return;
                }
                else if (string.IsNullOrEmpty(ActivityCity))
                {
                    await DisplayAlertAsync(AppResources.ActivityCityAlertText);
                    return;
                }
                else if (string.IsNullOrEmpty(ActivityCountry))
                {
                    await DisplayAlertAsync(AppResources.ActivityCountryAlertText);
                    return;
                }

                await ShowLoader();
                //SessionService.NewsPostInputData.newsTitle = NewsTitle;
                //SessionService.NewsPostInputData.newsContent = NewsDescription;
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
        }
    }
}
