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
        string SelectedActivityID;
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
        private string _Location = string.Empty;
        private string _ActivityTime = string.Empty;
        private ActivityListModel _activityDetails = new ActivityListModel();

        private string _ActivityTitle = string.Empty;
        private string _ActivityDescription = string.Empty;
        private string _CategoryName = string.Empty;
        private bool _IsShowCategotyType = false;
        private string _MaxGroupSize = string.Empty;
        private string _CostCoveredByCompany = string.Empty;
        private string _CostCoveredByEmployee = string.Empty;
        private string _ActivityLanguage = string.Empty;
        private string _SpecialRequirement = string.Empty;
        private string _OrganisarName = string.Empty;
        //private string _OrganisarAddress = string.Empty;
        private string _AdditionalInfo = string.Empty;
        
        private ObservableCollection<ActivityTagModel> _ActivityTagList = new ObservableCollection<ActivityTagModel>();
        #endregion

        #region Public Properties        
        //public string ActivityTitle
        //{
        //    get { return _ActivityTitle; }
        //    set { SetProperty(ref _ActivityTitle, value); }
        //}
        //public string ActivityDescription
        //{
        //    get { return _ActivityDescription; }
        //    set { SetProperty(ref _ActivityDescription, value); }
        //}
        //public string CategoryName
        //{
        //    get { return _CategoryName; }
        //    set { SetProperty(ref _CategoryName, value); }
        //}

        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }
        public string ActivityTime
        {
            get { return _ActivityTime; }
            set { SetProperty(ref _ActivityTime, value); }
        }
        public ActivityListModel ActivityDetails
        {
            get { return _activityDetails; }
            set { SetProperty(ref _activityDetails, value); }
        }

        public bool IsShowCategotyType
        {
            get { return _IsShowCategotyType; }
            set { SetProperty(ref _IsShowCategotyType, value); }
        }
        //public string MinGroupSize
        //{
        //    get { return _MinGroupSize; }
        //    set { SetProperty(ref _MinGroupSize, value); }
        //}
        //public string MaxGroupSize
        //{
        //    get { return _MaxGroupSize; }
        //    set { SetProperty(ref _MaxGroupSize, value); }
        //}
        //public string CostCoveredByCompany
        //{
        //    get { return _CostCoveredByCompany; }
        //    set { SetProperty(ref _CostCoveredByCompany, value); }
        //}
        //public string CostCoveredByEmployee
        //{
        //    get { return _CostCoveredByEmployee; }
        //    set { SetProperty(ref _CostCoveredByEmployee, value); }
        //}
        //public string ActivityLanguage
        //{
        //    get { return _ActivityLanguage; }
        //    set { SetProperty(ref _ActivityLanguage, value); }
        //}
        //public string SpecialRequirement
        //{
        //    get { return _SpecialRequirement; }
        //    set { SetProperty(ref _SpecialRequirement, value); }
        //}
        //public string OrganisarName
        //{
        //    get { return _OrganisarName; }
        //    set { SetProperty(ref _OrganisarName, value); }
        //}
        //public string OrganisarAddress
        //{
        //    get { return _OrganisarAddress; }
        //    set { SetProperty(ref _OrganisarAddress, value); }
        //}
        //public string AdditionalInfo
        //{
        //    get { return _AdditionalInfo; }
        //    set { SetProperty(ref _AdditionalInfo, value); }
        //}

        //public string Prop
        //{
        //    get { return _Prop; }
        //    set { SetProperty(ref _Prop, value); }
        //}

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
                var serviceResult = await ActivityServices.GetActivityDetail(SelectedActivityID.ToString());// "procorp2019023511232400420207251552438");
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityListModel>(serviceResult.Body);

                ActivityDetails = serviceResultBody;
                Location = serviceResultBody?.proLocation + ", " +serviceResultBody?.proCountry;
                ActivityTime = serviceResultBody?.proAddActivity_StartTime != null && serviceResultBody?.proAddActivity_StartTime != string.Empty && serviceResultBody?.proAddActivity_EndTime  != null && serviceResultBody?.proAddActivity_EndTime != string.Empty
                    ? serviceResultBody.proAddActivity_StartTime + " to  " + serviceResultBody.proAddActivity_EndTime
                    : string.Empty;
                if (serviceResultBody?.proCategoryName != null && serviceResultBody?.proCategoryName != string.Empty)
                {
                    IsShowCategotyType = true;
                }
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
            try
            {
                SelectedActivityID = parameters.GetValue<string>("SelectedActivityID");
                await LoadActivitiesDetails();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class ActivityTagModel
    {
        public string ActivityTag { get; set; }
    }
}
