﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Models;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class ActivityPageViewModel : ViewModelBase
    {
        #region Constructor
        public ActivityPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            Activitylist.Add(new ActivityItems() { title = "All categories" });
            Activitylist.Add(new ActivityItems() { title = "Corporate volunteering" });
            Activitylist.Add(new ActivityItems() { title = "Education" });
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private ObservableCollection<ActivityItems> _Activitylist = new ObservableCollection<ActivityItems>();
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public ObservableCollection<ActivityItems> Activitylist
        {
            get { return _Activitylist; }
            set { SetProperty(ref _Activitylist, value); }
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
        }
    }
}

