using System;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class ChangeProfilePicturePageViewModel : ViewModelBase
    {
        public ChangeProfilePicturePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.Cancel;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderDetailsTitle = AppResources.ProfilePictureText;
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
