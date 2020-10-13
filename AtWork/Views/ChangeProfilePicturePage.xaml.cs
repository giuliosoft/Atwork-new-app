using System;
using System.Collections.Generic;
using System.Diagnostics;
using AtWork.Helpers;
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
                    VMContext.ProfileImage = null;
                    VMContext.ProfileImage = ImageSource.FromFile(VMContext.SelectedNewsImageValue.ImagePath);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}
