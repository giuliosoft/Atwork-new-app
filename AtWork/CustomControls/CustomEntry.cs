using System;

using Xamarin.Forms;

namespace AtWork.CustomControls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty IsBorderLessProperty = BindableProperty.Create(propertyName: "IsBorderLess", returnType: typeof(bool), declaringType: typeof(CustomEntry), defaultValue: false);
        public bool IsBorderLess
        {
            get { return (bool)GetValue(IsBorderLessProperty); }
            set { SetValue(IsBorderLessProperty, value); }
        }
    }
}

