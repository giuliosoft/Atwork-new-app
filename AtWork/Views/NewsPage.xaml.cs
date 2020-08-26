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

        void newsPostCarousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
        {
            var cView = sender as CarouselView;
            var pView = cView.Parent.Parent as Grid;
            var children = pView.Children;
            foreach (var c in children)
            {
                if (c.GetType().Name == nameof(IndicatorView))
                {
                    var indcView = c as IndicatorView;
                    indcView.Position = cView.Position;
                }
            }
        }
    }

    public class CarouselModel
    {
        public ImageSource NewsImage { get; set; }
    }
}
