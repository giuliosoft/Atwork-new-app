using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.UserModel;
using static AtWork.Models.UserSettingModel;

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
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetUserDetails(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var UserProfileURL = ConfigService.BaseServiceURL + ConfigService.getprofile_v1 + id;
                resultModel = await GetResponse<string>(UserProfileURL, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
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
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UpdateUserLanguage(Volunteers inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateLanguageServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetAboutUserInfo()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var aboutUserServiceUrl = ConfigService.BaseServiceURL + ConfigService.AboutUserServiceURL;
                resultModel = await GetResponse<string>(aboutUserServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> UpdateAboutUserInfo(UserSettingInputModel inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateAboutUserServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetInterestsDetails()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var InterestsServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetInterestsServiceURL;
                resultModel = await GetResponse<string>(InterestsServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UpdateInterestsDetail(Volunteers inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateInterestsServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        //public static async Task<BaseResponse<string>> GetAboutUserInfo()
        //{
        //    BaseResponse<string> resultModel = new BaseResponse<string>();
        //    try
        //    {
        //        var aboutUserServiceUrl = ConfigService.BaseServiceURL + ConfigService.AboutUserServiceURL;
        //        resultModel = await GetResponse<string>(aboutUserServiceUrl, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultModel.Result = ResponseStatus.None;
        //        ExceptionHelper.CommanException(ex);
        //    }
        //    return resultModel;
        //}

        public static async Task<BaseResponse<string>> UpdateAboutUserInfo(Volunteers inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserInfoServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateAboutUserServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserInfoServiceUrl, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> GetUserProfilePicture()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var userProfileServiceUrl = ConfigService.BaseServiceURL + ConfigService.GetUserProfilePicServiceURL;
                resultModel = await GetResponse<string>(userProfileServiceUrl, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> UpdateProfilePicture(Volunteers inputModel, List<string> profilePicFile)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserProfileImageServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateUserProfilePicServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await FilePostResponse<string>(updateUserProfileImageServiceUrl, profilePicFile, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> ChangeUserPassword(Volunteers inputModel, bool isAddAuthToken = true)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var updateUserPasswordServiceUrl = ConfigService.BaseServiceURL + ConfigService.UpdateUserPasswordServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(updateUserPasswordServiceUrl, jData, isAddAuthToken);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> ClaimUserProfile(string inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var claimUserProfileServiceURL = ConfigService.BaseServiceURL + ConfigService.ClaimUserProfileServiceURL;
                resultModel = await GetResponse<string>($"{claimUserProfileServiceURL}{inputModel}", false);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }

        public static async Task<BaseResponse<string>> SubmitUserProfileCorrections(ProfileCorrectionInputModel inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var profileCorrectionServiceURL = ConfigService.BaseServiceURL + ConfigService.UserProfileCorrectionServiceURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(profileCorrectionServiceURL, jData, false);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UserForgotPassword(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var forgotpasswordServiceURL = ConfigService.BaseServiceURL + ConfigService.UserForgotPasswordServiceURL + id;
                resultModel = await GetResponse<string>(forgotpasswordServiceURL, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetUserByGroup(string id)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var Url = ConfigService.BaseServiceURL + ConfigService.GetUserByGroupServiceURL + id;
                resultModel = await GetResponse<string>(Url, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> GetBirthDate()
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var Url = ConfigService.BaseServiceURL + ConfigService.UserBirthdayURL;
                resultModel = await GetResponse<string>(Url, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
        public static async Task<BaseResponse<string>> UpdateBirthDate(VolunteerBirthday inputModel)
        {
            BaseResponse<string> resultModel = new BaseResponse<string>();
            try
            {
                var Url = ConfigService.BaseServiceURL + ConfigService.UpdateUserBirthdayURL;
                var jData = JsonConvert.SerializeObject(inputModel);
                resultModel = await PostResponse<string>(Url, jData, true);
            }
            catch (Exception ex)
            {
                resultModel.Result = ResponseStatus.None;
                ExceptionHelper.CommanException(ex);
            }
            return resultModel;
        }
    }
}
