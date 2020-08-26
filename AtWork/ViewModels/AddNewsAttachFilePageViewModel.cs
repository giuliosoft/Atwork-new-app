using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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
            AddNewsCancelImage = "Back";
            AddNewsNextImage = "Skip";
        }
        #endregion

        #region Private Properties
        private string _ProductDetail = string.Empty;
        private bool _AttachedFileViewIsVisible = false;
        private string _NewsPostAttachFileName = string.Empty;
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
        #endregion

        #region Commands
        public DelegateCommand GoForLoginCommand { get { return new DelegateCommand(async () => await GoForLogin()); } }
        public DelegateCommand AttachFileCommand { get { return new DelegateCommand(async () => await AttachFile()); } }
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

                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (fileData != null)
                    {
                        AttachedFileViewIsVisible = true;
                        byte[] data = fileData.DataArray;
                        var FileBase64String = Convert.ToBase64String(data);

                        string FileName = fileData.FileName;
                        NewsPostAttachFileName = FileName;
                        string FilePath = fileData.FilePath;
                        string name = (fileData.FilePath != null) ? Path.GetFileNameWithoutExtension(fileData.FilePath) : string.Empty;
                        string mimeType = Path.GetExtension(FilePath);

                        //await DisplayAlertAsync("File Picked Successfully");
                    }
                });

                
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

