using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.FeedbackModel;

namespace AtWork.Services
{
    public class FeedbackService : BaseService
    {
        public static async Task<BaseResponse<string>> SubmitUserFeedback(ActivityFeedbackInputModel inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var feedbackServiceUrl = ConfigService.BaseServiceURL + ConfigService.ActivitySaveFeedbackServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(feedbackServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetUserSubmittedFeedback(string input)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var getfeedbackServiceUrl = ConfigService.BaseServiceURL + ConfigService.ActivityGetFeedbackServiceURL;
                var fUrl = $"{getfeedbackServiceUrl}{input}";
                resultModel = await GetResponse<string>(fUrl, true);
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
