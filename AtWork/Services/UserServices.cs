using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

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
    }
}
