using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
using static AtWork.Models.UserModel;
using static AtWork.Models.UserSettingModel;

namespace AtWork.ViewModels
{
    public class LanguageListPageViewModel : ViewModelBase
    {
        #region Constructor
        public LanguageListPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(LanguageListPage);
            HeaderDetailsTitle = AppResources.Language;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            if (SessionService.IsWelcomeSetup)
            {
                HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
                SessionService.CurrentTab = 0;
                AddNewsCancelImage = AppResources.BackButtonText;
                ShowChooseLanguage = true;
            }
            else
            {
                HeaderView = (ControlTemplate)App.Current.Resources["AddNewsPostHeader_Template"];
                AddNewsCancelImage = AppResources.Cancel;
                ShowChooseLanguage = false;
            }

        }
        #endregion

        #region Private Properties
        private ObservableCollection<Language> _LanguageList = new ObservableCollection<Language>();
        private ControlTemplate _Header;
        private bool _ShowChooseLanguage = false;
        private string _ChooseLanguageButton = AppResources.ChooseLanguage;
        #endregion

        #region Public Properties
        public string ChooseLanguageButton
        {
            get { return _ChooseLanguageButton; }
            set { SetProperty(ref _ChooseLanguageButton, value); }
        }
        public string Selectedlanguage;
        public List<string> LanguageName { get; set; }

        public ControlTemplate HeaderView
        {
            get { return _Header; }
            set { SetProperty(ref _Header, value); }
        }
        public ObservableCollection<Language> LanguageList
        {
            get { return _LanguageList; }
            set { SetProperty(ref _LanguageList, value); }
        }

        public bool ShowChooseLanguage
        {
            get { return _ShowChooseLanguage; }
            set { SetProperty(ref _ShowChooseLanguage, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveUserLanguageDetail(obj)); } }
        public DelegateCommand<Language> SelectionChangedCommand { get { return new DelegateCommand<Language>(async (obj) => await OnSelectionChangedAsync(obj)); } }
        public DelegateCommand SelectedLanguageCommand { get { return new DelegateCommand(async () => await SelectedLanguage()); } }
        #endregion

        #region private methods

        async Task SelectedLanguage()
        {
            try
            {
                LanguageService.Init(Selectedlanguage);
                SaveUserLanguageDetail(Selectedlanguage);
                await _navigationService.NavigateAsync(nameof(ChangeProfilePicturePage), null);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task OnSelectionChangedAsync(Language item)
        {
            try
            {
                Selectedlanguage = item.Name;
                LanguageList.Clear();
                LanguageName.All((arg) =>
                {
                    if (arg == item.Name)
                    {
                        Selectedlanguage = arg;
                        LanguageList.Add(new Language() { Name = arg, TextColor = (Color)App.Current.Resources["OffWhiteColor"], BackGroundColour = (Color)App.Current.Resources["AccentColor"] }); ;
                    }
                    else
                    {
                        LanguageList.Add(new Language() { Name = arg, TextColor = (Color)App.Current.Resources["AccentColor"], BackGroundColour = (Color)App.Current.Resources["OffWhiteColor"] }); ;
                    }
                    return true;
                });
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task GetLanguages()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetUserlanguage();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<LanguageResponse>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        if (serviceResultBody.Data != null && serviceResultBody.Data.Count > 0)
                        {
                            LanguageName = serviceResultBody.Data;
                            string strCurrentSelectedLanguage = string.Empty;
                            if (!string.IsNullOrEmpty(serviceResultBody.Data1))
                            {
                                strCurrentSelectedLanguage = serviceResultBody.Data1;
                            }
                            LanguageList.Clear();
                            LanguageName.All((arg) =>
                            {
                                if (arg == strCurrentSelectedLanguage)
                                {
                                    Selectedlanguage = arg;
                                    LanguageList.Add(new Language() { Name = arg, TextColor = (Color)App.Current.Resources["OffWhiteColor"], BackGroundColour = (Color)App.Current.Resources["AccentColor"] }); ;
                                }
                                else
                                {
                                    LanguageList.Add(new Language() { Name = arg, TextColor = (Color)App.Current.Resources["AccentColor"], BackGroundColour = (Color)App.Current.Resources["OffWhiteColor"] }); ;
                                }
                                return true;
                            });

                        }
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task SaveUserLanguageDetail(string str)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!SessionService.IsWelcomeSetup)
                    await ShowLoader();
                Volunteers Input = new Volunteers();
                Input.volLanguage = Selectedlanguage;
                var serviceResult = await UserServices.UpdateUserLanguage(Input);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            if (!SessionService.IsWelcomeSetup)
                            {
                                await ChangeLanguage(Selectedlanguage, true);
                            }
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
            if (SessionService.IsWelcomeSetup)
            {
                ChooseLanguageButton = AppResources.ChooseLanguage;
                AddNewsCancelImage = AppResources.BackButtonText;
            }
            try
            {
                await GetLanguages();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
    public class Language
    {
        public string Name { get; set; }
        public Color BackGroundColour { get; set; }
        public Color TextColor { get; set; }

    }
}


