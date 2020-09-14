using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.UserSettingModel;

namespace AtWork.Services
{
    public class UserServices : BaseService
    {
        public static async Task<BaseResponse<string>> LoginToApp(LoginInputModel inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var loginServiceUrl = ConfigService.BaseServiceURL + ConfigService.LoginServiceURL;
                //var jData = JsonConvert.SerializeObject(inputModel);
                //resultModel = await PostResponse<string>(loginServiceUrl, jData, true);
                resultModel = await LoginPostResponse<string>(loginServiceUrl, inputModel, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetUserDetails(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var aboutUserServiceUrl = ConfigService.BaseServiceURL + ConfigService.AboutUserServiceURL;
                resultModel = await GetResponse<string>(aboutUserServiceUrl, true);
                var UserProfileURL = ConfigService.BaseServiceURL + ConfigService.UserProfileURL + id;
                resultModel = await GetResponse<string>(UserProfileURL, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetUserlanguage()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var UserLanguageURL = ConfigService.BaseServiceURL + ConfigService.UserLanguageURL;
                resultModel = await GetResponse<string>(UserLanguageURL, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UpdateUserLanguage(Volunteers inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateLanguageServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetAboutUserInfo()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var aboutUserServiceUrl = ConfigService.BaseServiceURL + ConfigService.AboutUserServiceURL;
                resultModel = await GetResponse<string>(aboutUserServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> UpdateAboutUserInfo(UserSettingInputModel inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateAboutUserServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetInterestsDetails()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var InterestsServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetInterestsServiceURL;
                resultModel = await GetResponse<string>(InterestsServiceUrl, true);
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
