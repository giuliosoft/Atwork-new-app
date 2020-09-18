using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ForgotPasswordPageViewModel : ViewModelBase
    {
        #region Constructor
        public ForgotPasswordPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            AddNewsCancelImage = AppResources.BackButtonText;
        }
        #endregion

        #region Private Properties
        private string _EmailorCode = string.Empty;
        #endregion

        #region Public Properties        
        public string EmailorCode
        {
            get { return _EmailorCode; }
            set { SetProperty(ref _EmailorCode, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand ResetPasswordCommand { get { return new DelegateCommand(async () => await ResetPassword()); } }
        #endregion

        #region private methods
        async Task ResetPassword()
        {
            try
            {
                if (string.IsNullOrEmpty(EmailorCode.Trim()))
                {
                    await DisplayAlertAsync(AppResources.EnterEmailAddressOrID);
                    return;
                }
                await ShowLoader();
                var serviceResult = await UserServices.UserForgotPassword(EmailorCode.Trim());
                await ClosePopup();
                var serviceResultBody = JsonConvert.DeserializeObject<CommonResponseModel>(serviceResult.Body);
                if (serviceResultBody != null && serviceResultBody.Flag)
                {
                    await DisplayAlertAsync(AppResources.SuccessResetPassword);
                    await _navigationService.GoBackAsync();
                }
                else if (serviceResultBody != null && !serviceResultBody.Flag)
                {
                    await DisplayAlertAsync(AppResources.EnterValidEmailAddressOrID);
                }
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
        }
    }
}
