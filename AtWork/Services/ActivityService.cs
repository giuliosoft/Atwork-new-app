using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;

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
    }
}
