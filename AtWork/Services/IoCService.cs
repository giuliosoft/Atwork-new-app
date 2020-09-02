using System;
using AtWork.ViewModels;
using AtWork.Views;
using Prism.Ioc;
using Xamarin.Forms;

namespace AtWork.Services
{
    public class IoCService
    {
        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="containerRegistry">containerRegistry</param>
        internal static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Infrastructure Depedencies
            RegisterInfrastructure(containerRegistry);
            // ViewModels for Navigation
            RegisterViewModelsForNavigation(containerRegistry);
            //Services
            RegisterServices(containerRegistry);
        }

        /// <summary>
        /// RegisterInfrastructure
        /// </summary>
        /// <param name="containerRegistry"></param>
        private static void RegisterInfrastructure(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
        }

        /// <summary>
        /// Register ViewModels For Navigation
        /// </summary>
        /// <param name="containerRegistry">containerRegistry</param>
        private static void RegisterViewModelsForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<NewsPage, NewsPageViewModel>();
            containerRegistry.RegisterForNavigation<NewsDetailPage, NewsDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewsPostPage, AddNewsPostPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewsPostImagePage, AddNewsPostImagePageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewsAttachFilePage, AddNewsAttachFilePageViewModel>();
            containerRegistry.RegisterForNavigation<CropImagePage, CropImagePageViewModel>();
            containerRegistry.RegisterForNavigation<PostNewsPage, PostNewsPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityPage, ActivityPageViewModel>();
            containerRegistry.RegisterForNavigation<StartUpPage, StartUpPageViewModel>();
            containerRegistry.RegisterForNavigation<FindAccountPage, FindAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<ClaimProfilePage, ClaimProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ClaimEditProfilePage, ClaimEditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ClaimThankYouReqeuestPage, ClaimThankYouRequestPageViewModel>();
            containerRegistry.RegisterForNavigation<CreatePasswordPage, CreatePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<DisclaimerPage, DisclaimerPageViewModel>();
            containerRegistry.RegisterForNavigation<AuthentificationIDPage, AuthenticationIDPageViewModel>();
            containerRegistry.RegisterForNavigation<TouchIDLoginPage, TouchIdLoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityDetailPage, ActivityDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardPageViewModel>();
            containerRegistry.RegisterForNavigation<MyActivityPage, MyActivityPageViewModel>();
        }

        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="containerRegistry"></param>
        private static void RegisterServices(IContainerRegistry containerRegistry)
        {
            // using live services
            RegisterLiveServices(containerRegistry);
        }

        /// <summary>
        /// RegisterLiveServices
        /// </summary>
        /// <param name="containerRegistry"></param>
        private static void RegisterLiveServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<FacadeService>();
        }
    }
}
