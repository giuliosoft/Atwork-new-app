using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace AtWork.Models
{
    public class FeedBackUIModel : BindableBase
    //{
    //    public FeedBackUIModel()
    //    {

    //    }

    //    private bool _isSelected;
    //    public bool IsSelected
    //    {
    //        get { return _isSelected; }
    //        set {
    //            SetProperty(ref _isSelected, value);
    //            if (value)
    //            {
    //                SelectedColor = (Color)App.Current.Resources["DarkGreenColor"];
    //                SelectedTextColor = (Color)App.Current.Resources["OffWhiteColor"];
    //            }
    //            else {
    //                SelectedColor = (Color)App.Current.Resources["OffWhiteColor"];
    //                SelectedTextColor = (Color)App.Current.Resources["DarkGreenColor"];
    //            }
    //        }
    //    }

    //    private Color _selectedColor = (Color)App.Current.Resources["OffWhiteColor"];
    //    public Color SelectedColor
    //    {
    //        get { return _selectedColor; }
    //        set { SetProperty(ref _selectedColor, value); }
    //    }

    //    private string _title;
    //    public string Title
    //    {
    //        get { return _title; }
    //        set { SetProperty(ref _title, value); }
    //    }

    //    private Color _selectedTextColor = (Color)App.Current.Resources["DarkGreenColor"];
    //    public Color SelectedTextColor
    //    {
    //        get { return _selectedTextColor; }
    //        set { SetProperty(ref _selectedTextColor, value); }
    //    }
    //}
    {
     
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        

    }
}
