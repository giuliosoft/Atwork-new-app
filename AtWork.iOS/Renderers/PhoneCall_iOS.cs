using AtWork.Helpers;
using AtWork.iOS.Renderers;
using Foundation;
using MessageUI;
using System;
using System.Windows;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneCall_iOS))]
namespace AtWork.iOS.Renderers
{
	public class PhoneCall_iOS : PhoneCallEmail
	{
		public void ComposerEmail(string email)
		{
			try
			{
				var url = new Uri("mailto:" + email);
				
				if (!UIApplication.SharedApplication.OpenUrl(url))
				{
					var av = new UIAlertView("Not supported",
						"Email is not supported on this device",
						null,
						"OK",
						null);
					av.Show();
				}
				else
                {
					UIApplication.SharedApplication.OpenUrl(url);
				}
				//hans.meier @volanty.com
 //Atwork2020

			}
			catch (Exception ex)
			{

			}
		}

		public void MakeQuickCall(string PhoneNumber)
		{
			try
			{
				var url = new Uri("tel:" + PhoneNumber);
				if (!UIApplication.SharedApplication.OpenUrl(url))
				{
					var av = new UIAlertView("Not supported",
						"Calling is not supported on this device",
						null,
						"OK",
						null);
					av.Show();
				}
				else
                {
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
