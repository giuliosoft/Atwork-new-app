using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

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
    }
}
