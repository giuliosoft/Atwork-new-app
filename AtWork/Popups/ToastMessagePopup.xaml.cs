using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AtWork.Popups
{
    public partial class ToastMessagePopup : PopupPage
    {
        public ToastMessagePopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await ViewModelBase.ClosePopup(true);
                });
                return false;
            });
        }
    }
}
