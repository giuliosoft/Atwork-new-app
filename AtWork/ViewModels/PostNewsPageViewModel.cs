using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class PostNewsPageViewModel : ViewModelBase
    {
        #region Constructor
        public PostNewsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(PostNewsPage);
            AddNewsCancelImage = "Back";
            AddNewsNextImage = "Publish";
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        
        #endregion

        #region Public Properties        
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand PostToEveryeodyCommand { get { return new DelegateCommand(async () => await PostToEveryeody()); } }
        public DelegateCommand PostToYourGroupCommand { get { return new DelegateCommand(async () => await PostToYourGroup()); } }
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
        async Task PostToEveryeody()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task PostToYourGroup()
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
            //AddNewsCancelImage = "Back";
            //AddNewsNextImage = "Publish";
        }
    }
}
