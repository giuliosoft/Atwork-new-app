using System;

using Xamarin.Forms;

namespace AtWork.Models
{
    public class NewsModel : ContentPage
    {
        public class NewsResponce
        {
            public bool Flag { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public NewsDetailModel Data { get; set; }
        }
        public class NewsDetailModel
        {
            public News News { get; set; }
            public int Comments_Likes { get; set; }
            public string Day { get; set; }
            public Volunteers Volunteers { get; set; }
        }
        public class News
        {
            public int id { get; set; }
            public string coUniqueID { get; set; }
            public string newsUniqueID { get; set; }
            public string volUniqueID { get; set; }
            public string newsTitle { get; set; }
            public string newsContent { get; set; }
            public DateTime? newsDateTime { get; set; }
            public DateTime? newsPostedTime { get; set; }
            public string newsPrivacy { get; set; }
            public string newsStatus { get; set; }
            public string newsOrigin { get; set; }
            public string newsImage { get; set; }
            public string newsFile { get; set; }
            public string newsFileOriginal { get; set; }
        }
        public partial class Volunteers
        {
            public int id { get; set; }
            public string coUniqueID { get; set; }
            public string volUniqueID { get; set; }
            public string volFirstName { get; set; }
            public string volLastName { get; set; }
            public string volGender { get; set; }
            public string volUserName { get; set; }
            public string VolUserPassword { get; set; }
            public string volEmail { get; set; }
            public string volOnBoardStatus { get; set; }
            public DateTime? volOnBoardDateSent { get; set; }
            public string volPicture { get; set; }
            public string volEducation { get; set; }
            public string volCompetencies { get; set; }
            public string volCategories { get; set; }
            public string volPhone { get; set; }
            public string volStatus { get; set; }
            public string restricted { get; set; }
            public string reviewStatus { get; set; }
            public DateTime? reviewDate { get; set; }
            public string volDepartment { get; set; }
            public string volLanguage { get; set; }
            public string volAbout { get; set; }
            public string volInterests { get; set; }
        }

    }
}

