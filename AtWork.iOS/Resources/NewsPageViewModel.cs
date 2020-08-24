using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        #region Constructor
        public NewsPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {

        }
        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        private string _ProductDetail = string.Empty;
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }

        private ObservableCollection<CarouselModel> _NewsImageCarouselList = new ObservableCollection<CarouselModel>();
        public ObservableCollection<CarouselModel> NewsImageCarouselList
        {
            get { return _NewsImageCarouselList; }
            set { SetProperty(ref _NewsImageCarouselList, value); }
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

            var tempCarousel = new ObservableCollection<CarouselModel>();
            tempCarousel.Add(new CarouselModel() { NewsImage = "bg" });
            tempCarousel.Add(new CarouselModel() { NewsImage = "bg" });
            tempCarousel.Add(new CarouselModel() { NewsImage = "bg" });
            NewsImageCarouselList = tempCarousel;
        }
    }

    public class CarouselModel
    {
        public Image NewsImage { get; set; }
    }
}
