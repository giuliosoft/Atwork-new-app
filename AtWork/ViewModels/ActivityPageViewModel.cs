using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class ActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public ActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextCustomLabelIsVisible = true;
            NextOptionText = AppResources.MyActivitiesHeaderText;
            //NewsGreenbg = (Color)App.Current.Resources["LightBrownColor"];
            //ActivitiesGreenbg = (Color)App.Current.Resources["AccentColor"];

            Activitylist.Add(new ActivityItems() { title = AppResources.AllCategoriesText });
            Activitylist.Add(new ActivityItems() { title = AppResources.CorporateVolunteeringText });
            Activitylist.Add(new ActivityItems() { title = AppResources.EducationsText });
        }
        #endregion

        #region Private Properties
        private ObservableCollection<ActivityItems> _Activitylist = new ObservableCollection<ActivityItems>();
        #endregion

        #region Public Properties        
        public ObservableCollection<ActivityItems> Activitylist
        {
            get { return _Activitylist; }
            set { SetProperty(ref _Activitylist, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(() => GoForLogin()); } }
        public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        #endregion

        #region private methods
        void GoForLogin()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task GotoActivityDetails()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(ActivityDetailPage), null);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}

