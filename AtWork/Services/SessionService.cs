using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.ActivityModel;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Services
{
    public class SessionService
    {
        #region private fields
        static string _Token;
        #endregion

        #region public properties

        public static int SelectedFloorID = 0;
        public static INavigationService AppNavigationService = null;
        public static bool IsOnGlueGuidePage = false;
        public static bool isLoadingPopupOpen = false;
        public static bool isEditingNews = false;
        public static bool isImageCropped = false;
        public static NewsDetailModel_Input NewsPostInputData = new NewsDetailModel_Input();
        public static ActivityListModel ActivityPostInputData = new ActivityListModel();
        public static List<string> NewsPostImageFiles = new List<string>();
        public static string NewsPostAttachmentFilePath = string.Empty;
        public static string NewsPostAttachmentFileName = string.Empty;
        public static List<string> NewsPostCarouselImages = new List<string>();
        public static string DeletedNewsPost = string.Empty;
        public static bool IsNeedToRefreshNews = false;
        public static bool IsShowActivitiesIntial = false;
        public static List<string> CreateActivityOurImages = new List<string>();
        public static string SelectedDefaultImageForActivity = string.Empty;
        public static int? LikeNewsID;
        public static int? LikeNewsCount;
        public static bool IsWelcomeSetup = false;
        public static int CurrentTab = 0;
        public static int GroupMemberCount = 0;
        public static bool isFromChangeUserProfile = false;
        public static bool isFromClaimProfile = false;
        public static Volunteers tempVolunteerData = null;
        public static LoginOutputModel tempClaimProfileData = null;

        /// <summary>
        /// Logout
        /// clear social media account details
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task Logout()
        {
            SessionService.tempVolunteerData = null;
            SessionService.tempClaimProfileData = null;
            SessionService.NewsPostInputData = new NewsDetailModel_Input();
            SessionService.AppNavigationService = null;
            SettingsService.LoggedInUserData = null;
            SettingsService.VolunteersUserData = null;
            SettingsService.LoggedInUserEmail = string.Empty;
            SettingsService.LoggedInUserPassword = string.Empty;
        }

        #endregion
    }
}
