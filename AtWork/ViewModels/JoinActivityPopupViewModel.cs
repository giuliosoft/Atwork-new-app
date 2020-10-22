using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class JoinActivityPopupViewModel : ViewModelBase
    {
        public EventHandler<bool> ClosePopupEvent;
        public EventHandler<List<JoinActivityDatesModel>> JoinActivityEvent;
        #region Constructor
        public JoinActivityPopupViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties
        private ObservableCollection<JoinActivityDatesModel> _ActivityJoinDates = new ObservableCollection<JoinActivityDatesModel>();
        List<JoinActivityDatesModel> SelectedDatesToJoin = new List<JoinActivityDatesModel>();
        #endregion

        #region Public Properties
        public ObservableCollection<JoinActivityDatesModel> ActivityJoinDates
        {
            get { return _ActivityJoinDates; }
            set { SetProperty(ref _ActivityJoinDates, value); }
        }
        public string SelectedActivityType = string.Empty;
        #endregion

        #region Commands        
        public DelegateCommand GoForClosePopupCommand { get { return new DelegateCommand(async () => await CloseProfile()); } }
        public DelegateCommand JoinActivityCommand { get { return new DelegateCommand(async () => await JoinAnActivity()); } }
        public DelegateCommand<JoinActivityDatesModel> DateSelectedCommand { get { return new DelegateCommand<JoinActivityDatesModel>(async (arg) => await DateSelected(arg)); } }
        #endregion

        #region Private Methods
        async Task DateSelected(JoinActivityDatesModel selectedDate)
        {
            try
            {
                if (SelectedActivityType == TextResources.RegularCategoryText)
                {
                    ActivityJoinDates.All((arg) =>
                    {
                        if (arg.Id == selectedDate.Id)
                        {
                            arg.IsSelected = true;
                            SelectedDatesToJoin = new List<JoinActivityDatesModel>();
                            SelectedDatesToJoin.Add(arg);
                        }
                        else
                        {
                            arg.IsSelected = false;
                        }
                        return true;
                    });
                }
                else if (SelectedActivityType == TextResources.RecurringCategoryText)
                {
                    ActivityJoinDates.All((item) =>
                    {
                        if (item == selectedDate)
                        {
                            item.IsSelected = !item.IsSelected;
                            if (item.IsSelected)
                            {
                                SelectedDatesToJoin.Add(item);
                            }
                            else
                            {
                                SelectedDatesToJoin.Remove(item);
                            }
                        }
                        return true;
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }

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

        async Task JoinAnActivity()
        {
            try
            {
                await PopupNavigationService.ClosePopup(true);
                JoinActivityEvent.Invoke(this, SelectedDatesToJoin);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        #endregion
    }

    public class JoinActivityDatesModel : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private string _DisplayDateString;
        public string DisplayDateString
        {
            get { return _DisplayDateString; }
            set { SetProperty(ref _DisplayDateString, value); }
        }

        private DateTime _ActivityDate;
        public DateTime ActivityDate
        {
            get { return _ActivityDate; }
            set { SetProperty(ref _ActivityDate, value); }
        }
    }
}
