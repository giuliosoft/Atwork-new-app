using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.SubViews;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class UnSubscribeActivityPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<string> ProfileSelectedEvent;
        #region Constructor
        public UnSubscribeActivityPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private bool _IsVisibleGuestOptions = true;

        #endregion

        #region Public Properties

        public bool IsVisibleGuestOptions
        {
            get { return _IsVisibleGuestOptions; }
            set { SetProperty(ref _IsVisibleGuestOptions, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand<string> ProfileOptionSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await ProfileOptionSelected(obj)); } }
        //public DelegateCommand ProfileCloseCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand GoForCancelCommand { get { return new DelegateCommand(async () => await GoForCancel()); } }

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
        async Task GoForCancel()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                await _navigationService.NavigateAsync(nameof(DashboardPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
