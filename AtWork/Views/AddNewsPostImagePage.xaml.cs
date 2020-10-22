using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AtWork.Helpers;
using AtWork.Services;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (SessionService.isImageCropped)
                {
                    SessionService.isImageCropped = false;
                    var tempCList = new ObservableCollection<NewsImageModel>();
                    foreach (var nItem in VMContext.NewsPostImageCarouselList)
                    {
                        tempCList.Add(new NewsImageModel() { ImagePath = nItem.ImagePath, ImagePreviewPath = nItem.ImagePreviewPath, FileType = nItem.FileType, NewsImage = ImageSource.FromFile(nItem.ImagePreviewPath) });
                    }
                    VMContext.NewsPostImageCarouselList = new ObservableCollection<NewsImageModel>(tempCList);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}
