using System;
using AtWork.CustomControls;
using AtWork.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace AtWork.iOS.Renderers
{
    public class CustomImageRenderer : ImageRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			SetTint();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			//if (e.PropertyName == MyImage.TintColorProperty.PropertyName)
			SetTint();
		}

		void SetTint()
		{
			if (Control?.Image == null || Element == null)
				return;

			if (((CustomImage)Element).TintColor == Color.Transparent)
			{
				//Turn off tinting
				Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
				Control.TintColor = null;
			}
			else
			{
				//Apply tint color
				Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.AlwaysTemplate);
				Control.TintColor = ((CustomImage)Element).TintColor.ToUIColor();
			}
		}

	}
}

