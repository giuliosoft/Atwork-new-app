using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private string _Prop = string.Empty;
        private bool _ShowChooseLanguage = false;
        #endregion

        #region Public Properties
        public string Selectedlanguage;
        public List<string> LanguageName { get; set; }
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

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
                SaveUserLanguageDetail(Selectedlanguage);
                await _navigationService.NavigateAsync(nameof(ChangeProfilePicturePage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
            try
            {
                //LanguageName = new List<string>();
                //LanguageName.Add("English");
                //LanguageName.Add("German");
                //LanguageName.Add("French");
                //LanguageName.Add("Italian");

                await GetLanguages();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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


