﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AtWork.SubViews
{
    public partial class DashboardFooterView : ContentView
    {
        public DashboardFooterView()
        {
            InitializeComponent();
            Content.SizeChanged += (object sender, EventArgs e) =>
            {
                GreenView.WidthRequest = Content.Width / 2;
                NewsLabel.WidthRequest = Content.Width / 2;
                ActivityLabel.WidthRequest = Content.Width / 2;
            };

            MessagingCenter.Subscribe<object>(this, "GetActivityTabSelected", (sender) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(100);
                    await GreenView.TranslateTo(GreenView.WidthRequest, 0, 300);
                    MessagingCenter.Unsubscribe<object>(this, "GetActivityTabSelected");
                });
            });
        }

        void TapGestureRecognizer_News(System.Object sender, System.EventArgs e)
        {
            GreenView.TranslateTo(0, 0, 300);
        }

        void TapGestureRecognizer_Activity(System.Object sender, System.EventArgs e)
        {
            GreenView.TranslateTo(GreenView.WidthRequest, 0, 300);
        }
    }
}
