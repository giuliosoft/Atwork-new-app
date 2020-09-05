using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AtWork.SubViews
{
    public partial class DashboardFooterView : ContentView
    {
        public DashboardFooterView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "GetActivityTabSelected", (sender) =>
              {
                  GreenView.TranslateTo((double)App.Current.Resources["DeviceWidthFooterTab"], 0, 300);
                  MessagingCenter.Unsubscribe<object>(this, "GetActivityTabSelected");
              });
        }

        void TapGestureRecognizer_News(System.Object sender, System.EventArgs e)
        {
            GreenView.TranslateTo(0, 0, 300);
        }

        void TapGestureRecognizer_Activity(System.Object sender, System.EventArgs e)
        {
            GreenView.TranslateTo((double)App.Current.Resources["DeviceWidthFooterTab"], 0, 300);
        }
    }
}
