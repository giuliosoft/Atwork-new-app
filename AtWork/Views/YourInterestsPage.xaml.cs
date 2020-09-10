using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class YourInterestsPage : ContentPage
    {
        YourInterestsPageViewModel VMContext;
        public YourInterestsPage()
        {
            InitializeComponent();
            VMContext = ((YourInterestsPageViewModel)this.BindingContext);
        }

        void commentEditor_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
        }

        void commentEditor_TextChanged_1(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
        }

        void commentEditor_TextChanged_2(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (VMContext == null)
                return;
            string editorText = sender as string;
            if (!string.IsNullOrEmpty(commentEditor.Text))
            {
                VMContext.SendButtonIsVisible = true;
            }
            else
            {
                VMContext.SendButtonIsVisible = false;
            }
        }
    }
}
