using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    public class YourFeedbackPageViewModel : ViewModelBase
    {
        #region Constructor
        public YourFeedbackPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.YourFeedbackText;

        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
      
        #endregion

        #region Public Properties
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        private ObservableCollection<FeedBackUIModel> _feedBackList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> FeedBackList
        {
            get { return _feedBackList; }
            set { SetProperty(ref _feedBackList, value); }
        }
        private ObservableCollection<FeedBackUIModel> _FeedBackListImproveList = new ObservableCollection<FeedBackUIModel>();
        public ObservableCollection<FeedBackUIModel> FeedBackListImproveList
        {
            get { return _FeedBackListImproveList; }
            set { SetProperty(ref _FeedBackListImproveList, value); }
        }
      
        #endregion

        #region Commands
        public DelegateCommand<FeedBackUIModel> SelectedTextCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await SelectedText(obj)); } }
        public DelegateCommand<FeedBackUIModel> SelectedImproveTextCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await SelectedImproveText(obj)); } }
        public DelegateCommand GoToMyActivityCommand { get { return new DelegateCommand(async () => await GoToMyActivity()); } }

        #endregion

        #region private methods

        #endregion

        #region public methods
        public async Task SelectedText(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        item.IsSelected = !item.IsSelected;
                    }
                    return true;
                });
               
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        public async Task SelectedImproveText(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackListImproveList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        item.IsSelected = !item.IsSelected;
                    }
                    return true;
                });

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        public async Task GoToMyActivity()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(MyActivityPage), null);

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
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
            FeedBackList = new ObservableCollection<FeedBackUIModel>();
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.HappyText, IsSelected = true });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.CuriousText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.FullfilledText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.TiredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.BoredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ExcitedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.AnnoyedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ProudText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SuprisedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SatisfiedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.InterestedText, IsSelected = false });

            FeedBackListImproveList = new ObservableCollection<FeedBackUIModel>();
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EventOrganizationText, IsSelected = true });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.TransportationText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.FoodAndBeverageText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.SecurityText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.NGOPartnersText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.CommunicationText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EverythingWasSatisfactoryText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.OtherText, IsSelected = false });

            
        }
        
    }
}
