using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AtWork.SubViews
{
    public partial class ActivityView : ContentView
    {
        public ActivityView()
        {
            InitializeComponent();
        }

        void Activitycollectionlist_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            string current = e.CurrentSelection.FirstOrDefault() as string;//(e.CurrentSelection.FirstOrDefault() as Monkey)?.Name;
        }
    }
}
