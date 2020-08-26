using System;
using AtWork.CustomControls;
using AtWork.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace AtWork.Droid.Renderers
{
    public class CustomImageRenderer : ImageRenderer
	{
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (Control != null)
			{
				CustomImage MIV = (CustomImage)sender;
				Control.SetColorFilter(MIV.TintColor.ToAndroid());
			}
		}
	}
}

