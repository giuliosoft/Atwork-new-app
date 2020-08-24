using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Prism.Services;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class CreatePasswordPage : ContentPage
    {
        public CreatePasswordPage()
        {
            InitializeComponent();
           //this.BindingContext = new CreatePasswordPageViewModel(null, null);
            var pageViewModel = (CreatePasswordPageViewModel)this.BindingContext;
            vm = this.BindingContext as CreatePasswordPageViewModel;
            txtpassword.TextChanged += (sender, e) =>
            {
                this.InvalidateMeasure();
                if (!string.IsNullOrEmpty(txtpassword.Text) && !string.IsNullOrWhiteSpace(txtpassword.Text))
                {
                    var Keyword = txtpassword.Text.Trim();
                    if (Keyword.Length >= 1)
                    {
                        if(!string.IsNullOrEmpty(vm.ConfirmPassword))
                        {
                            if (txtpassword.Text != vm.ConfirmPassword)
                            {
                                vm.Samepasswordworning = true;
                            }
                            else
                            {
                                vm.Samepasswordworning = false;
                            }

                        }
                    }
                }
            };
        }
        private CreatePasswordPageViewModel vm = null; 
    }
}
