using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using AtWork.Popups;

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
        private ObservableCollection<ActivityTagModel> _ActivityTagList = new ObservableCollection<ActivityTagModel>();
        #endregion

        #region Public Properties        
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
        }
    }

    public class ActivityTagModel
    {
        public string ActivityTag { get; set; }
    }
}
