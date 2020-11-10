using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Models;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static AtWork.Models.UserModel;

namespace AtWork.ViewModels
{
    public class ChangeBirthdatePageViewModel : ViewModelBase
    {
        #region Constructor
        public ChangeBirthdatePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(ChangeBirthdatePage);
            AddNewsCancelImage = AppResources.Cancel;
            HeaderDetailsTitle = AppResources.BirthDayDate;
            HeaderDetailsTitleFontSize = (double)App.Current.Resources["FontSize16"];
            HeaderDetailBackgroundColor = (Color)App.Current.Resources["HeaderBackgroundColor"];
            AddNewsNextImage = AppResources.SaveButtonText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
        }
        #endregion

        #region Private Properties
        private string _Prop = string.Empty;
        private bool _isShowBirthdatePublic = false;
        private DateTime _SelectedDate = DateTime.Now;
        #endregion

        #region Public Properties        
        public string Prop
        {
            get { return _Prop; }
            set { SetProperty(ref _Prop, value); }
        }
        public bool isShowBirthdatePublic
        {
            get { return _isShowBirthdatePublic; }
            set { SetProperty(ref _isShowBirthdatePublic, value); }
        }
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty(ref _SelectedDate, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await UpdateBirthDate(obj)); } }
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
       
        async Task UpdateBirthDate(string str)
        {
            try
            {
                await ShowLoader();
                
                VolunteerBirthday volunteerBirthday = new VolunteerBirthday();
                volunteerBirthday.volBirthDay = SelectedDate.Day;
                volunteerBirthday.volBirthMonth = SelectedDate.Month;
                volunteerBirthday.volShowBirthday = isShowBirthdatePublic;
                var serviceResult = await UserServices.UpdateBirthDate(volunteerBirthday);
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<BirthDateResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {

                    }
                }
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        async Task getBirthDate()
        {
            try
            {
                await ShowLoader();
                var serviceResult = await UserServices.GetBirthDate();
                await ClosePopup();
                if (serviceResult != null && serviceResult.Result == ResponseStatus.Ok)
                {
                    var serviceResultBody = JsonConvert.DeserializeObject<BirthDateResponce>(serviceResult.Body);
                    if (serviceResultBody != null && serviceResultBody.Data != null)
                    {
                        if (serviceResultBody.Data.volBirthMonth > 0 && serviceResultBody.Data.volBirthDay > 0)
                        {
                            DateTime dt = new DateTime(DateTime.Now.Year, serviceResultBody.Data.volBirthMonth, serviceResultBody.Data.volBirthDay);
                            SelectedDate = dt;
                        }
                        isShowBirthdatePublic = serviceResultBody.Data.volShowBirthday;
                    }
                }
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
            await getBirthDate();
        }
    }
}


