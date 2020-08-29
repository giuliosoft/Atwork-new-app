using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using Newtonsoft.Json;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Services
{
    public class BaseService
    {
        public BaseService()
        {
        }

        public static HttpStatusCode[] httpStatusCodesWorthRetrying =
        {
            HttpStatusCode.Unauthorized, // 401
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout, // 504
        };

        /// <summary>
        /// GetResponse
        /// </summary>
        /// <param name="serviceUrl">API Service Url</param>
        /// <param name="isAddAuthorizationToken">User Authorization Token</param>
        /// <returns></returns>
        public static async Task<BaseResponse<T>> GetResponse<T>(string serviceUrl, bool isAddAuthorizationToken)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(serviceUrl) && !serviceUrl.StartsWith("http", StringComparison.Ordinal))
                    {
                        client.BaseAddress = new Uri(ConfigService.BaseServiceURL);
                    }

                    Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);

                    if (isAddAuthorizationToken)
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}")));
                    }
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);

                    var response = await client.GetAsync(serviceUrl);
                    resultModel = await HandleServerResponse<T>(response);
                    return resultModel;
                }
            }
            catch (Exception exception)
            {
                return HandleException<T>(exception);
            }
        }

        /// <summary>
        /// PostResponse
        /// </summary>
        /// <param name="serviceUrl">API Service Url</param>
        /// <param name="jData">Input Json Data</param>
        /// <param name="isAddAuthorizationToken">User Authorization Token</param>
        /// <returns></returns>
        public static async Task<BaseResponse<T>> PostResponse<T>(string serviceUrl, string jData, bool isAddAuthorizationToken)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigService.BaseServiceURL);
                    Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);
                    Debug.WriteLine("\n\nRequest : " + jData);

                    if (isAddAuthorizationToken)
                    {
                        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SettingsService.AuthorizationToken);
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}")));
                    }

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var content = new StringContent(jData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(serviceUrl, content);
                    resultModel = await HandleServerResponse<T>(response);
                    return resultModel;
                }
            }
            catch (Exception exception)
            {
                return HandleException<T>(exception);
            }
        }

        /// <summary>
        /// PostResponse
        /// </summary>
        /// <param name="serviceUrl">API Service Url</param>
        /// <param name="Loing input">Input Json Data</param>
        /// <param name="isAddAuthorizationToken">User Authorization Token</param>
        /// <returns></returns>
        public static async Task<BaseResponse<T>> LoginPostResponse<T>(string serviceUrl, LoginInputModel loginInputModel, bool isAddAuthorizationToken)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    var jData = JsonConvert.SerializeObject(loginInputModel);
                    client.BaseAddress = new Uri(ConfigService.BaseServiceURL);
                    Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);
                    Debug.WriteLine("\n\nRequest : " + jData);

                    if (isAddAuthorizationToken)
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{loginInputModel.email}:{loginInputModel.password}")));
                        //string UserId = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{loginInputModel.email}:{loginInputModel.password}"));
                    }

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var content = new StringContent(jData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(serviceUrl, content);
                    resultModel = await HandleServerResponse<T>(response);
                    return resultModel;
                }
            }
            catch (Exception exception)
            {
                return HandleException<T>(exception);
            }
        }

        /// <summary>
        /// PostResponse
        /// </summary>
        /// <param name="serviceUrl">API Service Url</param>
        /// <param name="Loing input">Input Json Data</param>
        /// <param name="isAddAuthorizationToken">User Authorization Token</param>
        /// <returns></returns>
        public static async Task<BaseResponse<T>> FilePostResponse<T>(string serviceUrl, List<string> fileToAttachList, NewsDetailModel_Input inputModel, bool isAddAuthorizationToken)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            try
            {
                var client = new HttpClient();
                if (isAddAuthorizationToken)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}")));
                }
                
                MultipartFormDataContent Mcontent = new MultipartFormDataContent();
                //if (fileToAttachList != null && fileToAttachList.Count > 0)
                //{
                //    var last = fileToAttachList.LastOrDefault();
                //    var fileContentsInBytes = File.ReadAllBytes(last);
                //    ByteArrayContent byteContent = new ByteArrayContent(fileContentsInBytes);
                //    Mcontent.Add(byteContent, "NewsImage", inputModel.newsFileOriginal);
                //    for (var arg = 0; arg < fileToAttachList.Count - 1; arg++)
                //    {
                //        var fileContentsInBytes1 = File.ReadAllBytes(fileToAttachList[arg]);
                //        ByteArrayContent byteContent1 = new ByteArrayContent(fileContentsInBytes1);
                //        Mcontent.Add(byteContent1, "NewsImage", "NewsPost.png");
                //    }
                //}

                /*
                StringContent coUniqueIDContent = new StringContent(inputModel.coUniqueID);
                StringContent newsUniqueIDContent = new StringContent(inputModel.newsUniqueID);
                StringContent volUniqueIDContent = new StringContent(inputModel.volUniqueID);
                StringContent newsTitleContent = new StringContent(inputModel.newsTitle);
                StringContent newsDescriptionContent = new StringContent(inputModel.newsContent);
                StringContent newsDateTimeContent = new DateTime(inputModel.newsDateTime);
                StringContent newsPostedTimeContent = new DateTime(inputModel.newsPostedTime);
                StringContent newsPrivacyContent = new StringContent(inputModel.newsPrivacy);
                StringContent newsStatusContent = new StringContent(inputModel.newsStatus);
                StringContent newsOriginContent = new StringContent(inputModel.newsOrigin);
                StringContent newsFileOriginalContent = new StringContent(inputModel.newsFileOriginal);

                Mcontent.Add(coUniqueIDContent, nameof(inputModel.coUniqueID));
                Mcontent.Add(newsUniqueIDContent, nameof(inputModel.newsUniqueID));
                Mcontent.Add(volUniqueIDContent, nameof(inputModel.volUniqueID));
                Mcontent.Add(newsTitleContent, nameof(inputModel.newsTitle));
                Mcontent.Add(newsDescriptionContent, nameof(inputModel.newsContent));
                Mcontent.Add(newsDateTimeContent, nameof(inputModel.newsDateTime));
                Mcontent.Add(newsPostedTimeContent, nameof(inputModel.newsPostedTime));
                Mcontent.Add(newsPrivacyContent, nameof(inputModel.newsPrivacy));
                Mcontent.Add(newsStatusContent, nameof(inputModel.newsStatus));
                Mcontent.Add(newsOriginContent, nameof(inputModel.newsOrigin));
                Mcontent.Add(newsFileOriginalContent, nameof(inputModel.newsFileOriginal));
                */

                var jData = JsonConvert.SerializeObject(inputModel);
                var content1 = new StringContent(jData, Encoding.UTF8, "application/json");
                Mcontent.Add(content1, "Data");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);

                var response = await client.PostAsync(serviceUrl, Mcontent);
                resultModel = await HandleServerResponse<T>(response);
                return resultModel;
            }
            catch (Exception exception)
            {
                return HandleException<T>(exception);
            }
        }

        /// <summary>
        /// HandleServerResponse
        /// </summary>
        /// <param name="response">API Response</param>
        /// <returns></returns>        
        private static async Task<BaseResponse<T>> HandleServerResponse<T>(HttpResponseMessage response)
        {
            try
            {
                BaseResponse<T> resultModel = new BaseResponse<T>();

                if (response != null)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("\n\nResponse : " + result);

                    if (response.IsSuccessStatusCode)
                    {
                        if (typeof(T) != typeof(string))
                        {
                            var responseBody = JsonConvert.DeserializeObject<T>(result);
                            var responseMessage = JsonConvert.DeserializeObject<Message>(result);
                            if (responseMessage != null && !string.IsNullOrEmpty(responseMessage.message))
                            {
                                resultModel.Body = default(T);
                                resultModel.Message = responseMessage;
                                resultModel.Result = ResponseStatus.Ok;
                            }
                            else
                            {
                                resultModel.Body = responseBody;
                                resultModel.Message = null;
                                resultModel.Result = ResponseStatus.Ok;
                            }
                        }
                        else
                        {
                            resultModel.Body = (T)Convert.ChangeType(result, typeof(T));
                            resultModel.Message = null;
                            resultModel.Result = ResponseStatus.Ok;
                        }
                    }
                    else
                    {

                        Message messageModel = new Message();
                        if ((int)response.StatusCode == 500)
                        {
                            resultModel.Result = ResponseStatus.Failed;
                            messageModel.message = AppResources.InternalServerErrorMsg;
                        }
                        else if ((int)response.StatusCode == 401)
                        {
                            resultModel.Result = ResponseStatus.Unauthorized;
                            messageModel.message = AppResources.UnauthorizedErrorMsg;
                        }
                        else if ((int)response.StatusCode == 408)
                        {
                            resultModel.Result = ResponseStatus.Failed;
                            messageModel.message = AppResources.RequestTimeoutErrorMsg;
                        }
                        else
                        {
                            var responseMessage = JsonConvert.DeserializeObject<Message>(result);
                            if (responseMessage != null && !string.IsNullOrEmpty(responseMessage.message))
                            {
                                resultModel.Result = ResponseStatus.Failed;
                                if (responseMessage.message == TextResources.TooManyAttemptsText)
                                {
                                    responseMessage.message = AppResources.BackendErrorText;
                                }
                                messageModel = responseMessage;
                            }
                            else
                            {
                                resultModel.Result = ResponseStatus.None;
                                messageModel.message = AppResources.SomethingWentWrongText;
                            }
                        }
                        resultModel.Body = default(T);
                        resultModel.Message = messageModel;
                    }
                }
                else
                {
                    resultModel.Result = ResponseStatus.None;
                    resultModel.Message = new Message() { message = AppResources.SomethingWentWrongText };
                }
                return resultModel;
            }
            catch (Exception exception)
            {
                return HandleException<T>(exception);
            }
        }


        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns></returns>
        private static BaseResponse<T> HandleException<T>(Exception exception)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            resultModel.Result = ResponseStatus.None;
            resultModel.Message = new Message() { message = AppResources.SomethingWentWrongText };
            return resultModel;
        }
    }
}
