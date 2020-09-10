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
            AddNewsCancelImage = AppResources.BackButtonText;
            HeaderDetailsTitle = AppResources.Language;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            
        }
        #endregion

        #region Private Properties
        private ObservableCollection<Language> _LanguageList = new ObservableCollection<Language>();
        
        private string _Prop = string.Empty;
        #endregion

        #region Public Properties
        public string Selectedlanguage;
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public ObservableCollection<Language> LanguageList
        {
            get { return _LanguageList; }
            set { SetProperty(ref _LanguageList, value); }
        }
        public List<string> LanguageName { get; set; }

        #endregion

        #region Commands
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveDetail(obj)); } }
        public DelegateCommand<Language> SelectionChangedCommand { get { return new DelegateCommand<Language>(async (obj) => await OnSelectionChangedAsync(obj)); } }
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


