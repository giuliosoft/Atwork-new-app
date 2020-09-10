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
    public class ChangeUserPasswordPageViewModel : ViewModelBase
    {
        public string ConfirmPassword;
        bool isCreatingPwd = true;
        #region Constructor
        public ChangeUserPasswordPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties

        #endregion

        #region Commands
        #endregion

        #region private methods

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

