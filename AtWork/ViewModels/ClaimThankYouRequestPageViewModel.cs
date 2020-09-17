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
    public class ClaimThankYouRequestPageViewModel : ViewModelBase
    {
        #region Constructor
        public ClaimThankYouRequestPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            //ClaimProfileBackCommand = HeaderBackCommand;
            BackCancelText = AppResources.BackButtonText;

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
        public DelegateCommand ContinueSetupCommand { get { return new DelegateCommand(async () => await ContinueSetup()); } }
        //public DelegateCommand<string> HeaderBackCommand { get { return new DelegateCommand<string>(async (obj) => await PageHeaderBack(obj)); } }
        public DelegateCommand BackButtonCommand { get { return new DelegateCommand(async () => await PageHeaderBack()); } }
        #endregion

        #region private methods
        async Task PageHeaderBack()
        {
            try
            {
                await BackClick();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task ContinueSetup()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreatePasswordPage), null);
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

