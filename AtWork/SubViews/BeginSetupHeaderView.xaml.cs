using System;
using System.Collections.Generic;
using System.Linq;
using AtWork.Services;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.SubViews
{
    public partial class BeginSetupHeaderView : ContentView
    {
        public BeginSetupHeaderView()
        {
            InitializeComponent();
            Content.SizeChanged += (object sender, EventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine(Content.Width);
                switch (SessionService.CurrentTab)
                {
                    case 1:
                        tab1.IsVisible = false;
                        break;
                    case 2:
                        tab1.IsVisible = tab2.IsVisible = false;
                        break;
                    case 3:
                        tab1.IsVisible = tab2.IsVisible = tab3.IsVisible = false;
                        break;
                    case 4:
                        tab1.IsVisible = tab2.IsVisible = tab3.IsVisible = tab4.IsVisible = false;
                        break;
                    case 5:
                        tab1.IsVisible = tab2.IsVisible = tab3.IsVisible = tab4.IsVisible = tab5.IsVisible = false;
                        break;
                }
                if (SessionService.CurrentTab > 0)
                    tabBack.WidthRequest += (Content.Width - 90 - 40) / 5 * SessionService.CurrentTab;
                tab1.WidthRequest = tab2.WidthRequest = tab3.WidthRequest = tab4.WidthRequest = tab5.WidthRequest = (Content.Width - 90 - 40) / 5;
            };
        }
    }
}
