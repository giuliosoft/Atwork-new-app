using System;
using System.Diagnostics;
using AtWork.CustomControls;
using AtWork.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace AtWork.iOS.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            try
            {
                var view = (CustomEditor)Element;
                if (view != null && Control != null)
                {
                    Control.TintColor = view.TextColor.ToUIColor();
                    Control.Layer.BorderWidth = 0;

                    var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));

                    var doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Plain, (o, a) =>
                    {
                        Control.ResignFirstResponder();
                        if (view.ReturnCommand != null)
                        {
                            view.ReturnCommand.Execute(this);
                        }
                    });

                    var nextButton = new UIBarButtonItem("Next", UIBarButtonItemStyle.Plain, (o, a) =>
                    {
                        Control.ResignFirstResponder();
                        ((IEntryController)Element).SendCompleted();
                    });

                    if (view.ReturnType == ReturnType.Next)
                    {
                        toolbar.Items = new[]
                        {
                                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                                nextButton
                            };
                    }
                    else if (view.ReturnType == ReturnType.Done)
                    {
                        toolbar.Items = new[]
                        {
                                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                                doneButton
                            };
                    }
                    this.Control.InputAccessoryView = toolbar;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
    }
}
