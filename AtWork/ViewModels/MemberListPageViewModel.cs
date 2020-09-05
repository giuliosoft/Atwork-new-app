using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.LoginModel;

namespace AtWork.ViewModels
{
    public class MemberListPageViewModel : ViewModelBase
    {
        public MemberListPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
        }
        private ObservableCollection<Volunteers> _members = new ObservableCollection<Volunteers>();
        public ObservableCollection<Volunteers> Members
        {
            get { return _members; }
            set
            {
                if (SetProperty(ref _members, value))
                    HeaderDetailsTitle = $"{Members?.Count} joined";
            }
        }
        public DelegateCommand<Volunteers> SelectionChangedCommand { get { return new DelegateCommand<Volunteers>(async (obj) => await OnSelectionChanged(obj)); } }

        private async Task OnSelectionChanged(Volunteers member)
        {
            //TODO Profile
            //await DisplayAlertAsync(member.Name);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        async Task LoadMembersList(string id)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                var serviceResult = await ActivityService.GetActivityJoinedMemberList(id);
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityJoinedMemberListResponse>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Data != null)
                {
                    List<Volunteers> tempMembers = new List<Volunteers>();
                    foreach (var member in serviceResultBody.Data)
                        tempMembers.Add(member);
                    Members = new ObservableCollection<Volunteers>(tempMembers);
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await ClosePopup();
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                var activityId = parameters.GetValue<string>("ActivityID");
                if (!string.IsNullOrEmpty(activityId))
                    await LoadMembersList(activityId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
