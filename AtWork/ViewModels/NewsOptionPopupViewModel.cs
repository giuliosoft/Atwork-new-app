using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using DLToolkit.Forms.Controls;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class NewsOptionPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<string> ProfileSelectedEvent;
        public EventHandler<object> EditNewsEvent;
        public EventHandler<object> DeleteNewsEvent;
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
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand GoForEditNewsCommand { get { return new DelegateCommand(async () => await EditNews()); } }
        public DelegateCommand GoForDeleteNewsCommand { get { return new DelegateCommand(async () => await DeleteNews()); } }
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
        async Task EditNews()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                EditNewsEvent?.Invoke(this, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task DeleteNews()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                var result = await App.Current.MainPage.DisplayAlert(AppResources.DeletePostAlert, AppResources.DeleteCommentMessage, AppResources.Delete, AppResources.Cancel);
                if (result)
                {
                    DeleteNewsEvent?.Invoke(this, null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
