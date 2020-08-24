using System;
using Android.Content;
using AtWork.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AtWork.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
                var view = (CustomEntry)Element;

                if (view != null && Control != null)
                {
                    SetBorderLessEntry(view);
                    Control.SetPadding(Control.PaddingLeft, 0, Control.PaddingRight, 0);

                    view.Unfocused += (object sender, FocusEventArgs ev) =>
                    {
                        view.Text = (!view.IsPassword) ? view.Text?.Trim() : view.Text;
                    };
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void SetBorderLessEntry(CustomEntry view)
        {
            if (view.IsBorderLess)
            {
                Control.Background = null;
            }
        }
    }
}

