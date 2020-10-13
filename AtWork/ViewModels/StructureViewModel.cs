using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class StructureViewModel : ViewModelBase
    {
        #region Constructor
        public StructureViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
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
                ExceptionHelper.CommanException(ex);
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
