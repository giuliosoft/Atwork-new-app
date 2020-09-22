using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Constructor
        public SettingsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.SettingsText;
            SetSettingListData();
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
        }
        #endregion

        #region Private Properties
        private ObservableCollection<string> _SettingsList = new ObservableCollection<string>();
        #endregion

        #region Public Properties        
        public ObservableCollection<string> SettingsList
        {
            get { return _SettingsList; }
            set { SetProperty(ref _SettingsList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> SelectionChangedCommand { get { return new DelegateCommand<string>(async (obj) => await OnSelectionChangedAsync(obj)); } }
        #endregion

        #region Private Methods
        private async Task OnSelectionChangedAsync(string item)
        {
            try
            {
                if (item == AppResources.ChangeProfilePictureText)
                {
                    await _navigationService.NavigateAsync(nameof(ChangeProfilePicturePage));
                }
                else if (item == AppResources.EditInterestsText)
                {
                    await _navigationService.NavigateAsync(nameof(YourInterestsPage));
                }
                else if (item == AppResources.EditAboutMeText)
                {
                    await _navigationService.NavigateAsync(nameof(AboutMePage));
                }
                else if (item == AppResources.ChangeLanguageText)
                {
                    await _navigationService.NavigateAsync(nameof(LanguageListPage));
                }
                else if (item == AppResources.ChangePasswordText)
                {
                    await _navigationService.NavigateAsync(nameof(ChangeUserPasswordPage));
                }
                else if (item == AppResources.ManageNotificationText)
                {
                    await DisplayAlertAsync(AppResources.AllowNotificationsTitleText, AppResources.AllowNotificationsMsgText, AppResources.DontAllowText, AppResources.AllowText);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task SetSettingListData()
        {
            try
            {
                SettingsList = new ObservableCollection<string>();
                SettingsList.Add(AppResources.ChangeProfilePictureText);
                SettingsList.Add(AppResources.EditInterestsText);
                SettingsList.Add(AppResources.EditAboutMeText);
                SettingsList.Add(AppResources.ChangeLanguageText);
                SettingsList.Add(AppResources.ChangePasswordText);
                //SettingsList.Add(AppResources.ManageNotificationText);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                await SetSettingListData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
