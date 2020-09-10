using System;
using System.Collections.Generic;
using System.Diagnostics;
using AtWork.Services;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class ChangeProfilePicturePage : ContentPage
    {
        ChangeProfilePicturePageViewModel VMContext;
        public ChangeProfilePicturePage()
        {
            InitializeComponent();
            VMContext = ((ChangeProfilePicturePageViewModel)this.BindingContext);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (SessionService.isImageCropped)
                {
                    SessionService.isImageCropped = false;
                    //var temp = VMContext.SelectedNewsImageValue;
                    VMContext.ProfileImage = null;
                    VMContext.ProfileImage = ImageSource.FromFile(VMContext.SelectedNewsImageValue.ImagePath);
                    //VMContext.SelectedNewsImageValue = null;
                    //VMContext.SelectedNewsImageValue = temp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
