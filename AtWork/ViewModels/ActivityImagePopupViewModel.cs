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
    public class ActivityImagePopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<string> SelectedImageSourceEvent;
        #region Constructor
        public ActivityImagePopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
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
        //public DelegateCommand<string> ProfileOptionSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await ProfileOptionSelected(obj)); } }
        //public DelegateCommand ProfileCloseCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        //public DelegateCommand GoForCancelCommand { get { return new DelegateCommand(async () => await GoForCancel()); } }

        public DelegateCommand SelectActivityImage1Command { get { return new DelegateCommand(async () => await SelectActivityImage1()); } }
        public DelegateCommand SelectActivityImage2Command { get { return new DelegateCommand(async () => await SelectActivityImage2()); } }
        public DelegateCommand SelectActivityImage3Command { get { return new DelegateCommand(async () => await SelectActivityImage3()); } }

        #endregion

        #region Private Methods
        //async Task ProfileOptionSelected(string selectedProfileOption)
        //{
        //    try
        //    {
        //        await PopupNavigationService.ClosePopup(true);
        //        ProfileSelectedEvent?.Invoke(this, selectedProfileOption);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        async Task SelectActivityImage1()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                SelectedImageSourceEvent?.Invoke(this, "activitydefault1");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task SelectActivityImage2()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                SelectedImageSourceEvent?.Invoke(this, "activitydefault2");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task SelectActivityImage3()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                SelectedImageSourceEvent?.Invoke(this, "activitydefault3");
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
