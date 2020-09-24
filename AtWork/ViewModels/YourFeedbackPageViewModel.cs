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
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.FeedbackModel;

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
        ActivityListModel _SelectedPastActivity;
        private int OverallExperienceRatingValue = 0;
        private double ImpactRatingValue = 0;
        private double RecommendRatingValue = 0;
        List<FeedBackUIModel> SelectedActivityFeelingList = new List<FeedBackUIModel>();
        List<FeedBackUIModel> SelectedImprovementList = new List<FeedBackUIModel>();
        private string _FeedbackComments = string.Empty;
        private string _FeedbackLikeMostComment = string.Empty;
        private string _FeedbackAdditionalComments = string.Empty;

        private double _RatingPancakeWidth = 0;
        private CornerRadius _RatingPancakeCornerRadius = 25;

        private bool _Rate1FilledIsVisible = false;
        private bool _Rate1BorderedIsVisible = true;
        private bool _Rate1PathIsVisible = false;
        private bool _Rate2FilledIsVisible = false;
        private bool _Rate2BorderedIsVisible = true;
        private bool _Rate2PathIsVisible = false;
        private bool _Rate3FilledIsVisible = false;
        private bool _Rate3BorderedIsVisible = true;
        private bool _Rate3PathIsVisible = false;
        private bool _Rate4FilledIsVisible = false;
        private bool _Rate4BorderedIsVisible = true;
        private bool _Rate4PathIsVisible = false;
        private bool _Rate5FilledIsVisible = false;
        private bool _Rate5BorderedIsVisible = true;
        private bool _Rate5PathIsVisible = false;

        private double _ImpactRatingPancakeWidth = 0;
        private CornerRadius _ImpactRatingPancakeCornerRadius = 25;

        private bool _ImpactRate1FilledIsVisible = false;
        private bool _ImpactRate1BorderedIsVisible = true;
        private bool _ImpactRate1PathIsVisible = false;
        private bool _ImpactRate2FilledIsVisible = false;
        private bool _ImpactRate2BorderedIsVisible = true;
        private bool _ImpactRate2PathIsVisible = false;
        private bool _ImpactRate3FilledIsVisible = false;
        private bool _ImpactRate3BorderedIsVisible = true;
        private bool _ImpactRate3PathIsVisible = false;
        private bool _ImpactRate4FilledIsVisible = false;
        private bool _ImpactRate4BorderedIsVisible = true;
        private bool _ImpactRate4PathIsVisible = false;
        private bool _ImpactRate5FilledIsVisible = false;
        private bool _ImpactRate5BorderedIsVisible = true;
        private bool _ImpactRate5PathIsVisible = false;

        private double _RecommendRatingPancakeWidth = 0;
        private CornerRadius _RecommendRatingPancakeCornerRadius = 25;

        private bool _RecommendRate1FilledIsVisible = false;
        private bool _RecommendRate1BorderedIsVisible = true;
        private bool _RecommendRate1PathIsVisible = false;
        private bool _RecommendRate2FilledIsVisible = false;
        private bool _RecommendRate2BorderedIsVisible = true;
        private bool _RecommendRate2PathIsVisible = false;
        private bool _RecommendRate3FilledIsVisible = false;
        private bool _RecommendRate3BorderedIsVisible = true;
        private bool _RecommendRate3PathIsVisible = false;
        private bool _RecommendRate4FilledIsVisible = false;
        private bool _RecommendRate4BorderedIsVisible = true;
        private bool _RecommendRate4PathIsVisible = false;
        private bool _RecommendRate5FilledIsVisible = false;
        private bool _RecommendRate5BorderedIsVisible = true;
        private bool _RecommendRate5PathIsVisible = false;
        #endregion

        #region Public Properties
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ActivityListModel SelectedPastActivity
        {
            get { return _SelectedPastActivity; }
            set { SetProperty(ref _SelectedPastActivity, value); }
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

        public string FeedbackComments
        {
            get { return _FeedbackComments; }
            set { SetProperty(ref _FeedbackComments, value); }
        }

        public string FeedbackLikeMostComment
        {
            get { return _FeedbackLikeMostComment; }
            set { SetProperty(ref _FeedbackLikeMostComment, value); }
        }

        public string FeedbackAdditionalComments
        {
            get { return _FeedbackAdditionalComments; }
            set { SetProperty(ref _FeedbackAdditionalComments, value); }
        }

        public double RatingPancakeWidth
        {
            get { return _RatingPancakeWidth; }
            set { SetProperty(ref _RatingPancakeWidth, value); }
        }
        public CornerRadius RatingPancakeCornerRadius
        {
            get { return _RatingPancakeCornerRadius; }
            set { SetProperty(ref _RatingPancakeCornerRadius, value); }
        }

        public bool Rate1FilledIsVisible
        {
            get { return _Rate1FilledIsVisible; }
            set { SetProperty(ref _Rate1FilledIsVisible, value); }
        }
        public bool Rate1BorderedIsVisible
        {
            get { return _Rate1BorderedIsVisible; }
            set { SetProperty(ref _Rate1BorderedIsVisible, value); }
        }
        public bool Rate1PathIsVisible
        {
            get { return _Rate1PathIsVisible; }
            set { SetProperty(ref _Rate1PathIsVisible, value); }
        }
        public bool Rate2FilledIsVisible
        {
            get { return _Rate2FilledIsVisible; }
            set { SetProperty(ref _Rate2FilledIsVisible, value); }
        }
        public bool Rate2BorderedIsVisible
        {
            get { return _Rate2BorderedIsVisible; }
            set { SetProperty(ref _Rate2BorderedIsVisible, value); }
        }
        public bool Rate2PathIsVisible
        {
            get { return _Rate2PathIsVisible; }
            set { SetProperty(ref _Rate2PathIsVisible, value); }
        }
        public bool Rate3FilledIsVisible
        {
            get { return _Rate3FilledIsVisible; }
            set { SetProperty(ref _Rate3FilledIsVisible, value); }
        }
        public bool Rate3BorderedIsVisible
        {
            get { return _Rate3BorderedIsVisible; }
            set { SetProperty(ref _Rate3BorderedIsVisible, value); }
        }
        public bool Rate3PathIsVisible
        {
            get { return _Rate3PathIsVisible; }
            set { SetProperty(ref _Rate3PathIsVisible, value); }
        }
        public bool Rate4FilledIsVisible
        {
            get { return _Rate4FilledIsVisible; }
            set { SetProperty(ref _Rate4FilledIsVisible, value); }
        }
        public bool Rate4BorderedIsVisible
        {
            get { return _Rate4BorderedIsVisible; }
            set { SetProperty(ref _Rate4BorderedIsVisible, value); }
        }
        public bool Rate4PathIsVisible
        {
            get { return _Rate4PathIsVisible; }
            set { SetProperty(ref _Rate4PathIsVisible, value); }
        }
        public bool Rate5FilledIsVisible
        {
            get { return _Rate5FilledIsVisible; }
            set { SetProperty(ref _Rate5FilledIsVisible, value); }
        }
        public bool Rate5BorderedIsVisible
        {
            get { return _Rate5BorderedIsVisible; }
            set { SetProperty(ref _Rate5BorderedIsVisible, value); }
        }
        public bool Rate5PathIsVisible
        {
            get { return _Rate5PathIsVisible; }
            set { SetProperty(ref _Rate5PathIsVisible, value); }
        }

        public double ImpactRatingPancakeWidth
        {
            get { return _ImpactRatingPancakeWidth; }
            set { SetProperty(ref _ImpactRatingPancakeWidth, value); }
        }
        public CornerRadius ImpactRatingPancakeCornerRadius
        {
            get { return _ImpactRatingPancakeCornerRadius; }
            set { SetProperty(ref _ImpactRatingPancakeCornerRadius, value); }
        }

        public bool ImpactRate1FilledIsVisible
        {
            get { return _ImpactRate1FilledIsVisible; }
            set { SetProperty(ref _ImpactRate1FilledIsVisible, value); }
        }
        public bool ImpactRate1BorderedIsVisible
        {
            get { return _ImpactRate1BorderedIsVisible; }
            set { SetProperty(ref _ImpactRate1BorderedIsVisible, value); }
        }
        public bool ImpactRate1PathIsVisible
        {
            get { return _ImpactRate1PathIsVisible; }
            set { SetProperty(ref _ImpactRate1PathIsVisible, value); }
        }
        public bool ImpactRate2FilledIsVisible
        {
            get { return _ImpactRate2FilledIsVisible; }
            set { SetProperty(ref _ImpactRate2FilledIsVisible, value); }
        }
        public bool ImpactRate2BorderedIsVisible
        {
            get { return _ImpactRate2BorderedIsVisible; }
            set { SetProperty(ref _ImpactRate2BorderedIsVisible, value); }
        }
        public bool ImpactRate2PathIsVisible
        {
            get { return _ImpactRate2PathIsVisible; }
            set { SetProperty(ref _ImpactRate2PathIsVisible, value); }
        }
        public bool ImpactRate3FilledIsVisible
        {
            get { return _ImpactRate3FilledIsVisible; }
            set { SetProperty(ref _ImpactRate3FilledIsVisible, value); }
        }
        public bool ImpactRate3BorderedIsVisible
        {
            get { return _ImpactRate3BorderedIsVisible; }
            set { SetProperty(ref _ImpactRate3BorderedIsVisible, value); }
        }
        public bool ImpactRate3PathIsVisible
        {
            get { return _ImpactRate3PathIsVisible; }
            set { SetProperty(ref _ImpactRate3PathIsVisible, value); }
        }
        public bool ImpactRate4FilledIsVisible
        {
            get { return _ImpactRate4FilledIsVisible; }
            set { SetProperty(ref _ImpactRate4FilledIsVisible, value); }
        }
        public bool ImpactRate4BorderedIsVisible
        {
            get { return _ImpactRate4BorderedIsVisible; }
            set { SetProperty(ref _ImpactRate4BorderedIsVisible, value); }
        }
        public bool ImpactRate4PathIsVisible
        {
            get { return _ImpactRate4PathIsVisible; }
            set { SetProperty(ref _ImpactRate4PathIsVisible, value); }
        }
        public bool ImpactRate5FilledIsVisible
        {
            get { return _ImpactRate5FilledIsVisible; }
            set { SetProperty(ref _ImpactRate5FilledIsVisible, value); }
        }
        public bool ImpactRate5BorderedIsVisible
        {
            get { return _ImpactRate5BorderedIsVisible; }
            set { SetProperty(ref _ImpactRate5BorderedIsVisible, value); }
        }
        public bool ImpactRate5PathIsVisible
        {
            get { return _ImpactRate5PathIsVisible; }
            set { SetProperty(ref _ImpactRate5PathIsVisible, value); }
        }

        public double RecommendRatingPancakeWidth
        {
            get { return _RecommendRatingPancakeWidth; }
            set { SetProperty(ref _RecommendRatingPancakeWidth, value); }
        }
        public CornerRadius RecommendRatingPancakeCornerRadius
        {
            get { return _RecommendRatingPancakeCornerRadius; }
            set { SetProperty(ref _RecommendRatingPancakeCornerRadius, value); }
        }

        public bool RecommendRate1FilledIsVisible
        {
            get { return _RecommendRate1FilledIsVisible; }
            set { SetProperty(ref _RecommendRate1FilledIsVisible, value); }
        }
        public bool RecommendRate1BorderedIsVisible
        {
            get { return _RecommendRate1BorderedIsVisible; }
            set { SetProperty(ref _RecommendRate1BorderedIsVisible, value); }
        }
        public bool RecommendRate1PathIsVisible
        {
            get { return _RecommendRate1PathIsVisible; }
            set { SetProperty(ref _RecommendRate1PathIsVisible, value); }
        }
        public bool RecommendRate2FilledIsVisible
        {
            get { return _RecommendRate2FilledIsVisible; }
            set { SetProperty(ref _RecommendRate2FilledIsVisible, value); }
        }
        public bool RecommendRate2BorderedIsVisible
        {
            get { return _RecommendRate2BorderedIsVisible; }
            set { SetProperty(ref _RecommendRate2BorderedIsVisible, value); }
        }
        public bool RecommendRate2PathIsVisible
        {
            get { return _RecommendRate2PathIsVisible; }
            set { SetProperty(ref _RecommendRate2PathIsVisible, value); }
        }
        public bool RecommendRate3FilledIsVisible
        {
            get { return _RecommendRate3FilledIsVisible; }
            set { SetProperty(ref _RecommendRate3FilledIsVisible, value); }
        }
        public bool RecommendRate3BorderedIsVisible
        {
            get { return _RecommendRate3BorderedIsVisible; }
            set { SetProperty(ref _RecommendRate3BorderedIsVisible, value); }
        }
        public bool RecommendRate3PathIsVisible
        {
            get { return _RecommendRate3PathIsVisible; }
            set { SetProperty(ref _RecommendRate3PathIsVisible, value); }
        }
        public bool RecommendRate4FilledIsVisible
        {
            get { return _RecommendRate4FilledIsVisible; }
            set { SetProperty(ref _RecommendRate4FilledIsVisible, value); }
        }
        public bool RecommendRate4BorderedIsVisible
        {
            get { return _RecommendRate4BorderedIsVisible; }
            set { SetProperty(ref _RecommendRate4BorderedIsVisible, value); }
        }
        public bool RecommendRate4PathIsVisible
        {
            get { return _RecommendRate4PathIsVisible; }
            set { SetProperty(ref _RecommendRate4PathIsVisible, value); }
        }
        public bool RecommendRate5FilledIsVisible
        {
            get { return _RecommendRate5FilledIsVisible; }
            set { SetProperty(ref _RecommendRate5FilledIsVisible, value); }
        }
        public bool RecommendRate5BorderedIsVisible
        {
            get { return _RecommendRate5BorderedIsVisible; }
            set { SetProperty(ref _RecommendRate5BorderedIsVisible, value); }
        }
        public bool RecommendRate5PathIsVisible
        {
            get { return _RecommendRate5PathIsVisible; }
            set { SetProperty(ref _RecommendRate5PathIsVisible, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<FeedBackUIModel> ActivityFeelingSelectedCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await ActivityFeelingSelected(obj)); } }
        public DelegateCommand<FeedBackUIModel> ActivityImprovementSelectedCommand { get { return new DelegateCommand<FeedBackUIModel>(async (obj) => await ActivityImprovementSelected(obj)); } }
        public DelegateCommand SubmitFeedbackCommand { get { return new DelegateCommand(async () => await SubmitFeedback()); } }
        public DelegateCommand<string> RatingSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await RatingSelected(obj)); } }
        public DelegateCommand<string> ImpactRatingSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await ImpactRatingSelected(obj)); } }
        public DelegateCommand<string> RecommendRatingSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await RecommendRatingSelected(obj)); } }
        public DelegateCommand<string> JoinedMemberCommand { get { return new DelegateCommand<string>(async (obj) => await JoinedMember(obj)); } }
        #endregion

        #region private methods
        async Task JoinedMember(string selectedActivityPost)
        {
            try
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ActivityID", selectedActivityPost);
                await _navigationService.NavigateAsync(nameof(MemberListPage), navigationParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task SubmitFeedback()
        {
            try
            {
                List<string> activityFeelingList = new List<string>();
                SelectedActivityFeelingList.All((arg) =>
                {
                    activityFeelingList.Add(arg.Title);
                    return true;
                });
                List<string> activityImproveList = new List<string>();
                SelectedImprovementList.All((arg) =>
                {
                    activityImproveList.Add(arg.Title);
                    return true;
                });

                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                ActivityFeedbackInputModel inputModel = new ActivityFeedbackInputModel();
                inputModel.coUniqueID = SelectedPastActivity.coUniqueID;
                inputModel.proUniqueID = SelectedPastActivity.proUniqueID;
                inputModel.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                inputModel.ActivityDate = SelectedPastActivity.proAddActivityDate;
                inputModel.selectedStarRating = OverallExperienceRatingValue;
                inputModel.SliderValue = Convert.ToInt32(ImpactRatingValue * 2);
                inputModel.SliderValue2 = Convert.ToInt32(RecommendRatingValue * 2);
                string delim = ",";
                if (activityFeelingList != null && activityFeelingList.Count > 0)
                {
                    string commaSeparatedFeelList = String.Join(delim, activityFeelingList);
                    inputModel.ActivityFeedbackFeeling = commaSeparatedFeelList;
                }
                if (activityImproveList != null && activityImproveList.Count > 0)
                {
                    string commaSeparatedImproveList = String.Join(delim, activityImproveList);
                    inputModel.ActivityFeedbackImprove = commaSeparatedImproveList;
                }
                inputModel.ActivityFeedbackComments = FeedbackComments;
                inputModel.ActivityFeedback_Like = FeedbackLikeMostComment;
                inputModel.ActivityFeedbackAdditional = FeedbackAdditionalComments;
                var serviceResult = await FeedbackService.SubmitUserFeedback(inputModel);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    await BackClick();
                }
                await ClosePopup();
            }
            catch (Exception exception)
            {
                await ClosePopup();
                Debug.WriteLine(exception.Message);
            }
        }

        async Task ActivityFeelingSelected(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        item.IsSelected = !item.IsSelected;
                        if (item.IsSelected)
                        {
                            SelectedActivityFeelingList.Add(item);
                        }
                        else
                        {
                            SelectedActivityFeelingList.Remove(item);
                        }
                    }
                    return true;
                });

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task ActivityImprovementSelected(FeedBackUIModel selectedTab)
        {
            try
            {
                FeedBackListImproveList.All((item) =>
                {
                    if (item == selectedTab)
                    {
                        item.IsSelected = !item.IsSelected;
                        if (item.IsSelected)
                        {
                            SelectedImprovementList.Add(item);
                        }
                        else
                        {
                            SelectedImprovementList.Remove(item);
                        }
                    }
                    return true;
                });

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task RatingSelected(string selectedRating)
        {
            try
            {
                if (selectedRating == TextResources.RateOneHalf)
                {
                    return;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = false;
                    Rate1PathIsVisible = true;

                    Rate2BorderedIsVisible = true;
                    Rate2FilledIsVisible = false;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = true;
                    Rate3FilledIsVisible = false;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 15;
                    RatingPancakeWidth = 30;

                    //OverallExperienceRatingValue = 1.5;
                }
                else if (selectedRating == TextResources.RateOneFull)
                {
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;

                    Rate2BorderedIsVisible = true;
                    Rate2FilledIsVisible = false;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = true;
                    Rate3FilledIsVisible = false;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 30;
                    RatingPancakeWidth = 60;

                    OverallExperienceRatingValue = 1;
                }
                else if (selectedRating == TextResources.RateTwoHalf)
                {
                    return;
                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = false;
                    Rate2PathIsVisible = true;

                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate3BorderedIsVisible = true;
                    Rate3FilledIsVisible = false;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 25;
                    RatingPancakeWidth = 90;
                }
                else if (selectedRating == TextResources.RateTwoFull)
                {
                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;

                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate3BorderedIsVisible = true;
                    Rate3FilledIsVisible = false;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 35;
                    RatingPancakeWidth = 120;

                    OverallExperienceRatingValue = 2;
                }
                if (selectedRating == TextResources.RateThreeHalf)
                {
                    return;
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = false;
                    Rate3PathIsVisible = true;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 25;
                    RatingPancakeWidth = 150;
                }
                else if (selectedRating == TextResources.RateThreeFull)
                {
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = true;
                    Rate3PathIsVisible = false;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate4BorderedIsVisible = true;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 35;
                    RatingPancakeWidth = 180;

                    OverallExperienceRatingValue = 3;
                }
                if (selectedRating == TextResources.RateFourHalf)
                {
                    return;
                    Rate4BorderedIsVisible = false;
                    Rate4FilledIsVisible = false;
                    Rate4PathIsVisible = true;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = true;
                    Rate3PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 25;
                    RatingPancakeWidth = 210;
                }
                else if (selectedRating == TextResources.RateFourFull)
                {
                    Rate4BorderedIsVisible = false;
                    Rate4FilledIsVisible = true;
                    Rate4PathIsVisible = false;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = true;
                    Rate3PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;
                    Rate5BorderedIsVisible = true;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = false;

                    RatingPancakeCornerRadius = 35;
                    RatingPancakeWidth = 240;

                    OverallExperienceRatingValue = 4;
                }
                if (selectedRating == TextResources.RateFiveHalf)
                {
                    return;
                    Rate5BorderedIsVisible = false;
                    Rate5FilledIsVisible = false;
                    Rate5PathIsVisible = true;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = true;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = false;
                    Rate4FilledIsVisible = true;
                    Rate4PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;

                    RatingPancakeCornerRadius = 25;
                    RatingPancakeWidth = 270;
                }
                else if (selectedRating == TextResources.RateFiveFull)
                {
                    Rate5BorderedIsVisible = false;
                    Rate5FilledIsVisible = true;
                    Rate5PathIsVisible = false;

                    Rate2BorderedIsVisible = false;
                    Rate2FilledIsVisible = true;
                    Rate2PathIsVisible = false;
                    Rate3BorderedIsVisible = false;
                    Rate3FilledIsVisible = true;
                    Rate3PathIsVisible = false;
                    Rate4BorderedIsVisible = false;
                    Rate4FilledIsVisible = true;
                    Rate4PathIsVisible = false;
                    Rate1BorderedIsVisible = false;
                    Rate1FilledIsVisible = true;
                    Rate1PathIsVisible = false;

                    RatingPancakeCornerRadius = 35;
                    RatingPancakeWidth = 300;

                    OverallExperienceRatingValue = 5;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task ImpactRatingSelected(string selectedRating)
        {
            try
            {
                if (selectedRating == TextResources.RateOneHalf)
                {
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = false;
                    ImpactRate1PathIsVisible = true;

                    ImpactRate2BorderedIsVisible = true;
                    ImpactRate2FilledIsVisible = false;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = true;
                    ImpactRate3FilledIsVisible = false;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 15;
                    ImpactRatingPancakeWidth = 30;

                    ImpactRatingValue = 0.5;
                }
                else if (selectedRating == TextResources.RateOneFull)
                {
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;

                    ImpactRate2BorderedIsVisible = true;
                    ImpactRate2FilledIsVisible = false;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = true;
                    ImpactRate3FilledIsVisible = false;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 30;
                    ImpactRatingPancakeWidth = 60;

                    ImpactRatingValue = 1;
                }
                else if (selectedRating == TextResources.RateTwoHalf)
                {
                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = false;
                    ImpactRate2PathIsVisible = true;

                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = true;
                    ImpactRate3FilledIsVisible = false;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 25;
                    ImpactRatingPancakeWidth = 90;

                    ImpactRatingValue = 1.5;
                }
                else if (selectedRating == TextResources.RateTwoFull)
                {
                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;

                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = true;
                    ImpactRate3FilledIsVisible = false;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 35;
                    ImpactRatingPancakeWidth = 120;

                    ImpactRatingValue = 2;
                }
                if (selectedRating == TextResources.RateThreeHalf)
                {
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = false;
                    ImpactRate3PathIsVisible = true;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 25;
                    ImpactRatingPancakeWidth = 150;

                    ImpactRatingValue = 2.5;
                }
                else if (selectedRating == TextResources.RateThreeFull)
                {
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = true;
                    ImpactRate3PathIsVisible = false;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = true;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 35;
                    ImpactRatingPancakeWidth = 180;

                    ImpactRatingValue = 3;
                }
                if (selectedRating == TextResources.RateFourHalf)
                {
                    ImpactRate4BorderedIsVisible = false;
                    ImpactRate4FilledIsVisible = false;
                    ImpactRate4PathIsVisible = true;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = true;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 25;
                    ImpactRatingPancakeWidth = 210;

                    ImpactRatingValue = 3.5;
                }
                else if (selectedRating == TextResources.RateFourFull)
                {
                    ImpactRate4BorderedIsVisible = false;
                    ImpactRate4FilledIsVisible = true;
                    ImpactRate4PathIsVisible = false;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = true;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;
                    ImpactRate5BorderedIsVisible = true;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 35;
                    ImpactRatingPancakeWidth = 240;

                    ImpactRatingValue = 4;
                }
                if (selectedRating == TextResources.RateFiveHalf)
                {
                    ImpactRate5BorderedIsVisible = false;
                    ImpactRate5FilledIsVisible = false;
                    ImpactRate5PathIsVisible = true;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = true;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = false;
                    ImpactRate4FilledIsVisible = true;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 25;
                    ImpactRatingPancakeWidth = 270;

                    ImpactRatingValue = 4.5;
                }
                else if (selectedRating == TextResources.RateFiveFull)
                {
                    ImpactRate5BorderedIsVisible = false;
                    ImpactRate5FilledIsVisible = true;
                    ImpactRate5PathIsVisible = false;

                    ImpactRate2BorderedIsVisible = false;
                    ImpactRate2FilledIsVisible = true;
                    ImpactRate2PathIsVisible = false;
                    ImpactRate3BorderedIsVisible = false;
                    ImpactRate3FilledIsVisible = true;
                    ImpactRate3PathIsVisible = false;
                    ImpactRate4BorderedIsVisible = false;
                    ImpactRate4FilledIsVisible = true;
                    ImpactRate4PathIsVisible = false;
                    ImpactRate1BorderedIsVisible = false;
                    ImpactRate1FilledIsVisible = true;
                    ImpactRate1PathIsVisible = false;

                    ImpactRatingPancakeCornerRadius = 35;
                    ImpactRatingPancakeWidth = 300;

                    ImpactRatingValue = 5;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        async Task RecommendRatingSelected(string selectedRating)
        {
            try
            {
                if (selectedRating == TextResources.RateOneHalf)
                {
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = false;
                    RecommendRate1PathIsVisible = true;

                    RecommendRate2BorderedIsVisible = true;
                    RecommendRate2FilledIsVisible = false;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = true;
                    RecommendRate3FilledIsVisible = false;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 15;
                    RecommendRatingPancakeWidth = 30;

                    RecommendRatingValue = 0.5;
                }
                else if (selectedRating == TextResources.RateOneFull)
                {
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;

                    RecommendRate2BorderedIsVisible = true;
                    RecommendRate2FilledIsVisible = false;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = true;
                    RecommendRate3FilledIsVisible = false;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 30;
                    RecommendRatingPancakeWidth = 60;

                    RecommendRatingValue = 1;
                }
                else if (selectedRating == TextResources.RateTwoHalf)
                {
                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = false;
                    RecommendRate2PathIsVisible = true;

                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = true;
                    RecommendRate3FilledIsVisible = false;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 25;
                    RecommendRatingPancakeWidth = 90;

                    RecommendRatingValue = 1.5;
                }
                else if (selectedRating == TextResources.RateTwoFull)
                {
                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;

                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = true;
                    RecommendRate3FilledIsVisible = false;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 35;
                    RecommendRatingPancakeWidth = 120;

                    RecommendRatingValue = 2;
                }
                if (selectedRating == TextResources.RateThreeHalf)
                {
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = false;
                    RecommendRate3PathIsVisible = true;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 25;
                    RecommendRatingPancakeWidth = 150;

                    RecommendRatingValue = 2.5;
                }
                else if (selectedRating == TextResources.RateThreeFull)
                {
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = true;
                    RecommendRate3PathIsVisible = false;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = true;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 35;
                    RecommendRatingPancakeWidth = 180;

                    RecommendRatingValue = 3;
                }
                if (selectedRating == TextResources.RateFourHalf)
                {
                    RecommendRate4BorderedIsVisible = false;
                    RecommendRate4FilledIsVisible = false;
                    RecommendRate4PathIsVisible = true;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = true;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 25;
                    RecommendRatingPancakeWidth = 210;

                    RecommendRatingValue = 3.5;
                }
                else if (selectedRating == TextResources.RateFourFull)
                {
                    RecommendRate4BorderedIsVisible = false;
                    RecommendRate4FilledIsVisible = true;
                    RecommendRate4PathIsVisible = false;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = true;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;
                    RecommendRate5BorderedIsVisible = true;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 35;
                    RecommendRatingPancakeWidth = 240;

                    RecommendRatingValue = 4;
                }
                if (selectedRating == TextResources.RateFiveHalf)
                {
                    RecommendRate5BorderedIsVisible = false;
                    RecommendRate5FilledIsVisible = false;
                    RecommendRate5PathIsVisible = true;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = true;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = false;
                    RecommendRate4FilledIsVisible = true;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 25;
                    RecommendRatingPancakeWidth = 270;

                    RecommendRatingValue = 4.5;
                }
                else if (selectedRating == TextResources.RateFiveFull)
                {
                    RecommendRate5BorderedIsVisible = false;
                    RecommendRate5FilledIsVisible = true;
                    RecommendRate5PathIsVisible = false;

                    RecommendRate2BorderedIsVisible = false;
                    RecommendRate2FilledIsVisible = true;
                    RecommendRate2PathIsVisible = false;
                    RecommendRate3BorderedIsVisible = false;
                    RecommendRate3FilledIsVisible = true;
                    RecommendRate3PathIsVisible = false;
                    RecommendRate4BorderedIsVisible = false;
                    RecommendRate4FilledIsVisible = true;
                    RecommendRate4PathIsVisible = false;
                    RecommendRate1BorderedIsVisible = false;
                    RecommendRate1FilledIsVisible = true;
                    RecommendRate1PathIsVisible = false;

                    RecommendRatingPancakeCornerRadius = 35;
                    RecommendRatingPancakeWidth = 300;

                    RecommendRatingValue = 5;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
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
            FeedBackList = new ObservableCollection<FeedBackUIModel>();
            FeedBackList.Add(new FeedBackUIModel() { Title = AppResources.HappyText, IsSelected = false });
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
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EventOrganizationText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.TransportationText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.FoodAndBeverageText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.SecurityText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.NGOPartnersText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.CommunicationText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.EverythingWasSatisfactoryText, IsSelected = false });
            FeedBackListImproveList.Add(new FeedBackUIModel() { Title = AppResources.OtherText, IsSelected = false });

            var selectedActivity = parameters.GetValue<ActivityListModel>("SelectedPastActivity");
            if (selectedActivity != null)
            {
                SelectedPastActivity = selectedActivity;
            }
        }
    }
}
