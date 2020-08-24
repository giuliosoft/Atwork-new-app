using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AtWork.CustomControls;
using AtWork.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace AtWork.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            try
            {
                var view = (CustomEditor)Element;
                if (view != null && Control != null)
                {
                    Control.SetPadding(10, 10, 10, 10);
                    Control.SetBackgroundColor(view.BackgroundColor.ToAndroid());
                    Control.Gravity = GravityFlags.Start;
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.CustomCursor);
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }
    }
}
