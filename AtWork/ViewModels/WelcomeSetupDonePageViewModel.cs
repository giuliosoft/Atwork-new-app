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
    public class WelcomeSetupDonePageViewModel : ViewModelBase
    {
        #region Constructor
        public WelcomeSetupDonePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.MyInterests;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
            AddNewsCancelImage = AppResources.BackButtonText;
            SessionService.CurrentTab = 4;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ControlTemplate _Header;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public ControlTemplate HeaderView
        {
            get { return _Header; }
            set { SetProperty(ref _Header, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
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
        async void OpenAlertDialouge()
        {
            try
            {
                SessionService.CurrentTab = 0;
                SessionService.IsWelcomeSetup = false;
                await Task.Delay(3000);
                await _navigationService.NavigateAsync(nameof(DashboardPage));
                //var res = await App.Current.MainPage.DisplayAlert(AppResources.AllowNotificationsTitleText, AppResources.AllowNotificationsMsgText, AppResources.DontAllowText, AppResources.AllowText);
                //if (res)
                //{

                //}
                //Task.Factory.StartNew(async () =>
                //{
                //    await Task.Delay(5000);
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        // Do something on the UI thread.
                //    });
                //});
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
            OpenAlertDialouge();
        }
    }
}


