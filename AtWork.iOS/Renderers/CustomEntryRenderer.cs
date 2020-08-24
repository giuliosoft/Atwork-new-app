using System;
using AtWork.CustomControls;
using AtWork.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace AtWork.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (CustomEntry)Element;
            if (view != null && Control != null)
            {
                SetBorderLessEntry(view);
                view.Unfocused += (object sender, FocusEventArgs ev) =>
                {
                    Control.ResignFirstResponder();
                    view.Text = (!view.IsPassword) ? view.Text?.Trim() : view.Text;
                };

                view.Focused += (object sender, FocusEventArgs ev) =>
                {
                    Control.BecomeFirstResponder();
                };

                try
                {
                    UIView paddingView = new UIView(new CoreGraphics.CGRect(0, 0, 5, 20));
                    Control.LeftView = paddingView;
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
                Control.TintColor = view.TextColor.ToUIColor();
            }
        }

        void SetBorderLessEntry(CustomEntry view)
        {
            if (view.IsBorderLess)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
