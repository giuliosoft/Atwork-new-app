using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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
            ImageList.Add("01");
            ImageList.Add("02");
            ImageList.Add("03");
            ImageList.Add("04");
            ImageList.Add("05");
            ImageList.Add("06");
        }
        #region Private Properties
        private ObservableCollection<string> _imageList = new ObservableCollection<string>();
        private string _selectedImage;
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
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
