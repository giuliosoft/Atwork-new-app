using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class DisclaimerPage : ContentPage
    {
        DisclaimerPageViewModel VMContext;
        public DisclaimerPage()
        {
            InitializeComponent();
            VMContext = ((DisclaimerPageViewModel)this.BindingContext);
            VMContext.pageObject = this;
        }

        void ScrollView_Scrolled(System.Object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            if (VMContext == null)
                return;

            VMContext.isEnableDisclamerButton = true;
            VMContext.ButtonColor = (Color)App.Current.Resources["WhiteColor"];
            
        }
    }
}
