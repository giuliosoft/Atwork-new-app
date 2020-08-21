using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using Newtonsoft.Json;

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
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SettingsService.AuthorizationToken);
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
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SettingsService.AuthorizationToken);
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
