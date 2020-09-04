using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class JoinActivityPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<object> JoinActivityEvent;
        #region Constructor
        public JoinActivityPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private bool _IsVisibleGuestOptions = true;
        private Color _Firstbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
        private Color _Secoundbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
        private Color _Thirdbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
        private Color _FirstTextColor = (Color)App.Current.Resources["WhiteColor"];
        private Color _SecoundTextColor = (Color)App.Current.Resources["WhiteColor"];
        private Color _ThirdTextColor = (Color)App.Current.Resources["WhiteColor"];
        #endregion

        #region Public Properties

        public bool IsVisibleGuestOptions
        {
            get { return _IsVisibleGuestOptions; }
            set { SetProperty(ref _IsVisibleGuestOptions, value); }
        }
        public Color Firstbgcolor
        {
            get { return _Firstbgcolor; }
            set { SetProperty(ref _Firstbgcolor, value); }
        }
        public Color Secoundbgcolor
        {
            get { return _Secoundbgcolor; }
            set { SetProperty(ref _Secoundbgcolor, value); }
        }
        public Color Thirdbgcolor
        {
            get { return _Thirdbgcolor; }
            set { SetProperty(ref _Thirdbgcolor, value); }
        }
        public Color FirstTextColor
        {
            get { return _FirstTextColor; }
            set { SetProperty(ref _FirstTextColor, value); }
        }
        public Color SecoundTextColor
        {
            get { return _SecoundTextColor; }
            set { SetProperty(ref _SecoundTextColor, value); }
        }
        public Color ThirdTextColor
        {
            get { return _ThirdTextColor; }
            set { SetProperty(ref _ThirdTextColor, value); }
        }
        #endregion

        #region Commands        
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand JoinActivityCommand { get { return new DelegateCommand(async () => await JoinActivity()); } }
        public DelegateCommand FirstDateSelectedCommand { get { return new DelegateCommand(async () => await FirstDateSelected()); } }
        public DelegateCommand SecoundDateSelectedCommand { get { return new DelegateCommand(async () => await SecoundDateSelected()); } }
        public DelegateCommand ThirdDateSelectedCommand { get { return new DelegateCommand(async () => await ThirdDateSelected()); } }

        #endregion

        #region Private Methods
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

        async Task JoinActivity()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                JoinActivityEvent.Invoke(this, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task FirstDateSelected()
        {
            try
            {
                Firstbgcolor = (Color)App.Current.Resources["WhiteColor"];
                Secoundbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                Thirdbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                FirstTextColor = (Color)App.Current.Resources["BlackColor"];
                SecoundTextColor = (Color)App.Current.Resources["WhiteColor"];
                ThirdTextColor = (Color)App.Current.Resources["WhiteColor"];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task SecoundDateSelected()
        {
            try
            {
                Firstbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                Secoundbgcolor = (Color)App.Current.Resources["WhiteColor"];
                Thirdbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                FirstTextColor = (Color)App.Current.Resources["WhiteColor"];
                SecoundTextColor = (Color)App.Current.Resources["BlackColor"];
                ThirdTextColor = (Color)App.Current.Resources["WhiteColor"];

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task ThirdDateSelected()
        {
            try
            {
                Firstbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                Secoundbgcolor = (Color)App.Current.Resources["DarkBrownColor"];
                Thirdbgcolor = (Color)App.Current.Resources["WhiteColor"];
                FirstTextColor = (Color)App.Current.Resources["WhiteColor"];
                SecoundTextColor = (Color)App.Current.Resources["WhiteColor"];
                ThirdTextColor = (Color)App.Current.Resources["BlackColor"];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
