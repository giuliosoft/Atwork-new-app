using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
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
                    HeaderDetailsTitle = string.Format("{0} {1}", Members?.Count, AppResources.joinedText);
            }
        }
        public DelegateCommand<Volunteers> SelectionChangedCommand { get { return new DelegateCommand<Volunteers>(async (obj) => await OnSelectionChanged(obj)); } }

        private async Task OnSelectionChanged(Volunteers member)
        {
            try
            {
                if (!string.IsNullOrEmpty(member.volUniqueID))
                {
                    await OpenUserProfileAsync(member.volUniqueID);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
            //TODO Profile
            //await DisplayAlertAsync(member.Name);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        async Task LoadMembersList(string id = null, string classID = null)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                BaseResponse<string> serviceResult = new BaseResponse<string>();
                if (!string.IsNullOrEmpty(classID))
                {
                    serviceResult = await UserServices.GetUserByGroup(classID);
                }
                else
                {
                    serviceResult = await ActivityService.GetActivityJoinedMemberList(id);
                }
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityJoinedMemberListResponse>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Data != null)
                {
                    List<Volunteers> tempMembers = new List<Volunteers>();
                    foreach (var member in serviceResultBody.Data)
                        tempMembers.Add(member);
                    Members = new ObservableCollection<Volunteers>(tempMembers);
                    if (!string.IsNullOrEmpty(classID))
                    {
                        HeaderDetailsTitle = classID;
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
                await ClosePopup();
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                var activityId = parameters.GetValue<string>("ActivityID");
                var classId = parameters.GetValue<string>("ClassID");

                if (!string.IsNullOrEmpty(activityId))
                {
                    await LoadMembersList(activityId);
                }
                else if (!string.IsNullOrEmpty(classId))
                {
                   
                    await LoadMembersList(classID: classId);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}
