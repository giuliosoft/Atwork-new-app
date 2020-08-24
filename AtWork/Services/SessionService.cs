using System;
using System.Threading.Tasks;
using Prism.Navigation;

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
        public static bool isEditNews = false;

        /// <summary>
        /// Logout
        /// clear social media account details
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task Logout()
        {
            SessionService.AppNavigationService = null;
        }

        #endregion
    }
}
