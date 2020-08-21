using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();            
            //indicatorView.Position = 0;
        }

        void tutorialCarousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
        {
            var view = sender as CarouselView;

            //indicatorView.Position = view.Position;
        }
    }

    public class CarouselModel
    {
        public ImageSource NewsImage { get; set; }
    }
}
