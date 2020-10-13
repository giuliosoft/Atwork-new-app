using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ProfileImagePopupViewModel : ViewModelBase
    {
        public event Action<string> SelectedImageEvent;
        public ProfileImagePopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
        }
        #region Private Properties
        private ObservableCollection<string> _imageList = new ObservableCollection<string>();
        private string _selectedImage;
        private string _AvatarImages;
        #endregion
        #region Public Properties
        public ObservableCollection<string> ImageList
        {
            get { return _imageList; }
            set { SetProperty(ref _imageList, value); }
        }
        public string SelectedImage
        {
            get { return _selectedImage; }
            set { SetProperty(ref _selectedImage, value); }
        }

        public string AvatarImages
        {
            get { return _AvatarImages; }
            set { SetProperty(ref _AvatarImages, value); }
        }
        #endregion
        #region Commands
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand<string> ImageSelectedCommand { get { return new DelegateCommand<string>(async (obj) => await ImageSelected(obj)); } }
        #endregion
        #region Private Methods
        async Task CloseProfile()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task ImageSelected(string name)
        {
            try
            {
                SelectedImageEvent?.Invoke(name);
                await PopupNavigationService.ClosePopup(true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion
    }
}
