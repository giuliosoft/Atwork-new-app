using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ToastMessagePopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        #region Constructor
        public ToastMessagePopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private string _ToastText = string.Empty;
        #endregion

        #region Public Properties        
        public string ToastText
        {
            get { return _ToastText; }
            set { SetProperty(ref _ToastText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand CopyTestCommand { get { return new DelegateCommand(async () => await CopyTest()); } }
        #endregion

        #region Private Methods
        async Task CopyTest()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}


