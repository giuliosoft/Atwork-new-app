﻿using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Views
{
    public partial class NewsDetailPage : ContentPage
    {
        NewsDetailPageViewModel VMContext;
        public NewsDetailPage()
        {
            InitializeComponent();
            VMContext = ((NewsDetailPageViewModel)this.BindingContext);
            indicatorView.Position = 0;
        }

        void tutorialCarousel_PositionChanged(System.Object sender, Xamarin.Forms.PositionChangedEventArgs e)
        {
            var view = sender as CarouselView;

            indicatorView.Position = view.Position;
        }

        void CommentEditor_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string editorText = sender as string;
            if (!string.IsNullOrEmpty(commentEditor.Text))
            {
                VMContext.SendButtonIsVisible = true;
            }
            else
            {
                VMContext.SendButtonIsVisible = false;
            }
        }
    }
}
