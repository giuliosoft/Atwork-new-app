using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ClaimThankYouRequestPageViewModel : ViewModelBase
    {
        #region Constructor
        public ClaimThankYouRequestPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

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
        #endregion

        #region Commands
        //public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand ContinueSetupCommand { get { return new DelegateCommand(async () => await ContinueSetup()); } }
        #endregion

        #region private methods
        //async Task GoForLogin()
        //{
        //    try


        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        private async Task ContinueSetup()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreatePasswordPage), null);
                //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(await _navigationService.NavigateAsync(nameof(CreatePasswordPage), null);)}", null);
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

