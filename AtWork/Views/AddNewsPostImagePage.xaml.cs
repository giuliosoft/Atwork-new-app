using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class AddNewsPostImagePage : ContentPage
    {
        AddNewsPostImagePageViewModel VMContext;
        public AddNewsPostImagePage()
        {
            InitializeComponent();
            VMContext = ((AddNewsPostImagePageViewModel)this.BindingContext);
            VMContext.pageObject = this;
        }

        void newsImageCarousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
        {

        }
    }
}
