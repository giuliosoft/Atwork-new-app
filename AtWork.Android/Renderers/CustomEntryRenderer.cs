using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using AtWork.CustomControls;
using AtWork.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
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
				//var view = (Entry)Element;
                //if (view != null && Control != null)
                //{
                //    Control.SetPadding(0, 0, 0, 0);
                 //   Control.SetBackgroundColor(view.BackgroundColor.ToAndroid());
                 //   //Control.Gravity = GravityFlags.Start | GravityFlags.CenterVertical;
                 //   IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                 //   IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                //    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Layout.CustomCursor);
                //}
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

