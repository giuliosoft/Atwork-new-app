using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.SubViews;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ActivityImagePopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        //public EventHandler<string> SelectedImageSourceEvent;
        public event Action<string,ImageSource> SelectedImageSourceEvent1;
        #region Constructor
        public ActivityImagePopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            try
            {
                if (SessionService.CreateActivityOurImages.Count >= 0)
                    image1 = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + SessionService.CreateActivityOurImages[0]));
                if (SessionService.CreateActivityOurImages.Count >= 1)
                {
                    image2 = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + SessionService.CreateActivityOurImages[1]));
                    ShowImage2 = true;
                }
                if (SessionService.CreateActivityOurImages.Count >= 2)
                {
                    image3 = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + SessionService.CreateActivityOurImages[2]));
                    ShowImage3 = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Private Properties
        private bool _IsVisibleGuestOptions = true;
        private bool _ShowImage2 = false;
        private bool _ShowImage3 = false;
        private ImageSource _Image1;
        private ImageSource _Image2;
        private ImageSource _Image3;

        #endregion

        #region Public Properties

        public bool IsVisibleGuestOptions
        {
            get { return _IsVisibleGuestOptions; }
            set { SetProperty(ref _IsVisibleGuestOptions, value); }
        }
        public bool ShowImage2
        {
            get { return _ShowImage2; }
            set { SetProperty(ref _ShowImage2, value); }
        }
        public bool ShowImage3
        {
            get { return _ShowImage3; }
            set { SetProperty(ref _ShowImage3, value); }
        }
        public ImageSource image1
        {
            get { return _Image1; }
            set { SetProperty(ref _Image1, value); }
        }
        public ImageSource image2
        {
            get { return _Image2; }
            set { SetProperty(ref _Image2, value); }
        }
        public ImageSource image3
        {
            get { return _Image3; }
            set { SetProperty(ref _Image3, value); }
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
       
        async Task SelectActivityImage1()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                //SelectedImageSourceEvent?.Invoke(this, SessionService.CreateActivityOurImages[0]);
                SelectedImageSourceEvent1?.Invoke(SessionService.CreateActivityOurImages[0], image1);
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
                //SelectedImageSourceEvent?.Invoke(this, SessionService.CreateActivityOurImages[1]);
                SelectedImageSourceEvent1?.Invoke(SessionService.CreateActivityOurImages[1], image2);
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
                //SelectedImageSourceEvent?.Invoke(this, SessionService.CreateActivityOurImages[2]);
                SelectedImageSourceEvent1?.Invoke(SessionService.CreateActivityOurImages[2],image3);

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
