using System;
using System.Collections.Generic;
using AtWork.Services;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class TouchIDLoginPage : ContentPage
    {
        public TouchIDLoginPage()
        {
            InitializeComponent();
            AnimationService.ScaleAllToZero(new List<VisualElement>() {
               biomatricImage
            }) ;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationService.ScaleAllToOne(new List<VisualElement>() {
               biomatricImage
            });
        }
    }
}
