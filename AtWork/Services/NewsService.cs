using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.CommentsModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Services
{
    public class NewsService : BaseService
    {
        public static async Task<BaseResponse<string>> NewsDetail(string NewUrl, string CurrentUserId)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var loginServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsDetailsServiceURL + NewUrl;// + "/" + CurrentUserId;
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
        public static async Task<BaseResponse<string>> DeleteComment(int id)
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

        public static async Task<BaseResponse<string>> PostNewsFeed(NewsDetailModel_Input inputModel, List<string> filesToAttach)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var addNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsAddPostServiceURL;
                //var jData = JsonConvert.SerializeObject(inputModel);
                //resultModel = await PostResponse<string>(addNewsServiceUrl, jData, true);
                resultModel = await FilePostResponse<string>(addNewsServiceUrl, filesToAttach, inputModel, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> PostNewsFeed1(NewsDetailModel_Input inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var addNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsAddPostServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(addNewsServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetNewsCommentListByID(string NewsId)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                //var strCommentListUrl = "http://app.atwork.ai/api/commentslikes/getcommentlist/newscorp2019023511232400720208151822347";
                var strCommentListUrl = ConfigService.BaseServiceURL + ConfigService.NewsDetailsGetCommentListURL + NewsId;
                resultModel = await GetResponse<string>(strCommentListUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> PostNewsFeedEdit(NewsDetailModel_Input inputModel, List<string> filesToAttach)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var editNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsPostEditServiceURL;
                //var jData = JsonConvert.SerializeObject(inputModel);
                //resultModel = await PostResponse<string>(editNewsServiceUrl, jData, true);
                resultModel = await FilePostResponse<string>(editNewsServiceUrl, filesToAttach, inputModel, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> DeleteNewsPost(int id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var deleteNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsPostDeleteServiceURL;
                resultModel = await PostResponse<string>($"{deleteNewsServiceUrl}{id}", string.Empty, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetNewsList(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var newsServiceUrl = ConfigService.BaseServiceURL + ConfigService.NewsListServiceURL;
                resultModel = await GetResponse<string>($"{newsServiceUrl}{id}", true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> LikeNewsFeed(NewsLikes newsLikes)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var addNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.CommentsLikesServiceURL + ConfigService.AddNewsLikeServiceURL;
                var jData = JsonConvert.SerializeObject(newsLikes);
                resultModel = await PostResponse<string>(addNewsServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                Debug.WriteLine(ex.Message);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UnLikeNewsFeed(NewsLikes newsLikes)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var addNewsServiceUrl = ConfigService.BaseServiceURL + ConfigService.CommentsLikesServiceURL + ConfigService.DeleteNewsLikeServiceURL;
                var jData = JsonConvert.SerializeObject(newsLikes);
                resultModel = await PostResponse<string>(addNewsServiceUrl, jData, true);
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
