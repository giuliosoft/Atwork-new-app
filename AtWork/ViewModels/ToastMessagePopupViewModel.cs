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
        public EventHandler<string> ProfileSelectedEvent;
        public EventHandler<object> EditNewsEvent;
        public EventHandler<object> DeleteNewsEvent;
        #region Constructor
        public ToastMessagePopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        private bool _IsVisibleGuestOptions = true;
        public bool IsVisibleGuestOptions
        {
            get { return _IsVisibleGuestOptions; }
            set { SetProperty(ref _IsVisibleGuestOptions, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> ProfileOptionSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await ProfileOptionSelected(obj)); } }
        public DelegateCommand ProfileCloseCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand CopyTestCommand { get { return new DelegateCommand(async () => await CopyTest()); } }

        #endregion

        #region Private Methods
        async Task ProfileOptionSelected(string selectedProfileOption)
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                ProfileSelectedEvent?.Invoke(this, selectedProfileOption);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task CloseProfile()
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


