using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Xamarin.Forms;

namespace AtWork.Services
{
    public class ActivityServices : BaseService
    {
        public static async Task<BaseResponse<string>> GetActivityDetail(string NewUrl)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var ActivitiesDetailsUrl = ConfigService.BaseServiceURL + ConfigService.ActivitiesGetRowServiceURL + NewUrl;
                //resultModel = await PostResponse<string>(loginServiceUrl, jData, true);
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
