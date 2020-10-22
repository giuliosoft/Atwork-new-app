using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AtWork.Popups
{
    public partial class LoadingPopup : PopupPage
    {
        public LoadingPopup()
        {
            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        async void Handle_OnFinish(object sender, System.EventArgs e)
        {
            await ViewModelBase.ClosePopup();
        }
    }
}
