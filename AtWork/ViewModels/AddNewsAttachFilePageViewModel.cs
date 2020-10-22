using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AtWork.Helpers;
using AtWork.Multilingual;
using AtWork.Services;
using AtWork.Views;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AtWork.ViewModels
{
    public class AddNewsAttachFilePageViewModel : ViewModelBase
    {
        #region Constructor
        public AddNewsAttachFilePageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            NextClickPageName = nameof(AddNewsAttachFilePage);
            AddNewsCancelImage = AppResources.BackButtonText;
            AddNewsNextImage = AppResources.SkipText;
            HeaderNextNavigationCommand = NewsPostProceedCommand;
            AttachFileButtonText = AppResources.AttachFile;
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private bool _AttachedFileViewIsVisible = false;
        private string _NewsPostAttachFileName = string.Empty;
        private string NewsPostAttachFilePath = string.Empty;
        private string _AttachFileButtonText = string.Empty;
        #endregion

        #region Public Properties        
        public string ProductDetail
        {
            get { return _ProductDetail; }
            set { SetProperty(ref _ProductDetail, value); }
        }

        public bool AttachedFileViewIsVisible
        {
            get { return _AttachedFileViewIsVisible; }
            set { SetProperty(ref _AttachedFileViewIsVisible, value); }
        }

        public string NewsPostAttachFileName
        {
            get { return _NewsPostAttachFileName; }
            set { SetProperty(ref _NewsPostAttachFileName, value); }
        }

        public string AttachFileButtonText
        {
            get { return _AttachFileButtonText; }
            set { SetProperty(ref _AttachFileButtonText, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand AttachFileCommand { get { return new DelegateCommand(async () => await AttachFile()); } }
        public DelegateCommand<string> NewsPostProceedCommand { get { return new DelegateCommand<string>(async (obj) => await NewsPostProceed(obj)); } }
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

        async Task NewsPostProceed(string selectedTab)
        {
            try
            {
                await ShowLoader();
                if (!string.IsNullOrEmpty(NewsPostAttachFilePath) && !string.IsNullOrEmpty(NewsPostAttachFileName))
                {
                    SessionService.NewsPostAttachmentFilePath = NewsPostAttachFilePath;
                    SessionService.NewsPostAttachmentFileName = NewsPostAttachFileName;
                }
                if (SessionService.NewsPostInputData != null)
                {
                    SessionService.NewsPostInputData.newsFile = NewsPostAttachFileName;
                }
                await _navigationService.NavigateAsync(nameof(PostNewsPage));
                await ClosePopup();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                ExceptionHelper.CommanException(ex);
            }
        }

        async Task AttachFile()
        {
            try
            {
                //FileData fileData = new FileData();
                //fileData = await CrossFilePicker.Current.PickFile();
                //if (fileData != null)
                //{
                //    AttachedFileViewIsVisible = true;
                //    byte[] data = fileData.DataArray;
                //    string name = fileData.FileName;
                //    NewsPostAttachFileName = name;
                //    string filePath = fileData.FilePath;
                //}

                string[] strFileType = DependencyService.Get<FileNameInterface>().FileTypeList();
                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile(allowedTypes: strFileType);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (fileData != null)
                    {
                        AttachedFileViewIsVisible = true;
                        AttachFileButtonText = AppResources.ReplaceFile;
                        AddNewsNextImage = AppResources.NextButtonText;
                        byte[] data = fileData.DataArray;
                        var FileBase64String = Convert.ToBase64String(data);

                        string FileName = fileData.FileName;
                        NewsPostAttachFileName = FileName;
                        string FilePath = fileData.FilePath;
                        NewsPostAttachFilePath = FilePath;
                        string name = (fileData.FilePath != null) ? Path.GetFileNameWithoutExtension(fileData.FilePath) : string.Empty;
                        string mimeType = Path.GetExtension(FilePath);

                        //await DisplayAlertAsync("File Picked Successfully");
                    }
                });
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
            if (!string.IsNullOrEmpty(SessionService.NewsPostInputData.newsFileOriginal))
            {
                AttachedFileViewIsVisible = true;
                AddNewsNextImage = AppResources.NextButtonText;
                AttachFileButtonText = AppResources.ReplaceFile;
                NewsPostAttachFileName = SessionService.NewsPostInputData.newsFileOriginal;
            }
        }
    }
}

