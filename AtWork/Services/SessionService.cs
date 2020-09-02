using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
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
        public static List<string> NewsPostImageFiles = new List<string>();
        public static string NewsPostAttachmentFilePath = string.Empty;
        public static string NewsPostAttachmentFileName = string.Empty;
        public static List<string> NewsPostCarouselImages = new List<string>();
        /// <summary>
        /// Logout
        /// clear social media account details
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task Logout()
        {
            SessionService.NewsPostInputData = null;
            SessionService.AppNavigationService = null;
            SettingsService.LoggedInUserData = null;
        }

        #endregion
    }
}
