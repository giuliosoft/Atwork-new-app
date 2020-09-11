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
    public class AboutMePageViewModel : ViewModelBase
    {
        #region Constructor
        public AboutMePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(AboutMePage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;

            if (SessionService.IsWelcomeSetup)
            {
                HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
                SessionService.CurrentTab = 2;
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
        private string _Prop = string.Empty;
        private ControlTemplate _Header;
        private bool _ShowHeadingText;
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
        public bool ShowHeadingText
        {
            get { return _ShowHeadingText; }
            set { SetProperty(ref _ShowHeadingText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveDetail(obj)); } }
        public DelegateCommand DoneCommand { get { return new DelegateCommand(async () => await DoneButton()); } }
        #endregion

        #region private methods
        async Task SaveDetail(string str)
        {
            try
            {
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task DoneButton()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(YourInterestsPage));
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
        }
    }
}


