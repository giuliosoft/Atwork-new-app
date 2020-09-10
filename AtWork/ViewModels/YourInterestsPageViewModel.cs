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
    public class YourInterestsPageViewModel : ViewModelBase
    {
        #region Constructor
        public YourInterestsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.YourFeedbackText;

        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _Rate1IsVisible = true;
        private bool _Rate2IsVisible = true;
        private bool _Rate3IsVisible = true;
        private bool _Rate4IsVisible = true;
        private bool _Rate5IsVisible = true;
        private double _RatingPancakeWidth = 0;
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

        public double RatingPancakeWidth
        {
            get { return _RatingPancakeWidth; }
            set { SetProperty(ref _RatingPancakeWidth, value); }
        }

        public bool Rate1IsVisible
        {
            get { return _Rate1IsVisible; }
            set { SetProperty(ref _Rate1IsVisible, value); }
        }

        public bool Rate2IsVisible
        {
            get { return _Rate2IsVisible; }
            set { SetProperty(ref _Rate2IsVisible, value); }
        }

        public bool Rate3IsVisible
        {
            get { return _Rate3IsVisible; }
            set { SetProperty(ref _Rate3IsVisible, value); }
        }

        public bool Rate4IsVisible
        {
            get { return _Rate4IsVisible; }
            set { SetProperty(ref _Rate4IsVisible, value); }
        }

        public bool Rate5IsVisible
        {
            get { return _Rate5IsVisible; }
            set { SetProperty(ref _Rate5IsVisible, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<FeedBackUIModel> ActivityFeelingSelectedCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await ActivityFeelingSelected(obj)); } }
        #endregion

        #region private methods

        async Task ActivityFeelingSelected(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackList.All((item) =>
                {
                    if (item == selectedTab)
                        FeedBackList.Remove(item);
                    return true;
                });

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        //async Task ActivityImprovementSelected(FeedBackUIModel selectedTab)
        //{
        //    try
        //    {
        //        FeedBackListImproveList.All((item) =>
        //        {
        //            if (item == selectedTab)
        //            {
        //                item.IsSelected = !item.IsSelected;
        //            }
        //            return true;
        //        });

        //    }
        //    catch (Exception exception)
        //    {
        //        Debug.WriteLine(exception.Message);
        //    }
        //}
        #endregion

        #region public methods        
        //public async Task GoToMyActivity()
        //{
        //    try
        //    {
        //        await _navigationService.NavigateAsync(nameof(MyActivityPage), null);

        //    }
        //    catch (Exception exception)
        //    {
        //        Debug.WriteLine(exception.Message);
        //    }
        //}
        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            FeedBackList = new ObservableCollection<FeedBackUIModel>();
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.HappyText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.CuriousText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.FullfilledText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.TiredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.BoredText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ExcitedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = "Long text Text one", IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.ProudText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SuprisedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.SatisfiedText, IsSelected = false });
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.InterestedText, IsSelected = false });

            //FeedBackListImproveList = new ObservableCollection<FeedBackUIModel>();
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EventOrganizationText, IsSelected = true });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.TransportationText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.FoodAndBeverageText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.SecurityText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.NGOPartnersText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.CommunicationText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EverythingWasSatisfactoryText, IsSelected = false });
            //FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.OtherText, IsSelected = false });
        }
    }
}
