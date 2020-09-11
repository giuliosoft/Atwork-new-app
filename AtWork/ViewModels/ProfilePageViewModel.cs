using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public ProfilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.ProfileText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
        }
        public string Name { get; } = SettingsService.VolunteersUserData.FullName;
        #region Commands
        public DelegateCommand SettingCommand { get { return new DelegateCommand(async () => await Setting()); } }
        public DelegateCommand LogoutCommand { get { return new DelegateCommand(async () => await Logout()); } }
        #endregion
        async Task Setting()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(SettingsPage));
            }
            catch (Exception ex)
            {

            }
        }
        async Task Logout()
        {
            try
            {
                SessionService.CurrentTab = 0;
                SessionService.IsWelcomeSetup = false;
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartUpPage)}", null);
            }
            catch (Exception ex)
            {

            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
