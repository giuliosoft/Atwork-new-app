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
using static AtWork.Models.UserModel;

namespace AtWork.ViewModels
{
    public class MemberListPageViewModel : ViewModelBase
    {
        private int PageNo = 1;
        bool _isBusy = false;
        string activityId = string.Empty;
        string classUniqueID = string.Empty;
        string ClassValue = string.Empty;
        private int _remainingItemsThreshold = 0;
        private int _remainingActivityItemsThreshold = 0;
        public MemberListPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
        }
        public int RemainingItemsThreshold
        {
            get { return _remainingItemsThreshold; }
            set { SetProperty(ref _remainingItemsThreshold, value); }
        }
        public int RemainingActivityItemsThreshold
        {
            get { return _remainingActivityItemsThreshold; }
            set { SetProperty(ref _remainingActivityItemsThreshold, value); }
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
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        public DelegateCommand<Volunteers> SelectionChangedCommand { get { return new DelegateCommand<Volunteers>(async (obj) => await OnSelectionChanged(obj)); } }
        public DelegateCommand NewsLoadMoreItemsCommand { get { return new DelegateCommand(async () => await NewsLoadMoreItems()); } }

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
        async Task LoadMembersList(string id = null, bool isForGroupmember = false)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                if (!await CheckConnectivity())
                {
                    return;
                }
                await ShowLoader();
                BaseResponse<string> serviceResult = new BaseResponse<string>();
                if (isForGroupmember)
                {
                    GetUsersByGroupWise input = new GetUsersByGroupWise();
                    input.coUniqueID = SettingsService.LoggedInUserData.coUniqueID;
                    input.classUniqueID = classUniqueID;
                    input.classValue = ClassValue;
                    input.pageNumber = PageNo;
                    serviceResult = await UserServices.GetUserByGroup(input);
                }
                else
                {
                    serviceResult = await ActivityService.GetActivityJoinedMemberList(id);
                }
                var serviceResultBody = JsonConvert.DeserializeObject<ActivityJoinedMemberListResponse>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Data != null)
                {
                    //List<Volunteers> tempMembersList = new List<Volunteers>();
                    //if (PageNo < 5)
                    //{
                    //    for (int i = 0; i < 25; i++)
                    //    {
                    //        tempMembersList.Add(new Volunteers() { volFirstName = i.ToString() });
                    //    }
                    //}
                    if (isForGroupmember)
                    {
                        if (serviceResultBody.Data.Count < 25)
                            RemainingItemsThreshold = -1;
                        else
                            RemainingItemsThreshold = 0;

                        if (PageNo == 1)
                        {
                            Members.Clear();
                        }
                    }
                    List<Volunteers> tempMembers = new List<Volunteers>(Members);
                    foreach (var member in serviceResultBody.Data)
                        tempMembers.Add(member);
                    Members = new ObservableCollection<Volunteers>(tempMembers);
                    if (!string.IsNullOrEmpty(ClassValue))
                    {
                        HeaderDetailsTitle = ClassValue;
                    }
                }
                await ClosePopup(); 
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
                await ClosePopup();
            }
            finally
            {
                IsBusy = false;
            }
        }


        async Task NewsLoadMoreItems()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                PageNo++;
                await LoadMembersList(isForGroupmember: true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }

        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                activityId = parameters.GetValue<string>("ActivityID");
                classUniqueID = parameters.GetValue<string>("ClassUniqueID");
                ClassValue = parameters.GetValue<string>("ClassValue");

               

                if (!string.IsNullOrEmpty(activityId))
                {
                    await LoadMembersList(activityId);
                }
                else if (!string.IsNullOrEmpty(classUniqueID) && !string.IsNullOrEmpty(ClassValue) && !string.IsNullOrEmpty(SettingsService.LoggedInUserData.coUniqueID))
                {
                   
                    await LoadMembersList(isForGroupmember: true);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}
