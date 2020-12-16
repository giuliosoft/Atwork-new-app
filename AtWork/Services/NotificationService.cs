using System;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using Newtonsoft.Json;
using static AtWork.Models.NotificationModel;

namespace AtWork.Services
{
    public class NotificationService : BaseService
    {
        public static async Task<BaseResponse<string>> SaveNotificationSetting(Notification inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var SaveNotificationSettingUrl = ConfigService.BaseServiceURL + ConfigService.UpdateSaveNotificationSetting;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(SaveNotificationSettingUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetNotificationSetting(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                 var activityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetNotificationSetting;
                var paramServiceUrl = $"{activityListServiceUrl}{id}";

                resultModel = await GetResponse<string>(paramServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetNotificationSetting_v1(Notification input)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var GetNotificationUrl = ConfigService.BaseServiceURL + ConfigService.GetNotificationSetting_V1;
                var jData = JsonConvert.SerializeObject(input);
                resultModel = await PostResponse<string>(GetNotificationUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetConnectNotificationSetting(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var activityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetConnectSetting;
                var paramServiceUrl = $"{activityListServiceUrl}{id}";

                resultModel = await GetResponse<string>(paramServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> SaveConnectNotificationSetting(Connect_Notification_Setting inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var SaveNotificationSettingUrl = ConfigService.BaseServiceURL + ConfigService.SaveConnectSetting;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(SaveNotificationSettingUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }


        public static async Task<BaseResponse<string>> GetActivityNotificationSetting(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var activityListServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetActiveSetting;
                var paramServiceUrl = $"{activityListServiceUrl}{id}";

                resultModel = await GetResponse<string>(paramServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> SaveActivityNotificationSetting(Activity_Notification_Setting inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var SaveNotificationSettingUrl = ConfigService.BaseServiceURL + ConfigService.SaveActiveSetting;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(SaveNotificationSettingUrl, jData, true);
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
