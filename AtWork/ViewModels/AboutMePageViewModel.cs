using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.UserSettingModel;

namespace AtWork.ViewModels
{
    public class AboutMePageViewModel : ViewModelBase
    {
        #region Constructor
        public AboutMePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(AboutMePage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;

            if (SessionService.IsWelcomeSetup)
            {
                HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
                SessionService.CurrentTab = 2;
                ShowHeadingText = true;
            }
            else
            {
                HeaderView = (ControlTemplate)App.Current.Resources["AddNewsPostHeader_Template"];
                ShowHeadingText = false;
            }
        }
        #endregion

        #region Private Properties
        private ControlTemplate _Header;
        private bool _ShowHeadingText;
        private string _AboutUserText = string.Empty;
        #endregion

        #region Public Properties        
        public ControlTemplate HeaderView
        {
            get { return _Header; }
            set { SetProperty(ref _Header, value); }
        }
        public bool ShowHeadingText
        {
            get { return _ShowHeadingText; }
            set { SetProperty(ref _ShowHeadingText, value); }
        }

        public string AboutUserText
        {
            get { return _AboutUserText; }
            set { SetProperty(ref _AboutUserText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveUserAboutDetail(obj)); } }
        public DelegateCommand DoneCommand { get { return new DelegateCommand(async () => await DoneButton()); } }
        #endregion

        #region private methods
        async Task GetAboutUserDetail()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetAboutUserInfo();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            AboutUserText = serviceBody.Data as string;
                        }
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task SaveUserAboutDetail(string str)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!SessionService.IsWelcomeSetup)
                    await ShowLoader();
                Volunteers inputModel = new Volunteers();
                inputModel = SettingsService.VolunteersUserData;
                inputModel.volAbout = AboutUserText;
                var serviceResult = await UserServices.UpdateAboutUserInfo(inputModel);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            if (!SessionService.IsWelcomeSetup)
                                await _navigationService.GoBackAsync();
                        }
                    }
                }
                if (!SessionService.IsWelcomeSetup)
                    await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task DoneButton()
        {
            try
            {
                await SaveUserAboutDetail(AboutUserText);
                await _navigationService.NavigateAsync(nameof(YourInterestsPage));
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion

        #region public methods

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (!SessionService.IsWelcomeSetup)
                await GetAboutUserDetail();
        }
    }
}


