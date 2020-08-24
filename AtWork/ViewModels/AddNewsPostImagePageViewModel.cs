﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class AddNewsPostImagePageViewModel : ViewModelBase
    {
        #region Constructor
        public AddNewsPostImagePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = "AddNewsPostBack";
            AddNewsNextImage = "AddNewsPostNext";
            NextClickPageName = nameof(AddNewsPostImagePage);
            _multiMediaPickerService = DependencyService.Get<IMultiMediaPickerService>();
        }
        #endregion

        #region Private Properties
        IMultiMediaPickerService _multiMediaPickerService;
        private ObservableCollection<NewsImageModel> _NewsPostImageCarouselList = new ObservableCollection<NewsImageModel>();
        private string _Prop = string.Empty;
        private bool _NewsPickedImageViewIsVisible = false;
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
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand AddImagesFromGalleryCommand { get { return new DelegateCommand(async () => await AddImagesFromGallery()); } }
        public DelegateCommand CropNewsImageCommand { get { return new DelegateCommand(async () => await CropNewsImage()); } }
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
                        NewsPickedImageViewIsVisible = true;
                        res.All((mFile) =>
                        {
                            NewsPostImageCarouselList.Add(new NewsImageModel() { ImagePath = mFile.Path, ImagePreviewPath = mFile.PreviewPath, FileType = mFile.Type, NewsImage = ImageSource.FromFile(mFile.PreviewPath) });
                            return true;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task CropNewsImage()
        {
            try
            {
                if (pageObject != null)
                {
                    var carouselRef = pageObject.FindByName("newsImageCarousel") as CarouselView;
                    if (carouselRef != null)
                    {
                        var cPosition = carouselRef.Position;
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("ImagePath", NewsPostImageCarouselList[cPosition].ImagePreviewPath);
                        await _navigationService.NavigateAsync(nameof(CropImagePage), navigationParams);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
            AddNewsCancelImage = "AddNewsPostBack";
            AddNewsNextImage = "AddNewsPostNext";
        }
    }

    public class NewsImageModel
    {
        public string ImagePreviewPath { get; set; }
        public string ImagePath { get; set; }
        public MediaFileType FileType { get; set; }
        public ImageSource NewsImage { get; set; }
    }
}

