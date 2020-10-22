using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace AtWork.CustomControls
{
    public class CustomEditor : Editor
    {
        public CustomEditor()
        {
            this.TextChanged += (sender, e) =>
            {
                this.InvalidateMeasure();
            };
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
           propertyName: "ReturnType", returnType: typeof(ReturnType), declaringType: typeof(CustomEditor), defaultValue: ReturnType.Default);

        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
           propertyName: "ReturnCommand", returnType: typeof(ICommand), declaringType: typeof(CustomEditor), defaultValue: null);

        public ICommand ReturnCommand
        {
            get { return (ICommand)GetValue(ReturnCommandProperty); }
            set { SetValue(ReturnCommandProperty, value); }
        }
    }
}
