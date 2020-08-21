using System;
using Prism.Services;

namespace AtWork.Services
{
    public class FacadeService
    {
        public IPageDialogService dialogService;
        public FacadeService(IPageDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
    }
}
