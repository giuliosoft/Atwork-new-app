using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ChangeProfilePicturePageViewModel : ViewModelBase
    {
        public ChangeProfilePicturePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.Cancel;
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderDetailsTitle = AppResources.ProfilePictureText;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            _multiMediaPickerService = DependencyService.Get<IMultiMediaPickerService>();
            ProfileImage = UserProfileImage;
            ImageOptionText = AppResources.EditCropButtonText;
        }
        MediaFile mFile = null;
        private static ImageSource _userProfileImage;
        private string _ImageOptionText = AppResources.EditCropButtonText;
        private NewsImageModel _SelectedNewsImageValue = null;
        public ImageSource ProfileImage
        {
            get
            {
                return _userProfileImage;
            }
            set { SetProperty(ref _userProfileImage, value); }
        }
        public string ImageOptionText
        {
            get { return _ImageOptionText; }
            set { SetProperty(ref _ImageOptionText, value); }
        }
        public NewsImageModel SelectedNewsImageValue
        {
            get { return _SelectedNewsImageValue; }
            set
            {
                SetProperty(ref _SelectedNewsImageValue, value);
                    //RaisePropertyChanged(nameof(ProfileImage));
            }
        }
        IMultiMediaPickerService _multiMediaPickerService;
        public DelegateCommand AddImagesFromGalleryCommand { get { return new DelegateCommand(async () => await AddImagesFromGallery()); } }
        public DelegateCommand OurImagesCommand { get { return new DelegateCommand(async () => await OurImages()); } }
        public DelegateCommand<string> CropNewsImageCommand { get { return new DelegateCommand<string>(async (obj) => await CropNewsImage(obj)); } }
        async Task AddImagesFromGallery()
        {
            try
            {
                await TakePhotoFromGallery();
                //var hasPermission = await TakePermissionsToPickPhoto();
                //if (hasPermission)
                //{
                //    var res = await _multiMediaPickerService.PickPhotosAsync();
                //    if (res != null && res.Count > 0)
                //    {
                //        var mFile = res[0];
                //        ProfileImage = ImageSource.FromFile(mFile.PreviewPath);
                //        SessionService.NewsPostImageFiles = new System.Collections.Generic.List<string>();
                //        SessionService.NewsPostImageFiles.Add(mFile.Path);
                //        SelectedNewsImageValue = new NewsImageModel() { ImagePath = mFile.Path, ImagePreviewPath = mFile.PreviewPath, FileType = mFile.Type, NewsImage = ImageSource.FromFile(mFile.PreviewPath) };
                //    }
                //}
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
                //ActivityImagePopup activityImagePopup = new ActivityImagePopup();
                //ActivityImagePopupViewModel activityImagePopupViewModel = new ActivityImagePopupViewModel(_navigationService, _facadeService);
                //activityImagePopupViewModel.SelectedImageSourceEvent += async (object sender, string SelectedObj) =>
                //{
                //    try
                //    {
                //        //NewsPostImageCarouselList.Add(new NewsImageModel() { NewsImage = SelectedObj });
                //        //NewsPickedImageViewIsVisible = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        Debug.WriteLine(ex.Message);
                //    }
                //};
                //activityImagePopup.BindingContext = activityImagePopupViewModel;
                //await PopupNavigationService.ShowPopup(activityImagePopup, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task CropNewsImage(string selectedOption)
        {
            try
            {
                if (SelectedNewsImageValue == null)
                    return;
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ImagePath", SelectedNewsImageValue.ImagePreviewPath);
                navigationParams.Add("SelectedNewsImage", SelectedNewsImageValue);
                await _navigationService.NavigateAsync(nameof(CropImagePage), navigationParams);
                //if (pageObject != null)
                //{
                //    var carouselRef = pageObject.FindByName("newsImageCarousel") as CarouselView;
                //    if (selectedOption == AppResources.Delete)
                //    {
                //        if (carouselRef != null)
                //        {
                //            var pos = carouselRef.Position;
                //            var tmpList = new ObservableCollection<NewsImageModel>(NewsPostImageCarouselList);
                //            tmpList.RemoveAt(pos);
                //            NewsPostImageCarouselList = new ObservableCollection<NewsImageModel>(tmpList);
                //            SessionService.NewsPostCarouselImages.RemoveAt(pos);
                //            if (NewsPostImageCarouselList != null)
                //            {
                //                if (NewsPostImageCarouselList.Count > 0)
                //                {
                //                    if (NewsPostImageCarouselList[CarouselPosition] != null)
                //                    {
                //                        if (!string.IsNullOrEmpty(NewsPostImageCarouselList[CarouselPosition].ImagePath))
                //                        {
                //                            ImageOptionText = AppResources.EditCropButtonText;
                //                        }
                //                    }
                //                }
                //                else if (NewsPostImageCarouselList.Count == 0)
                //                {
                //                    NewsPickedImageViewIsVisible = false;
                //                    ImageOptionText = AppResources.EditCropButtonText;
                //                }
                //            }
                //        }
                //    }
                //    else if (selectedOption == AppResources.EditCropButtonText)
                //    {
                //        if (carouselRef != null)
                //        {
                //            NewsImageSelectedForCrop = carouselRef.Position;
                //            var navigationParams = new NavigationParameters();
                //            navigationParams.Add("ImagePath", NewsPostImageCarouselList[NewsImageSelectedForCrop].ImagePreviewPath);
                //            navigationParams.Add("SelectedNewsImage", NewsPostImageCarouselList[NewsImageSelectedForCrop]);
                //            await _navigationService.NavigateAsync(nameof(CropImagePage), navigationParams);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task TakePhotoFromGallery()
        {
            var permissionRes = await TakePermissionsToPickPhoto();
            if (permissionRes)
            {
                //if (!CrossMedia.Current.IsPickPhotoSupported)
                //{
                //    await DisplayAlertAsync(AppResources.PhotoPickAlert);
                //    return;
                //}

                var pickedFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    SaveMetaData = true
                    //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,                    
                });

                if (pickedFile == null)
                    return;

                Debug.WriteLine("Photo Location ===== " + pickedFile.Path);

                mFile = pickedFile;
                //IsImageLoading = true;
                if (mFile != null && !string.IsNullOrEmpty(mFile.Path))
                {
                    SelectedNewsImageValue = new NewsImageModel() { ImagePath = mFile.AlbumPath, ImagePreviewPath = mFile.AlbumPath, NewsImage = ImageSource.FromFile(mFile.Path) };
                    //new ImageCrop()
                    //{
                    //    CropShape = ImageCrop.CropShapeType.Rectangle,
                    //    Success = (imageFile) =>
                    //    {
                    //        Device.BeginInvokeOnMainThread(() =>
                    //        {
                    //            LoggedInUserProfilePic = ImageSource.FromFile(imageFile);
                    //            profilePicFilePath = imageFile;
                    //            if (LoggedInUserProfilePic != null)
                    //            {
                    //                IsImageLoading = false;
                    //            }
                    //        });
                    //    },
                    //    Faiure = () =>
                    //    {
                    //        IsImageLoading = false;
                    //    }
                    //}.Show(pickedMediaFile.Path);
                }

                //Stream pickedImageStream = null;
                //LoggedInUserProfilePic = ImageSource.FromStream(() =>
                //{
                //    var stream = pickedFile.GetStream();
                //    pickedImageStream = stream;
                //    //pickedFile.Dispose();
                //    return stream;
                //});
                //profilePicFilePath = pickedFile.Path;
            }
        }
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
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
