using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using AtWork.Popups;
using AtWork.Helpers;
using Newtonsoft.Json;
using static AtWork.Models.ActivityModel;

namespace AtWork.ViewModels
{
    public class ActivityDetailPageViewModel : ViewModelBase
    {
        #region Constructor
        public ActivityDetailPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
            ActivityTagList.Add(new ActivityTagModel() { ActivityTag = "Corporate volunteering" });
            ActivityTagList.Add(new ActivityTagModel() { ActivityTag = "Environment & nature" });
            ActivityTagList.Add(new ActivityTagModel() { ActivityTag = "Nature" });
            ActivityTagList.Add(new ActivityTagModel() { ActivityTag = "Physically active" });

            HeaderDetailsTitle = AppResources.ActivityText;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private string _ActivityTitle = string.Empty;
        private string _ActivityDescription = string.Empty;
        private string _CategoryName = string.Empty;
        private string _Location = string.Empty;
        private string _MinGroupSize = string.Empty;
        private string _MaxGroupSize = string.Empty;
        private string _ActivityTime = string.Empty;
        private string _CostCoveredByCompany = string.Empty;
        private string _CostCoveredByEmployee = string.Empty;
        private string _ActivityLanguage = string.Empty;
        private string _SpecialRequirement = string.Empty;
        private string _OrganiserName = string.Empty;
        private string _OrganiserAddress = string.Empty;
        private string _AdditionalInfo = string.Empty;
        
        private ObservableCollection<ActivityTagModel> _ActivityTagList = new ObservableCollection<ActivityTagModel>();
        #endregion

        #region Public Properties        
        public string ActivityTitle
        {
            get { return _ActivityTitle; }
            set { SetProperty(ref _ActivityTitle, value); }
        }
        public string ActivityDescription
        {
            get { return _ActivityDescription; }
            set { SetProperty(ref _ActivityDescription, value); }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { SetProperty(ref _CategoryName, value); }
        }
        
        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }
        public string MinGroupSize
        {
            get { return _MinGroupSize; }
            set { SetProperty(ref _MinGroupSize, value); }
        }
        public string MaxGroupSize
        {
            get { return _MaxGroupSize; }
            set { SetProperty(ref _MaxGroupSize, value); }
        }
        public string ActivityTime
        {
            get { return _ActivityTime; }
            set { SetProperty(ref _ActivityTime, value); }
        }
        public string CostCoveredByCompany
        {
            get { return _CostCoveredByCompany; }
            set { SetProperty(ref _CostCoveredByCompany, value); }
        }
        public string CostCoveredByEmployee
        {
            get { return _CostCoveredByEmployee; }
            set { SetProperty(ref _CostCoveredByEmployee, value); }
        }
        public string ActivityLanguage
        {
            get { return _ActivityLanguage; }
            set { SetProperty(ref _ActivityLanguage, value); }
        }
        public string SpecialRequirement
        {
            get { return _SpecialRequirement; }
            set { SetProperty(ref _SpecialRequirement, value); }
        }
        public string OrganiserName
        {
            get { return _OrganiserName; }
            set { SetProperty(ref _OrganiserName, value); }
        }
        public string OrganiserAddress
        {
            get { return _OrganiserAddress; }
            set { SetProperty(ref _OrganiserAddress, value); }
        }
        public string AdditionalInfo
        {
            get { return _AdditionalInfo; }
            set { SetProperty(ref _AdditionalInfo, value); }
        }


        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ObservableCollection<ActivityTagModel> ActivityTagList
        {
            get { return _ActivityTagList; }
            set { SetProperty(ref _ActivityTagList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand GoToJoinActivityPopupCommand { get { return new DelegateCommand(async () => await GoToJoinActivityPopup()); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task GoToJoinActivityPopup()
        {
            try
            {
                JoinActivityPopup JoinActivityPopup = new JoinActivityPopup();
                JoinActivityPopupViewModel joinactivityPopupViewModel = new JoinActivityPopupViewModel(_navigationService, _facadeService);
                joinactivityPopupViewModel.ProfileSelectedEvent += async (object sender, string SelectedObj) =>
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                };
                JoinActivityPopup.BindingContext = joinactivityPopupViewModel;
                await PopupNavigationService.ShowPopup(JoinActivityPopup, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task LoadActivitiesDetails()
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                var serviceResult = await ActivityServices.GetActivityDetail("procorp2019023511232400420207251552438");
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityDetails>(serviceResult.Body);
                ActivityTitle = serviceResultBody?.proTitle;
                ActivityDescription = serviceResultBody?.proDescription;
                CategoryName = serviceResultBody?.proCategoryName;
                Location = serviceResultBody?.proLocation + ", " +serviceResultBody?.proCountry;
                MinGroupSize = serviceResultBody?.proAddActivity_ParticipantsMinNumber != null && serviceResultBody?.proAddActivity_ParticipantsMinNumber != string.Empty
                    ? serviceResultBody?.proAddActivity_ParticipantsMinNumber
                    : "-";

                MaxGroupSize = serviceResultBody?.proAddActivity_ParticipantsMaxNumber != null && serviceResultBody?.proAddActivity_ParticipantsMaxNumber != string.Empty
                    ? serviceResultBody.proAddActivity_ParticipantsMaxNumber
                    : "-";
                
                ActivityTime = serviceResultBody?.proAddActivity_StartTime != null && serviceResultBody?.proAddActivity_StartTime != string.Empty && serviceResultBody?.proAddActivity_EndTime  != null && serviceResultBody?.proAddActivity_EndTime != string.Empty
                    ? serviceResultBody.proAddActivity_StartTime + " to  " + serviceResultBody.proAddActivity_EndTime
                    : string.Empty;

                CostCoveredByCompany = serviceResultBody?.proCostCoveredCompany != null && serviceResultBody?.proCostCoveredCompany != string.Empty
                    ? serviceResultBody.proCostCoveredCompany : "-";

                CostCoveredByEmployee = serviceResultBody?.proCostCoveredEmployee != null && serviceResultBody?.proCostCoveredEmployee != string.Empty
                    ? serviceResultBody.proCostCoveredEmployee : "-";

                ActivityLanguage = serviceResultBody?.proActivityLanguage != null && serviceResultBody?.proActivityLanguage != string.Empty
                    ? serviceResultBody.proActivityLanguage : "-";

                SpecialRequirement = serviceResultBody?.proSpecialRequirements != null && serviceResultBody?.proSpecialRequirements != string.Empty
                    ? serviceResultBody.proSpecialRequirements : "-";

                OrganiserName = serviceResultBody?.proAddActivity_OrgName != null && serviceResultBody?.proAddActivity_OrgName != string.Empty
                    ? serviceResultBody.proAddActivity_OrgName : "-";

                AdditionalInfo = serviceResultBody?.proAddActivity_AdditionalInfo != null && serviceResultBody?.proAddActivity_AdditionalInfo != string.Empty
                    ? serviceResultBody.proAddActivity_AdditionalInfo : "-";


                await ClosePopup();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await ClosePopup();
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
            LoadActivitiesDetails();
        }
    }

    public class ActivityTagModel
    {
        public string ActivityTag { get; set; }
    }
}
