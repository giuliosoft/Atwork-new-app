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
        public static string NewsTabText = "News";
        public static string ActivityTabText = "Activities";
        public static string OnDemandCategoryText = "On-Demand";
        public static string RegularCategoryText = "Regular";
        public static string RecurringCategoryText = "Recurring";
        public static string DefaultCalendarText = "Default";

        #region App Constants
        public const string CorpVolID = "cat001";
        public const string SportsID = "cat002";
        public const string CultureID = "cat003";
        public const string EducationID = "cat004";
        public const string CompEventsID = "cat005";
        public const string GetTogetherID = "cat006";

        public const string ActiveStatus = "Active";
        public const string InActiveStatus = "Inactive";

        public const string RateOneHalf = "OneHalf";
        public const string RateOneFull = "OneFull";
        public const string RateTwoHalf = "TwoHalf";
        public const string RateTwoFull = "TwoFull";
        public const string RateThreeHalf = "ThreeHalf";
        public const string RateThreeFull = "ThreeFull";
        public const string RateFourHalf = "FourHalf";
        public const string RateFourFull = "FourFull";
        public const string RateFiveHalf = "FiveHalf";
        public const string RateFiveFull = "FiveFull";
        #endregion
    }
}
