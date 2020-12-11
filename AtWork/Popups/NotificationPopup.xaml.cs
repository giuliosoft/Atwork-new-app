using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AtWork.Popups
{
    public partial class NotificationPopup : PopupPage
    {
        NotificationPopupViewModel VMContext;
        public NotificationPopup()
        {
            InitializeComponent();
            VMContext = ((NotificationPopupViewModel)this.BindingContext);
        }

        //protected override  bool OnBackgroundClicked()
        //{
        //    return true;
        //}

        //protected override bool OnBackButtonPressed()
        //{
        //    return false;
        //}
    }
}
