using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class ManageNotificationPage : ContentPage
    {
        ManageNotificationPageViewModel VMContext;
        public ManageNotificationPage()
        {
            InitializeComponent();
            VMContext = ((ManageNotificationPageViewModel)this.BindingContext);
        }

        async void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (VMContext != null)
            {
                await VMContext.OpenNotificationDialog();
            }
        }
    }
}
