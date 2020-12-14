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

        void Switch_ToggledCompany(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (VMContext != null)
                {
                   // if (!VMContext.isTaponEveryone)
                    {
                        VMContext.FromYourCompany(e.Value);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        void Switch_ToggledGroup(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (VMContext != null)
                {
                   // if (!VMContext.isTaponEveryone)
                    {
                        VMContext.FromYourGroup(e.Value);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        void Switch_ToggledEveryone(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (VMContext != null)
                {
                    //if (e.Value)
                    {
                        VMContext.FromEveryone(e.Value);
                        //VMContext.isTaponEveryone = true;
                        //VMContext.isTaponCompany = false;
                        //VMContext.isTaponGroup = false;

                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }

    }
}
