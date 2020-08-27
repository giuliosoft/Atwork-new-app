using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AtWork.Services;
using AtWork.ViewModels;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class CropImagePage : ContentPage
    {
        CropImagePageViewModel VMContext;
        IHelper _helperService;
        public Stream croppedImageStream = null;
        public CropImagePage()
        {
            InitializeComponent();
            VMContext = ((CropImagePageViewModel)this.BindingContext);
            VMContext.pageObject = this;
            _helperService = DependencyService.Get<IHelper>();
        }

        private void editor_ImageSaving(object sender, ImageSavingEventArgs args)
        {
            croppedImageStream = args.Stream;
            //ImageSource image = ImageSource.FromStream(() => croppedImageStream);
        }

        private async void editor_ImageSaved(object sender, ImageSavedEventArgs args)
        {
            try
            {
                string savedLocation = args.Location;
                await _helperService.SaveImageFile(croppedImageStream, VMContext.SelectedNewsImageValue.ImagePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
