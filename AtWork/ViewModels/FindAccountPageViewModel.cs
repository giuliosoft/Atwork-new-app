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
    public class FindAccountPageViewModel : ViewModelBase
    {
        #region Constructor
        public FindAccountPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.BackButtonText;
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
        public DelegateCommand GoForSubmitCommand { get { return new DelegateCommand(async () => await GoForSubmit()); } }
        #endregion

        #region private methods
        async Task GoForSubmit()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ClaimProfilePage));
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
