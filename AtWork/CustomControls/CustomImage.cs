using System;

using Xamarin.Forms;

namespace AtWork.CustomControls
{
    public class CustomImage : Image
	{
		
		public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(CustomImage), Color.Transparent);

		public Color TintColor
		{
			get { return (Color)GetValue(TintColorProperty); }
			set
			{
				SetValue(TintColorProperty, value);
			}
		}

	}
}
