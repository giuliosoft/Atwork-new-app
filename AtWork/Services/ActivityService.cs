using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using Newtonsoft.Json;
using static AtWork.Models.ActivityModel;

namespace AtWork.Services
{
    public class ActivityService : BaseService
    {
        public static async Task<BaseResponse<string>> GetActivityList(string id, string categoryId = "")
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var activityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.ActivityListServiceURL;
                var paramServiceUrl = $"{activityListServiceUrl}{id}";
                if (!string.IsNullOrEmpty(categoryId)) { paramServiceUrl += "/" + categoryId; }

                resultModel = await GetResponse<string>(paramServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetMyActivityList(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var myActivityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.MyActivityListServiceURL;
                resultModel = await GetResponse<string>($"{myActivityListServiceUrl}{id}", true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetActivityDetail(string NewUrl)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var ActivitiesDetailsUrl = ConfigService.BaseServiceURL + ConfigService.ActivitiesGetRowServiceURL + NewUrl;
                resultModel = await GetResponse<string>(ActivitiesDetailsUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> JoinActivity(JoinActivityInputModel joinActivityInputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var JoinActivityUrl = ConfigService.BaseServiceURL + ConfigService.ActivityJoinServiceURL;
                var jData = JsonConvert.SerializeObject(joinActivityInputModel);
                resultModel = await PostResponse<string>(JoinActivityUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> UnSubscribeActivity(JoinActivityInputModel unsubscribeActivityInputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var UnsubscribeActivityUrl = ConfigService.BaseServiceURL + ConfigService.ActivityUnsubscribeServiceURL;
                var jData = JsonConvert.SerializeObject(unsubscribeActivityInputModel);
                resultModel = await PostResponse<string>(UnsubscribeActivityUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> PostActivityFeedEdit(ActivityListModel inputModel, List<string> filesToAttach)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var editNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.CreateActivityServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await FilePostResponse<string>(editNewsServiceUrl, filesToAttach, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> ActivityEdit(ActivityListModel inputModel, List<string> filesToAttach)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var editNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.EditActivityServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                //resultModel = await PostResponse<string>(editNewsServiceUrl, jData, true);
                resultModel = await FilePostResponse<string>(editNewsServiceUrl, filesToAttach, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetActivityJoinedMemberList(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var ActivitiesDetailsUrl = ConfigService.BaseServiceURL + ConfigService.ActivitiesJoinedMembersServiceURL + id;
                resultModel = await GetResponse<string>(ActivitiesDetailsUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetGroupMemberListCount(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var GroupMemberCountUrl = ConfigService.BaseServiceURL + ConfigService.GroupMemberCountURL + id;
                resultModel = await GetResponse<string>(GroupMemberCountUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> DeleteActivity(int id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var deleteNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.ActivityDeleteServiceURL;
                resultModel = await GetResponse<string>($"{deleteNewsServiceUrl}{id}", true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetActivityHistoryDetails(string Url)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var ActivityHistoryURL = ConfigService.BaseServiceURL + ConfigService.ActivityHistoryServiceURL + Url;
                resultModel = await GetResponse<string>(ActivityHistoryURL, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

    }
}
