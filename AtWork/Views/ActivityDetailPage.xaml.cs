using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class ActivityDetailPage : ContentPage
    {
        public ActivityDetailPage()
        {
            InitializeComponent();
        }

        void activityImageCarousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
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
}
