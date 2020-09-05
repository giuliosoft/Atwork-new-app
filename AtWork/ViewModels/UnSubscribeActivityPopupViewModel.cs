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
        public EventHandler<object> ConfirmEvent;
        #region Constructor
        public UnSubscribeActivityPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties

        #endregion

        #region Commands
        public DelegateCommand CloseConfirmPopupCommand { get { return new DelegateCommand(async () => await CloseClick()); } }
        public DelegateCommand ConfirmCommand { get { return new DelegateCommand(async () => await ConfirmAction()); } }

        #endregion

        #region Private Methods        
        async Task CloseClick()
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

        async Task ConfirmAction()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                ConfirmEvent.Invoke(this, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
