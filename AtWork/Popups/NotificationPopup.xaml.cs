using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AtWork.Popups
{
    public partial class NotificationPopup : PopupPage
    {
        public NotificationPopup()
        {
            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
