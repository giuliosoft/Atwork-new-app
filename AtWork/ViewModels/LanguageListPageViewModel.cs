using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

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
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveDetail(obj)); } }
        public DelegateCommand<Language> SelectionChangedCommand { get { return new DelegateCommand<Language>(async (obj) => await OnSelectionChangedAsync(obj)); } }
        public DelegateCommand SelectedLanguageCommand { get { return new DelegateCommand(async () => await SelectedLanguage()); } }
        #endregion

        #region private methods
        async Task SaveDetail(string str)
        {
            try
            {
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task SelectedLanguage()
        {
            try
            {
                //Selectedlanguage
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
                LanguageName = new List<string>();
                LanguageName.Add("English");
                LanguageName.Add("German");
                LanguageName.Add("French");
                LanguageName.Add("Italian");

                LanguageList = new ObservableCollection<Language>();
                LanguageName.All((arg) =>
                {
                    if (arg == "English")
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


