using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class CreateActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public CreateActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(CreateActivityPage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.NextButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            EmojiList = new ObservableCollection<EmojiDisplayModel>();
            EmojiList = CommonUtility.EmojisList();
        }
        #endregion

        #region Private Properties
        List<EmojiDisplayModel> emojiDisplayModelsSelected = new List<EmojiDisplayModel>();
        private string _ActivityTitle = string.Empty;
        private string _ActivityDescription = string.Empty;
        private string _ActivityAddress = string.Empty;
        private string _ActivityCity = string.Empty;
        private string _ActivityCountry = string.Empty;
        private string _ActivityPrice = string.Empty;
        private DateTime _SelectedDate = DateTime.Now;
        private TimeSpan _SelectedTime = DateTime.Now.TimeOfDay;
        private TimeSpan _SelectedEndTime = DateTime.Now.TimeOfDay;
        ObservableCollection<EmojiDisplayModel> _EmojiList = new ObservableCollection<EmojiDisplayModel>();
        #endregion

        #region Public Properties        
        public string ActivityTitle
        {
            get { return _ActivityTitle; }
            set { SetProperty(ref _ActivityTitle, value); }
        }
        
        //public string ActivityDescription
        //{
        //    get { return _ActivityDescription; }
        //    set { SetProperty(ref _ActivityDescription, value); }
        //}
        public string ActivityAddress
        {
            get { return _ActivityAddress; }
            set { SetProperty(ref _ActivityAddress, value); }
        }
        public string ActivityCity
        {
            get { return _ActivityCity; }
            set { SetProperty(ref _ActivityCity, value); }
        }
        public string ActivityCountry
        {
            get { return _ActivityCountry; }
            set { SetProperty(ref _ActivityCountry, value); }
        }
        public string ActivityPrice
        {
            get { return _ActivityPrice; }
            set { SetProperty(ref _ActivityPrice, value); }
        }
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty(ref _SelectedDate, value); }
        }
        public TimeSpan SelectedTime
        {
            get { return _SelectedTime; }
            set { SetProperty(ref _SelectedTime, value); }
        }
        public TimeSpan SelectedEndTime
        {
            get { return _SelectedEndTime; }
            set { SetProperty(ref _SelectedEndTime, value); }
        }
        public ObservableCollection<EmojiDisplayModel> EmojiList
        {
            get { return _EmojiList; }
            set { SetProperty(ref _EmojiList, value); }
        }
        private bool _labelIsVisible { get; set; } = true; public bool LabelIsVisible { get { return _labelIsVisible; } set { _labelIsVisible = value; OnPropertyChanged(nameof(LabelIsVisible)); } } 
        public bool IsAddrerssPlaceHolderVisible
        {
            get => _labelIsVisible;
            set
            {
                _labelIsVisible = value;
                RaisePropertyChanged();
            }
        }
        //private string _address = string.Empty;
        public string ActivityDescription
        {
            get => _ActivityDescription;
            set
            {
                _ActivityDescription = value;
                if (value.Length > 0)
                {
                    IsAddrerssPlaceHolderVisible = false;
                }
                else
                {
                    IsAddrerssPlaceHolderVisible = true;
                }
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
        public DelegateCommand<EmojiDisplayModel> EmojiSelectionCommand { get { return new DelegateCommand<EmojiDisplayModel>(async (obj) => await EmojiSelection(obj)); } }
        #endregion

        #region private methods

        async Task EmojiSelection(EmojiDisplayModel selectedTab)
        {
            try
            {
                EmojiList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        item.IsSelected = !item.IsSelected;
                        if (item.IsSelected)
                        {
                            emojiDisplayModelsSelected.Add(item);
                        }
                        else
                        {
                            emojiDisplayModelsSelected.Remove(item);
                        }
                    }
                    return true;
                });

            }
            catch
            {

            }
        }
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                if (string.IsNullOrEmpty(ActivityTitle.Trim()) || string.IsNullOrEmpty(ActivityDescription.Trim()))
                {
                    await DisplayAlertAsync(AppResources.ActivityDataAlertText);
                    return;
                }

                await ShowLoader();
                SessionService.ActivityPostInputData.proTitle = ActivityTitle;
                SessionService.ActivityPostInputData.proDescription = ActivityDescription;
                SessionService.ActivityPostInputData.proAddress1 = ActivityAddress;
                SessionService.ActivityPostInputData.proCity = ActivityCity;
                SessionService.ActivityPostInputData.proCountry = ActivityCountry;
                SessionService.ActivityPostInputData.proCostCoveredEmployee = ActivityPrice;
                SessionService.ActivityPostInputData.proAddActivity_StartTime = SelectedTime.ToString("hh\\:mm");
                SessionService.ActivityPostInputData.proAddActivity_EndTime = SelectedEndTime.ToString("hh\\:mm");
                SessionService.ActivityPostInputData.proAddActivityDate = SelectedDate;
                try
                {
                    //List<string> lstEmoji = emojiDisplayModelsSelected.Where(x => x.IsSelected).ToList().Select(x => x.EmojiName).ToList();
                    List<string> lstEmoji = emojiDisplayModelsSelected.Select(x => x.EmojiName).ToList();
                    SessionService.SelectedEmojiForActivity = string.Join(", ", lstEmoji);
                }
                catch (Exception ex)
                {
                }
                
                
                var navigationParams = new NavigationParameters();
                navigationParams.Add("isFromActivity", true);
                await _navigationService.NavigateAsync(nameof(AddNewsPostImagePage), navigationParams);
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
            if (SessionService.isEditingActivity) 
            {
                try
                {
                    ActivityTitle = SessionService.ActivityPostInputData.proTitle;
                    ActivityDescription = SessionService.ActivityPostInputData.proDescription;
                    ActivityAddress = SessionService.ActivityPostInputData.proAddress1;
                    ActivityCity = SessionService.ActivityPostInputData.proCity;
                    ActivityCountry = SessionService.ActivityPostInputData.proCountry;
                    ActivityPrice = SessionService.ActivityPostInputData.proCostCoveredEmployee;
                    SelectedTime = TimeSpan.Parse(SessionService.ActivityPostInputData.proAddActivity_StartTime);
                    SelectedEndTime = TimeSpan.Parse(SessionService.ActivityPostInputData.proAddActivity_EndTime);
                    SelectedDate = Convert.ToDateTime(SessionService.ActivityPostInputData.proAddActivityDate);

                    string strEmoji = SessionService.ActivityPostInputData.Emoji;
                    List<string> SelectedEmojiList = new List<string>();
                    if (!string.IsNullOrEmpty(strEmoji))
                    {
                        if (strEmoji.Contains(","))
                        {
                            SelectedEmojiList = strEmoji.Trim().Split(',').ToList();
                        }
                        else
                        {
                            SelectedEmojiList.Add(strEmoji);
                        }
                        SelectedEmojiList = SelectedEmojiList.Select(x => x.Trim()).ToList();
                    }
                    ObservableCollection<EmojiDisplayModel> TempEmojiList = CommonUtility.EmojisList();
                    if (SelectedEmojiList != null && SelectedEmojiList.Count > 0)
                    {
                        var result = TempEmojiList.Where(x => SelectedEmojiList.Contains(x.EmojiName)).ToList();//.ForEach(f => f.IsSelected = true);
                        result.ForEach(x => x.IsSelected = true);
                        emojiDisplayModelsSelected = result; //new ObservableCollection<EmojiDisplayModel>(result as List<EmojiDisplayModel>); ;
                        EmojiList = TempEmojiList;
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}
