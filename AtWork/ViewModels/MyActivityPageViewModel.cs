using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class MyActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public MyActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            HeaderDetailsTitle = AppResources.MyActivitesText;
            MyActivitycollectionlist.Add(new ActivityItems());
            MyActivitycollectionlist.Add(new ActivityItems());
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ObservableCollection<ActivityItems> _MyActivitycollectionlist = new ObservableCollection<ActivityItems>();

        #endregion

        #region Public Properties
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public ObservableCollection<ActivityItems> MyActivitycollectionlist
        {
            get { return _MyActivitycollectionlist; }
            set { SetProperty(ref _MyActivitycollectionlist, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GotoActivityDetailsCommand { get { return new DelegateCommand(async () => await GotoActivityDetails()); } }
        public DelegateCommand GotoCreateActivityCommand { get { return new DelegateCommand(async () => await GotoCreateActivity()); } }
        #endregion

        #region private methods

        async Task GotoActivityDetails()
        {
            try
            {
                //await _navigationService.NavigateAsync(nameof(ActivityDetailPage), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task GotoCreateActivity()
        {
            try
            {
                await _navigationService.NavigateAsync(nameof(CreateActivityPage), null);
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