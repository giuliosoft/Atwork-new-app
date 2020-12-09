using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class ManageNotificationActivePageViewModel : ViewModelBase
    {
        #region Constructor
        public ManageNotificationActivePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            HeaderDetailsTitle = AppResources.ManageNotificationText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand ConnectCommand { get { return new DelegateCommand(async () => await OpenConnectPage()); } }
        public DelegateCommand ActiveCommand { get { return new DelegateCommand(async () => await OpenActivePage()); } }
        #endregion

        #region private methods
        async Task OpenConnectPage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ManageNotificationConnectPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task OpenActivePage()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ManageNotificationConnectPage));
            }
            catch (Exception ex)
            {
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}

