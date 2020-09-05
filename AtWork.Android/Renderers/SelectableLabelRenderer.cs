using System;
using Android.Content;
using AtWork.CustomControls;
using AtWork.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SelectableLabel), typeof(SelectableLabelRenderer))]
namespace AtWork.Droid.Renderers
{
    class SelectableLabelRenderer : LabelRenderer
    {
        public SelectableLabelRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.SetTextIsSelectable(true);
        }
    }
}
