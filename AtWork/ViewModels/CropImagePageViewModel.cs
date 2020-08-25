using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
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
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ImageSource _NewsImageToCrop = string.Empty;
        private NewsImageModel _SelectedNewsImageValue = null;
        #endregion

        #region Public Properties
        public CropImagePage pageObject;
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
