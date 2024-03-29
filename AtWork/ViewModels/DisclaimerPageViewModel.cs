﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class DisclaimerPageViewModel : ViewModelBase
    {
        #region Constructor
        public DisclaimerPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DisclaimerLabel = AppResources.DisclaimerHeaderText;
            BackCancelText = AppResources.BackButtonText;
            Disclaimerbtn = true;
            Termsconditionbtn = false;
            ButtonColor = (Color)App.Current.Resources["GrayColor"];
        }
        #endregion

        #region Private Properties        
        private string _DisclaimerLabel = AppResources.DisclaimerHeaderText;
        private string _disclaimerText = AppResources.DisclamerText;
        private bool _Disclaimerbtn = true;
        private bool _Termsconditionbtn = false;
        private ImageSource _UserCompanyLogo = string.Empty;
        private Color _ButtonColor = (Color)App.Current.Resources["WhiteColor"];
        bool isDisclaimer = true;
        #endregion

        #region Public Properties
        public bool isEnableDisclamerButton = false;
        public DisclaimerPage pageObject = null;

        public string DisclaimerLabel
        {
            get { return _DisclaimerLabel; }
            set { SetProperty(ref _DisclaimerLabel, value); }
        }
        public bool Disclaimerbtn
        {
            get { return _Disclaimerbtn; }
            set { SetProperty(ref _Disclaimerbtn, value); }
        }
        public bool Termsconditionbtn
        {
            get { return _Termsconditionbtn; }
            set { SetProperty(ref _Termsconditionbtn, value); }
        }
        public Color ButtonColor
        {
            get { return _ButtonColor; }
            set { SetProperty(ref _ButtonColor, value); }
        }
        public ImageSource UserCompanyLogo
        {
            get
            {
                return ImageSource.FromUri(new Uri(ConfigService.BaseCompanyLogoURL + SessionService.tempClaimProfileData.coLogo));
            }
            set { SetProperty(ref _UserCompanyLogo, value); }
        }
        public string DisclaimerText
        {
            get { return _disclaimerText; }
            set { SetProperty(ref _disclaimerText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand BackButtonCommand { get { return new DelegateCommand(async () => await BackButton()); } }
        public DelegateCommand ContinueSetupCommand { get { return new DelegateCommand(async () => await TermsandCondition()); } }
        public DelegateCommand GoToAuthenticationIdCommand { get { return new DelegateCommand(async () => await TermsandCondition()); } }
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
        async Task BackButton()
        {
            try
            {
                if (isDisclaimer)
                {
                    await _navigationService.GoBackAsync();
                }
                else
                {
                    isDisclaimer = true;
                    DisclaimerLabel = AppResources.DisclaimerHeaderText;
                    DisclaimerText = AppResources.DisclamerText;
                    Disclaimerbtn = true;
                    Termsconditionbtn = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        private async Task TermsandCondition()
        {
            try
            {
                if (isEnableDisclamerButton)
                {
                    if (isDisclaimer)
                    {
                        isDisclaimer = false;
                        DisclaimerLabel = AppResources.TermsAndConditionHeaderText;
                        DisclaimerText = AppResources.TermsText;
                        Disclaimerbtn = false;
                        Termsconditionbtn = true;
                        if (pageObject != null)
                        {
                            var scrollRef = pageObject.FindByName("appDetailScrollView");
                            if (scrollRef != null)
                            {
                                var scroller = scrollRef as ScrollView;
                                await scroller.ScrollToAsync(0, 0, false);
                            }
                            isEnableDisclamerButton = false;
                            ButtonColor = (Color)App.Current.Resources["GrayColor"];
                        }
                    }
                    else
                    {
                        if (SessionService.isFromClaimProfile)
                        {
                            SessionService.isFromClaimProfile = false;
                            await _navigationService.NavigateAsync(nameof(AuthentificationIDPage), null);
                        }
                        else
                        {
                            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(DashboardPage)}", null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
        private async Task GoToAuthenticationId()
        {
            try
            {
                if (isEnableDisclamerButton)
                {
                    await _navigationService.NavigateAsync(nameof(AuthentificationIDPage), null);
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
            try
            {
                var isDisclaimer = parameters.GetValue<bool>("isDisclaimer");
                if (isDisclaimer)
                {
                    DisclaimerLabel = AppResources.DisclaimerHeaderText;
                    DisclaimerText = AppResources.DisclamerText;
                }
                else
                {
                    DisclaimerLabel = AppResources.TermsAndConditionHeaderText;
                    DisclaimerText = AppResources.TermsText;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
        }
    }
}

