using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using DLToolkit.Forms.Controls;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class NewsOptionPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<string> ProfileSelectedEvent;
        #region Constructor
        public NewsOptionPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
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
        #endregion
    }
}
