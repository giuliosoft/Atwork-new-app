using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void entEmail_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            btnLogin.VerticalOptions = LayoutOptions.StartAndExpand;
            //slBtnLogin.VerticalOptions = LayoutOptions.StartAndExpand;

        }

        void entEmail_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            btnLogin.VerticalOptions = LayoutOptions.Start;
            //slBtnLogin.VerticalOptions = LayoutOptions.Start;
        }
    }
}
