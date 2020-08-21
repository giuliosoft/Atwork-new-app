using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace AtWork.Services
{
    public class PopupNavigationService
    {
        static IPopupNavigation _instance;
        public static IPopupNavigation Instance
        {
            get
            {
                return _instance ?? (_instance = PopupNavigation.Instance);
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// ShowLoader
        /// </summary>
        /// <param name="animate">Pass true/false for animate the popup view</param>
        /// <returns></returns>
        public static async Task ShowPopup(PopupPage popup, bool animate = false)
        {
            try
            {
                await Instance.PushAsync(popup, animate);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ClosePopup
        /// </summary>
        /// <param name="animate">Pass true/false for animate the popup view</param>
        /// <returns></returns>
        public static async Task ClosePopup(bool animate = false)
        {
            try
            {
                if (Instance.PopupStack != null && Instance.PopupStack.Count > 0)
                {
                    await Instance.PopAsync(animate);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
