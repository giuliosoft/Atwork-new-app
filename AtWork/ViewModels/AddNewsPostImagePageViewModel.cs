using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class AddNewsPostImagePageViewModel : ViewModelBase
    {
        bool isActivity = false;
        #region Constructor
        public AddNewsPostImagePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            try
            {
                NextClickPageName = nameof(AddNewsPostImagePage);
                _multiMediaPickerService = DependencyService.Get<IMultiMediaPickerService>();
                AddNewsCancelImage = AppResources.BackButtonText;
                AddNewsNextImage = AppResources.NextButtonText;
                NextTextColor = (Color)App.Current.Resources["ShadedWhiteColor"];
                HeaderNextNavigationCommand = NewsPostProceedCommand;

                _helperService = DependencyService.Get<IHelper>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Private Properties
        IMultiMediaPickerService _multiMediaPickerService;
        private ObservableCollection<NewsImageModel> _NewsPostImageCarouselList = new ObservableCollection<NewsImageModel>();
        private string _Prop = string.Empty;
        private bool _NewsPickedImageViewIsVisible = false;
        private bool _IsShowOurImage = false;
        private int NewsImageSelectedForCrop = -1;
        private string _ImageOptionText = AppResources.EditCropButtonText;
        private string _AddImagesTitle = AppResources.AddImagesToYourPost;
        private bool _ShowPickOgOurImage = false;
        private int CarouselPosition = 0;
        private string _SelectedDefaultImage = string.Empty;
        private ImageSource _OurSelectedImage = string.Empty;
        IHelper _helperService;
        #endregion

        #region Public Properties
        public AddNewsPostImagePage pageObject;
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ObservableCollection<NewsImageModel> NewsPostImageCarouselList
        {
            get { return _NewsPostImageCarouselList; }
            set { SetProperty(ref _NewsPostImageCarouselList, value); }
        }

        public bool NewsPickedImageViewIsVisible
        {
            get { return _NewsPickedImageViewIsVisible; }
            set { SetProperty(ref _NewsPickedImageViewIsVisible, value); }
        }
        public bool IsShowOurImage
        {
            get { return _IsShowOurImage; }
            set { SetProperty(ref _IsShowOurImage, value); }
        }

        public string ImageOptionText
        {
            get { return _ImageOptionText; }
            set { SetProperty(ref _ImageOptionText, value); }
        }
        public string AddImagesTitle
        {
            get { return _AddImagesTitle; }
            set { SetProperty(ref _AddImagesTitle, value); }
        }
        public bool ShowPickOgOurImage
        {
            get { return _ShowPickOgOurImage; }
            set { SetProperty(ref _ShowPickOgOurImage, value); }
        }
        public string SelectedDefaultImage
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }

        public ImageSource OurSelectedImage
        {
            get { return _OurSelectedImage; }
            set { SetProperty(ref _OurSelectedImage, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand AddImagesFromGalleryCommand { get { return new DelegateCommand(async () => await AddImagesFromGallery()); } }
        public DelegateCommand OurImagesCommand { get { return new DelegateCommand(async () => await OurImages()); } }
        public DelegateCommand<string> CropNewsImageCommand { get { return new DelegateCommand<string>(async (obj) => await CropNewsImage(obj)); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
        #endregion

        #region private methods
        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                if ((NewsPostImageCarouselList != null && NewsPostImageCarouselList.Count == 0 || NewsPostImageCarouselList.Count > 5) && string.IsNullOrEmpty(SelectedDefaultImage))
                {
                    await DisplayAlertAsync(AppResources.ImageSelectionAlertText);
                    return;
                }
                else
                {
                    NextTextColor = (Color)App.Current.Resources["WhiteColor"];
                    await ShowLoader();
                    List<string> PostImageFiles = new List<string>();
                    NewsPostImageCarouselList?.All((arg) =>
                    {
                        if (!string.IsNullOrEmpty(arg.ImagePath))
                        {
                            PostImageFiles.Add(arg.ImagePath);
                        }
                        return true;
                    });
                    if (!string.IsNullOrEmpty(SelectedDefaultImage))
                    {
                        SessionService.SelectedDefaultImageForActivity = SelectedDefaultImage;
                    }
                    else
                    {
                        SessionService.NewsPostImageFiles = new List<string>(PostImageFiles);
                    }
                    if (isActivity)
                    {
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("isFromActivity", true);
                        await _navigationService.NavigateAsync(nameof(PostNewsPage), navigationParams);
                    }
                    else
                    {
                        await _navigationService.NavigateAsync(nameof(AddNewsAttachFilePage));
                    }
                    await ClosePopup();
                }
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
        }

        async Task AddImagesFromGallery()
        {
            try
            {
                var hasPermission = await TakePermissionsToPickPhoto();
                if (hasPermission)
                {
                    var res = await _multiMediaPickerService.PickPhotosAsync();
                    if (res != null && res.Count > 0)
                    {
                        //if (isActivity)
                        //{
                        NewsPostImageCarouselList.Clear();
                        SessionService.NewsPostCarouselImages = new List<string>();
                        //}
                        if (res.Count <= 5)
                        {
                            NextTextColor = (Color)App.Current.Resources["WhiteColor"];
                        }
                        NewsPickedImageViewIsVisible = true;
                        SelectedDefaultImage = string.Empty;
                        SessionService.SelectedDefaultImageForActivity = string.Empty;
                        //Original for taking original path that is picked :
                        //res.All((mFile) =>
                        //{                            
                        //    NewsPostImageCarouselList.Add(new NewsImageModel() { ImagePath = mFile.Path, ImagePreviewPath = mFile.PreviewPath, FileType = mFile.Type, NewsImage = ImageSource.FromFile(mFile.PreviewPath) });
                        //    return true;
                        //});

                        //New for saving file at project directory and use it :
                        foreach (var mFile in res)
                        {
                            Stream selectedImgStream = await _helperService.RetriveCompressedImageStreamFromLocation(mFile.Path);
                            if (selectedImgStream != null)
                            {
                                var newProfileImagePath = await _helperService.SaveProfileImage(selectedImgStream);
                                if (!string.IsNullOrEmpty(newProfileImagePath))
                                {
                                    NewsPostImageCarouselList.Add(new NewsImageModel() { ImagePath = newProfileImagePath, ImagePreviewPath = newProfileImagePath, NewsImage = ImageSource.FromFile(newProfileImagePath) });
                                }
                            }
                        }
                        IsShowOurImage = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task OurImages()
        {
            try
            {
                ActivityImagePopup activityImagePopup = new ActivityImagePopup();
                ActivityImagePopupViewModel activityImagePopupViewModel = new ActivityImagePopupViewModel(_navigationService, _facadeService);
                activityImagePopupViewModel.SelectedImageSourceEvent1 += async (string arg1, ImageSource arg2) =>
                {
                    NextTextColor = (Color)App.Current.Resources["WhiteColor"];
                    NewsPostImageCarouselList.Clear();
                    SessionService.NewsPostCarouselImages = new List<string>();
                    NewsPickedImageViewIsVisible = false;
                    IsShowOurImage = true;
                    SelectedDefaultImage = arg1;
                    OurSelectedImage = arg2;
                };
                //activityImagePopupViewModel.SelectedImageSourceEvent += async (object sender, string SelectedObj) =>
                //{
                //    try
                //    {
                //        NextTextColor = (Color)App.Current.Resources["WhiteColor"];
                //        NewsPostImageCarouselList.Clear();
                //        NewsPickedImageViewIsVisible = false;
                //        IsShowOurImage = true;
                //        SelectedDefaultImage = SelectedObj;
                //        //NewsPostImageCarouselList.Add(new NewsImageModel() {  NewsImage = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + SelectedDefaultImage)) });
                //        OurSelectedImage = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + SelectedDefaultImage));
                //    }
                //    catch (Exception ex)
                //    {
                //        Debug.WriteLine(ex.Message);
                //    }
                //};
                activityImagePopup.BindingContext = activityImagePopupViewModel;
                await PopupNavigationService.ShowPopup(activityImagePopup, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ActivityImagePopupViewModel_SelectedImageSourceEvent1(string arg1, ImageSource arg2)
        {
            throw new NotImplementedException();
        }

        async Task CropNewsImage(string selectedOption)
        {
            try
            {
                if (pageObject != null)
                {
                    var carouselRef = pageObject.FindByName("newsImageCarousel") as CarouselView;
                    if (selectedOption == AppResources.Delete)
                    {
                        if (carouselRef != null)
                        {
                            var pos = carouselRef.Position;
                            var tmpList = new ObservableCollection<NewsImageModel>(NewsPostImageCarouselList);
                            tmpList.RemoveAt(pos);
                            NewsPostImageCarouselList = new ObservableCollection<NewsImageModel>(tmpList);
                            SessionService.NewsPostCarouselImages.RemoveAt(pos);
                            if (NewsPostImageCarouselList != null)
                            {
                                if (NewsPostImageCarouselList.Count > 0)
                                {
                                    if (NewsPostImageCarouselList[CarouselPosition] != null)
                                    {
                                        if (!string.IsNullOrEmpty(NewsPostImageCarouselList[CarouselPosition].ImagePath))
                                        {
                                            ImageOptionText = AppResources.EditCropButtonText;
                                        }
                                    }
                                }
                                else if (NewsPostImageCarouselList.Count == 0)
                                {
                                    NextTextColor = (Color)App.Current.Resources["ShadedWhiteColor"];
                                    NewsPickedImageViewIsVisible = false;
                                    ImageOptionText = AppResources.EditCropButtonText;
                                }
                            }
                        }
                    }
                    else if (selectedOption == AppResources.EditCropButtonText)
                    {
                        if (carouselRef != null)
                        {
                            NewsImageSelectedForCrop = carouselRef.Position;
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("ImagePath", NewsPostImageCarouselList[NewsImageSelectedForCrop].ImagePreviewPath);
                            navigationParams.Add("SelectedNewsImage", NewsPostImageCarouselList[NewsImageSelectedForCrop]);
                            await _navigationService.NavigateAsync(nameof(CropImagePage), navigationParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void Carousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
        {
            try
            {
                var control = sender as CarouselView;
                CarouselPosition = control.Position;
                if (NewsPostImageCarouselList.Count > 0 && string.IsNullOrEmpty(NewsPostImageCarouselList[control.Position].ImagePath))
                {
                    ImageOptionText = AppResources.Delete;
                }
                else
                {
                    ImageOptionText = AppResources.EditCropButtonText;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region public methods        
        async Task<bool> TakePermissionsToPickPhoto()
        {
            var retVal = false;
            try
            {
                var storageWritePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (storageWritePermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    storageWritePermissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }

                var storageReadPermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (storageReadPermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    storageReadPermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                if (storageReadPermissionStatus == Xamarin.Essentials.PermissionStatus.Granted &&
                    storageWritePermissionStatus == Xamarin.Essentials.PermissionStatus.Granted)
                {
                    retVal = true;
                }
                else
                {
                    if (storageWritePermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        await DisplayAlertAsync(AppResources.StoragePermissionAlert);
                    }
                    else if (storageReadPermissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        await DisplayAlertAsync(AppResources.StoragePermissionAlert);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await DisplayAlertAsync(AppResources.PermissionErrorText);
            }
            return retVal;
        }
        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                if (pageObject != null)
                {
                    var carouselRef = pageObject.FindByName("newsImageCarousel") as CarouselView;
                    carouselRef.PositionChanged -= Carousel_PositionChanged;
                    carouselRef.PositionChanged += Carousel_PositionChanged;
                }
                if ((SessionService.isEditingNews || SessionService.isEditingActivity) && SessionService.NewsPostCarouselImages != null && SessionService.NewsPostCarouselImages.Count > 0)
                {
                    var tempList = new ObservableCollection<NewsImageModel>();
                    try
                    {
                        SessionService.NewsPostCarouselImages.All((arg) =>
                        {
                            if (arg != null)
                            {
                                tempList.Add(new NewsImageModel() { NewsImage = ImageSource.FromUri(new Uri(arg)) });
                                return true;
                            }
                            return false;
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                    NewsPostImageCarouselList = tempList;
                    ImageOptionText = AppResources.Delete;
                    NewsPickedImageViewIsVisible = true;
                    NextTextColor = (Color)App.Current.Resources["WhiteColor"];
                }
                if (!isActivity)
                {
                    isActivity = parameters.GetValue<bool>("isFromActivity");
                }
                if (isActivity)
                {
                    AddImagesTitle = AppResources.AddaCoverForYour;
                    ShowPickOgOurImage = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class NewsImageModel : BindableBase
    {
        private string _ImagePreviewPath; //{ get; set; }
        private string _ImagePath; //{ get; set; }
        private MediaFileType _FileType; //{ get; set; }
        private ImageSource _NewsImage; //{ get; set; }

        public string ImagePreviewPath
        {
            get { return _ImagePreviewPath; }
            set { SetProperty(ref _ImagePreviewPath, value); }
        }

        public string ImagePath
        {
            get { return _ImagePath; }
            set { SetProperty(ref _ImagePath, value); }
        }

        public MediaFileType FileType
        {
            get { return _FileType; }
            set { SetProperty(ref _FileType, value); }
        }

        public ImageSource NewsImage
        {
            get { return _NewsImage; }
            set { SetProperty(ref _NewsImage, value); }
        }
    }
}

