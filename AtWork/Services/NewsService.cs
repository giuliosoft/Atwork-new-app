using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Services
{
    public class NewsService : BaseService
    {
        public static async Task<BaseResponse<string>> NewsDetail(string NewUrl)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var loginServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsDetailsServiceURL + NewUrl;
                //resultModel = await PostResponse<string>(loginServiceUrl, jData, true);
                resultModel = await GetResponse<string>(loginServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> AddComment(NewsComment NewsComment)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var addCommentServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsDetailsAddCommentServiceURL;
                var jData = JsonConvert.SerializeObject(NewsComment);
                resultModel = await PostResponse<string>(addCommentServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> EditComment(NewsComment NewsComment)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var url = ConfigService.BaseServiceURL + ConfigService.NewsDetailsEditCommentServiceURL;
                var jData = JsonConvert.SerializeObject(NewsComment);
                resultModel = await PostResponse<string>(url, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> DeleteComment(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var url = ConfigService.BaseServiceURL + ConfigService.NewsDetailsDeleteCommentServiceURL;
                resultModel = await PostResponse<string>($"{url}{id}", string.Empty, true);
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
