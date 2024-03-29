﻿using System;
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
using AtWork.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
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

                    //Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);
                    Debug.WriteLine("\n\nAPI : " + serviceUrl);

                    if (isAddAuthorizationToken)
                    {
                        Debug.WriteLine("\n\nTKN : " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}"))); 
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
                    //Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);
                    Debug.WriteLine("\n\nAPI : " + serviceUrl);
                    Debug.WriteLine("\n\nRequest : " + jData);

                    if (isAddAuthorizationToken)
                    {
                        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SettingsService.AuthorizationToken);
                        Debug.WriteLine("\n\nTKN : " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}")));
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
                        Debug.WriteLine("\n\nTKN : " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{loginInputModel.email}:{loginInputModel.password}")));
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
        public static async Task<BaseResponse<T>> FilePostResponse<T>(string serviceUrl, List<string> fileToAttachList, string jData, bool isAddAuthorizationToken)
        {
            BaseResponse<T> resultModel = new BaseResponse<T>();
            try
            {
                var client = new HttpClient();
                if (isAddAuthorizationToken)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{SettingsService.LoggedInUserEmail}:{SettingsService.LoggedInUserPassword}")));
                }
                Debug.WriteLine("\n\nAPI : " + ConfigService.BaseServiceURL + serviceUrl);
                Debug.WriteLine("\n\nRequest : " + jData);

                string boundary = "---8d0f01e6b3b5dafaaadaad";
                MultipartFormDataContent Mcontent = new MultipartFormDataContent(boundary);
                if (fileToAttachList != null && fileToAttachList.Count > 0)
                {
                    for (var arg = 0; arg < fileToAttachList.Count; arg++)
                    {
                        try
                        {
                            var fileContentsInBytes1 = File.ReadAllBytes(fileToAttachList[arg]);
                            ByteArrayContent baContent = new ByteArrayContent(fileContentsInBytes1);
                            baContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                            baContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = Path.GetFileName(fileToAttachList[arg]),
                                FileName = Path.GetFileName(fileToAttachList[arg])
                            };
                            Mcontent.Add(baContent);//fileToAttachList[arg]), Path.GetFileName(fileToAttachList[arg]));
                        }
                        catch (Exception ex)
                        {
                            ExceptionHelper.CommanException(ex);
                        }
                    }
                }
                
                var content1 = new StringContent(jData, Encoding.UTF8, "application/json");
                Mcontent.Add(content1, "Data");
                client.Timeout = new TimeSpan(0, 10, 0);
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
                            //SessionService.Logout();
                           // await SessionService.AppNavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
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
