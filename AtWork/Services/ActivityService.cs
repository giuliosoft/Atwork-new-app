using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using static AtWork.Models.ActivityModel;

namespace AtWork.Services
{
    public class ActivityService : BaseService
    {
        public static async Task<BaseResponse<string>> GetActivityList(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var activityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.ActivityListServiceURL;
                resultModel = await GetResponse<string>($"{activityListServiceUrl}{id}", true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        
        public static async Task<BaseResponse<string>> UnSubscribeActivity(int id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var UnsubscribeActivityUrl = ConfigService.BaseServiceURL + ConfigService.ActivityUnsubscribeServiceURL;
                resultModel = await PostResponse<string>($"{UnsubscribeActivityUrl}{id}", string.Empty, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
    }
}
