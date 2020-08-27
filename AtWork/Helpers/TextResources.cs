using System;
using AtWork.Views;

namespace AtWork.Helpers
{
    public class TextResources
    {
        public static string TooManyAttemptsText { get { return "Too Many Attempts."; } }
        public static string AddPostTitlePageText = nameof(AddNewsPostPage);
        public static string AddPostImagePageText = nameof(AddNewsPostImagePage);
		public static string LoginEmptyFieldMsg = "Email or Password can't be empty.";
        public static string InvalidEmailMsg = "This is not valid email. Please enter valid email address.";
        public static string PasswordLengthMsg = "Password must be atleast of 6 characters.";
        public static string InvalidUserNameorPaddword = "Invalid username or password.";
    }
}
