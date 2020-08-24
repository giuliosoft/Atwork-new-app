using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AtWork.Helpers
{
    public class CommonUtility
    {
        public static ImageSource getImageFromEmbeddedResource(string ImageName)
        {
            ImageSource imageSource = null;
            try
            {
                string Source = "PowerHold.Images." + ImageName;
                imageSource = ImageSource.FromResource(Source, typeof(CommonUtility).GetTypeInfo().Assembly);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return imageSource;
        }

        public static bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
