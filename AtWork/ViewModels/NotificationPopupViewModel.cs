using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.NotificationModel;

namespace AtWork.ViewModels
{
    public class NotificationPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;

        public EventHandler<string> SaveNotificationEvent;
        #region Constructor
        public NotificationPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            LanguageName = new List<string>() { AppResources.txtPauseFor30Min, AppResources.txtPauseFor1Hour, AppResources.txtPauseFor1day, AppResources.txtPauseFor1week, AppResources.txtPauseForForever };
            LanguageName.All((arg) =>
            {
                if (arg == SelectedTime)
                {
                    SelectedTime = arg;
                    LanguageList.Add(new NotificationType() { Name = arg, TextColor = (Color)App.Current.Resources["OffWhiteColor"], BackGroundColour = (Color)App.Current.Resources["AccentColor"] }); ;
                }
                else
                {
                    LanguageList.Add(new NotificationType() { Name = arg, TextColor = (Color)App.Current.Resources["AccentColor"], BackGroundColour = (Color)App.Current.Resources["OffWhiteColor"] }); ;
                }
                return true;
            });
        }
        #endregion

        #region Private Properties
        private ObservableCollection<NotificationType> _LanguageList = new ObservableCollection<NotificationType>();
        List<JoinActivityDatesModel> SelectedDatesToJoin = new List<JoinActivityDatesModel>();
        #endregion

        #region Public Properties
        public ObservableCollection<NotificationType> LanguageList
        {
            get { return _LanguageList; }
            set { SetProperty(ref _LanguageList, value); }
        }
        public string SelectedTime;
        public List<string> LanguageName = new List<string>();

        public string _txtTime;
        public string txtTime
        {
            get { return _txtTime; }
            set { SetProperty(ref _txtTime, value); }
        }


        #endregion

        #region Commands        
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await ClosePopup()); } }
        //public DelegateCommand SaveCommand { get { return new DelegateCommand(async () => await SaveNotification()); } }
        public DelegateCommand<NotificationType> SelectionChangedCommand { get { return new DelegateCommand<NotificationType>((obj) => OnSelectionChanged(obj)); } }
        #endregion

        #region Private Methods

        async void OnSelectionChanged(NotificationType item)
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                SaveNotificationEvent?.Invoke(this, item.Name);
                //SelectedTime = item.Name;
                //LanguageList.Clear();
                //LanguageName.All((arg) =>
                //{
                //    if (arg == item.Name)
                //    {
                //        SelectedTime = arg;
                //        LanguageList.Add(new NotificationType() { Name = arg, TextColor = (Color)App.Current.Resources["OffWhiteColor"], BackGroundColour = (Color)App.Current.Resources["AccentColor"], IsSelected = true }); ;
                //    }
                //    else
                //    {
                //        LanguageList.Add(new NotificationType() { Name = arg, TextColor = (Color)App.Current.Resources["AccentColor"], BackGroundColour = (Color)App.Current.Resources["OffWhiteColor"], IsSelected = false }); ;
                //    }
                //    return true;
                //});
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        public async Task ClosePopup()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                ClosePopupEvent.Invoke(this, true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task SaveNotification()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                SaveNotificationEvent?.Invoke(this, SelectedTime);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion
    }
    public class NotificationType
    {
        public string Name { get; set; }
        public Color BackGroundColour { get; set; }
        public Color TextColor { get; set; }
        public bool IsSelected { get; set; }

    }
   
}
