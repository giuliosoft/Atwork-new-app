using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class ManageNotificationConnectPage : ContentPage
    {
        ManageNotificationConnectPageViewModel VMContext;
        public ManageNotificationConnectPage()
        {
            InitializeComponent();
            VMContext = ((ManageNotificationConnectPageViewModel)this.BindingContext);
        }

        //void Switch_ToggledCompany(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        //{

        //}

        //void Switch_ToggledGroup(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        //{

        //}

        //void Switch_ToggledEveryone(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        //{
        //    //    if (VMContext != null)
        //    //    {
        //    //        if (e.Value)
        //    //        {
        //    //            VMContext.isTaponEveryone = true;
        //    //        }
        //    //    }
        //}

    }
}
