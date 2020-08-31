using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class CropImagePageViewModel : ViewModelBase
    {
        #region Constructor
        public CropImagePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(CropImagePage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            _helperService = DependencyService.Get<IHelper>();
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ImageSource _NewsImageToCrop = string.Empty;
        private NewsImageModel _SelectedNewsImageValue = null;
        IHelper _helperService;
        public Stream CroppedImageStream;
        #endregion

        #region Public Properties
        public CropImagePage pageObject;
        public string CroppedImageFilePath;
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ImageSource NewsImageToCrop
        {
            get { return _NewsImageToCrop; }
            set { SetProperty(ref _NewsImageToCrop, value); }
        }

        public NewsImageModel SelectedNewsImageValue
        {
            get { return _SelectedNewsImageValue; }
            set { SetProperty(ref _SelectedNewsImageValue, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
        #endregion

        #region private methods
        async Task GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                if (pageObject != null)
                {
                    var imgEditor = pageObject.FindByName("sfImageEditor") as SfImageEditor;
                    if (imgEditor != null)
                    {
                        imgEditor.ImageSaving -= editor_ImageSaving;
                        imgEditor.ImageSaving += editor_ImageSaving;
                        imgEditor.Save();
                        //imgEditor.ImageSaved -= editor_ImageSaved;
                        //imgEditor.ImageSaved += editor_ImageSaved;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void editor_ImageSaving(object sender, ImageSavingEventArgs args)
        {
            try
            {
                await ShowLoader(true);
                CroppedImageStream = args.Stream;
                //ImageSource image = ImageSource.FromStream(() => croppedImageStream);
                if (CroppedImageStream != null)
                {
                    CroppedImageFilePath = await _helperService.SaveImageFile(CroppedImageStream, SelectedNewsImageValue.ImagePath);
                }
                if (!string.IsNullOrEmpty(CroppedImageFilePath))
                {
                    await _helperService.ReplaceCroppedFile(CroppedImageFilePath, SelectedNewsImageValue.ImagePath);
                    SessionService.isImageCropped = true;
                }
                await BackClick();
                await ClosePopup(true);
            }
            catch (Exception ex)
            {
                await ClosePopup(true);
                Debug.WriteLine(ex.Message);
            }
        }

        private async void editor_ImageSaved(object sender, ImageSavedEventArgs args)
        {
            try
            {
                string savedLocation = args.Location;
                //var croppedImgFilePath = await _helperService.SaveImageFile(croppedImageStream, VMContext.SelectedNewsImageValue.ImagePath);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region public methods

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SelectedNewsImageValue = parameters.GetValue<NewsImageModel>("SelectedNewsImage");
            if (SelectedNewsImageValue != null)
            {
                NewsImageToCrop = SelectedNewsImageValue.ImagePreviewPath;
            }
        }
    }
}
