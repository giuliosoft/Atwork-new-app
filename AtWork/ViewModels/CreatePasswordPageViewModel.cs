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
    public class CreatePasswordPageViewModel : ViewModelBase
    {
        public string ConfirmPassword;

        #region Constructor
        public CreatePasswordPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            CreatePassowrdLabeltext = "Create your password";
            Passwordmessage = true;
            Samepasswordworning = false;
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private string _CreatePassowrdLabeltext = string.Empty;
        private string _CreatePassowrdEntrytext = string.Empty;
        private bool _Passwordmessage = true;
        private bool _Samepasswordworning = false;
        #endregion

        #region Public Properties

        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }
       
        public string CreatePassowrdLabeltext
        {
            get { return _CreatePassowrdLabeltext; }
            set { SetProperty(ref _CreatePassowrdLabeltext, value); }
        }
        public string CreatePassowrdEntrytext
        {
            get { return _CreatePassowrdEntrytext; }
            set { SetProperty(ref _CreatePassowrdEntrytext, value); }
        }
        public bool Passwordmessage
        {
            get { return _Passwordmessage; }
            set { SetProperty(ref _Passwordmessage, value); }
        }
        public bool Samepasswordworning
        {
            get { return _Samepasswordworning; }
            set { SetProperty(ref _Samepasswordworning, value); }
        }
        #endregion

        #region Commands
        //public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand ConfiromPasswordCommand{ get { return new DelegateCommand(async () => await ConfiromPassword()); } }

        #endregion

        #region private methods
        //async Task GoForLogin()
        //{
        //    try


        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        private async Task ConfiromPassword()
        {
            try
            {
                if (!String.IsNullOrEmpty(CreatePassowrdEntrytext))
                {
                    CreatePassowrdLabeltext = "Confirm your password";
                    ConfirmPassword = CreatePassowrdEntrytext;
                    CreatePassowrdEntrytext = null;
                    Passwordmessage = false;
                }
                
                //await _navigationService.NavigateAsync(nameof(DisclaimerPage), null);
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
            CreatePassowrdLabeltext = "Create your password";
        }
    }
}

