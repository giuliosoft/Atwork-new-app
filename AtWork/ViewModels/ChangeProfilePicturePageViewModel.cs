using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Popups;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

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
            //ProfileImage = UserProfileImage;
            ImageOptionText = AppResources.EditCropButtonText;
            HeaderNextNavigationCommand = ProfileSettingProceedCommand;

            if (SessionService.IsWelcomeSetup)
            {
                AddNewsCancelImage = AppResources.BackButtonText;
                HeaderView = (ControlTemplate)App.Current.Resources["BeginSetupHeader_Template"];
                SessionService.CurrentTab = 1;
                isShowLooktext = true;
            }
            else
            {
                HeaderView = (ControlTemplate)App.Current.Resources["AddNewsPostHeader_Template"];
                isShowLooktext = false;
            }
        }
        ObservableCollection<string> ImageList = new ObservableCollection<string>();
        private bool _showCropOption;
        private ImageSource _userProfileImage;
        private string _ImageOptionText = AppResources.EditCropButtonText;
        private string _ChooseFromCamera = AppResources.ChooseFromCameraRoll;
        private NewsImageModel _SelectedNewsImageValue = null;
        private ControlTemplate _Header;
        private bool _isShowLooktext;
        private bool _isShowEditPhoto = false;
        private bool IsImageSelected = false;
        private bool _ShowPickOfOurImage = true;
        public string selectedPicture = string.Empty;
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
        public string ChooseFromCamera
        {
            get { return _ChooseFromCamera; }
            set { SetProperty(ref _ChooseFromCamera, value); }
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
        public bool ShowCropOption
        {
            get { return _showCropOption; }
            set { SetProperty(ref _showCropOption, value); }
        }
        public ControlTemplate HeaderView
        {
            get { return _Header; }
            set { SetProperty(ref _Header, value); }
        }
        public bool isShowLooktext
        {
            get { return _isShowLooktext; }
            set { SetProperty(ref _isShowLooktext, value); }
        }
        public bool isShowEditPhoto
        {
            get { return _isShowEditPhoto; }
            set { SetProperty(ref _isShowEditPhoto, value); }
        }
        public bool ShowPickOfOurImage
        {
            get { return _ShowPickOfOurImage; }
            set { SetProperty(ref _ShowPickOfOurImage, value); }
        }
        IMultiMediaPickerService _multiMediaPickerService;

        #region Commands
        public DelegateCommand AddImagesFromGalleryCommand { get { return new DelegateCommand(async () => await AddImagesFromGallery()); } }
        public DelegateCommand OurImagesCommand { get { return new DelegateCommand(async () => await OurImages()); } }
        public DelegateCommand<string> CropNewsImageCommand { get { return new DelegateCommand<string>(async (obj) => await CropNewsImage(obj)); } }
        public DelegateCommand<string> ProfileSettingProceedCommand { get { return new DelegateCommand<string>(async (obj) => await SaveProfilePicture(obj)); } }
        #endregion

        #region Private Methods
        async Task SaveProfilePicture(string str)
        {
            try
            {
                if (!await CheckConnectivity())
                {
                    return;
                }
                if (!SessionService.IsWelcomeSetup)
                    await ShowLoader();
                List<string> profilePic = new List<string>();
                Volunteers Input = new Volunteers();
                Input.volUniqueID = SettingsService.VolunteersUserData.volUniqueID;
                Input.volDefaultPicture = selectedPicture;
                profilePic.Add(SelectedNewsImageValue?.ImagePath);
                var serviceResult = await UserServices.UpdateProfilePicture(Input, profilePic);
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        SessionService.IsNeedToRefreshNews = true;
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            string ImageName = serviceBody?.Data as string;
                            if (!string.IsNullOrEmpty(ImageName))
                             {
                                SettingsService.UserProfile = ImageName;
                            }
                            if (!SessionService.IsWelcomeSetup)
                                await _navigationService.GoBackAsync();
                        }
                    }
                }
                if (!SessionService.IsWelcomeSetup)
                    await ClosePopup();
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
                if (ChooseFromCamera == AppResources.ChooseFromCameraRoll)
                {
                    await TakePhotoFromGallery();
                }
                else
                {
                    await SaveProfilePicture(SelectedNewsImageValue?.ImagePath);
                    await _navigationService.NavigateAsync(nameof(AboutMePage));
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
                ProfileImagePopup profileImagePopup = new ProfileImagePopup();
                ProfileImagePopupViewModel profileImagePopupViewModel = new ProfileImagePopupViewModel(_navigationService, _facadeService);
                profileImagePopupViewModel.ImageList = ImageList;
                profileImagePopupViewModel.SelectedImageEvent += ProfileImagePopupViewModel_ImageSelected;
                profileImagePopup.BindingContext = profileImagePopupViewModel;
                await PopupNavigationService.ShowPopup(profileImagePopup, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ProfileImagePopupViewModel_ImageSelected(string obj)
        {
            try
            {

                ShowCropOption = false;
                //ProfileImage = ImageSource.FromUri(new Uri(obj));
                ProfileImage = obj;
                selectedPicture = obj.Replace(ConfigService.BaseProfileImageURL, "");
                if (SessionService.IsWelcomeSetup)
                {
                    IsImageSelected = true;
                    isShowEditPhoto = false;
                    isShowLooktext = false;
                    ShowPickOfOurImage = false;
                    ChooseFromCamera = AppResources.SetProfilePicture;
                }
                else
                {
                    isShowEditPhoto = false;
                    isShowLooktext = false;
                    ShowPickOfOurImage = true;
                    ChooseFromCamera = AppResources.ChooseFromCameraRoll;
                }
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
                SessionService.isFromChangeUserProfile = true;
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ImagePath", SelectedNewsImageValue.ImagePreviewPath);
                navigationParams.Add("SelectedNewsImage", SelectedNewsImageValue);
                await _navigationService.NavigateAsync(nameof(CropImagePage), navigationParams);
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
                var pickedFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    SaveMetaData = true
                });

                if (pickedFile == null)
                    return;
                Debug.WriteLine("Photo Location ===== " + pickedFile.Path);
                if (pickedFile != null && !string.IsNullOrEmpty(pickedFile.Path))
                {
                    SelectedNewsImageValue = new NewsImageModel() { ImagePath = pickedFile.Path, ImagePreviewPath = pickedFile.Path, NewsImage = ImageSource.FromFile(pickedFile.Path) };
                    ProfileImage = ImageSource.FromFile(SelectedNewsImageValue?.ImagePath);
                    ShowCropOption = true;
                    SessionService.NewsPostImageFiles = new System.Collections.Generic.List<string>();
                    SessionService.NewsPostImageFiles.Add(pickedFile.Path);
                    if (SessionService.IsWelcomeSetup)
                    {
                        IsImageSelected = true;
                        isShowEditPhoto = true;
                        isShowLooktext = false;
                        ShowPickOfOurImage = false;
                        ChooseFromCamera = AppResources.SetProfilePicture;
                    }
                    else
                    {
                        isShowEditPhoto = false;
                        isShowLooktext = false;
                        ShowPickOfOurImage = true;
                        ChooseFromCamera = AppResources.ChooseFromCameraRoll;
                    }
                }
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

        async Task GetUserExistingProfilePic()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetUserProfilePicture();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    if (serviceResult.Body != null)
                    {
                        var serviceBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                        if (serviceBody != null && serviceBody.Flag)
                        {
                            var url = ConfigService.BaseProfileImageURL + serviceBody.Data as string;
                            ProfileImage = ImageSource.FromUri(new Uri(url));
                            string strAvatarImage = serviceBody?.Data1 as string;

                            if (!string.IsNullOrEmpty(strAvatarImage))
                            {
                                string imgStr = strAvatarImage;
                                List<string> Avatarlist = new List<string>();
                                if (!string.IsNullOrEmpty(imgStr))
                                {
                                    if (imgStr.Contains(","))
                                    {
                                        Avatarlist = imgStr.Split(',').ToList();
                                    }
                                    else
                                    {
                                        Avatarlist.Add(imgStr);
                                    }
                                }
                                if (Avatarlist != null && Avatarlist.Count > 0)
                                {
                                    ImageList = new ObservableCollection<string>();
                                    Avatarlist.All((arg) =>
                                    {
                                        string imageUri = ConfigService.BaseProfileImageURL + arg;
                                        ImageList.Add(imageUri);
                                        return true;
                                    });
                                }
                            }
                        }
                    }
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (!SessionService.isFromChangeUserProfile)// && !SessionService.IsWelcomeSetup)
            {
                await GetUserExistingProfilePic();
            }
            else
            {
                if (SessionService.isFromChangeUserProfile)
                    SessionService.isFromChangeUserProfile = false;
            }
        }
    }
}
