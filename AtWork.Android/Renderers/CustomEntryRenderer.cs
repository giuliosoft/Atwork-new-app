using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using AtWork.CustomControls;
using AtWork.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
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
                var view = (Entry)Element;
                if (view != null && Control != null)
                {
                    Control.SetPadding(0, 0, 0, 0);
                    Control.SetBackgroundColor(view.BackgroundColor.ToAndroid());
                    //Control.Gravity = GravityFlags.Start | GravityFlags.CenterVertical;
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Layout.CustomCursor);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}