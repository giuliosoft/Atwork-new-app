using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.SettingsText;
            SettingsList = new ObservableCollection<string>();
            SettingsList.Add(AppResources.ChangeProfilePictureText);
            SettingsList.Add(AppResources.EditInterestsText);
            SettingsList.Add(AppResources.EditAboutMeText);
            SettingsList.Add(AppResources.ChangeLanguageText);
            SettingsList.Add(AppResources.ChangePasswordText);
            SettingsList.Add(AppResources.ManageNotificationText);
            SelectionChangedCommand = new DelegateCommand(async () => await OnSelectionChangedAsync());
        }
        public ObservableCollection<string> SettingsList { get; }
        public DelegateCommand SelectionChangedCommand { get; }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        private async Task OnSelectionChangedAsync()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
