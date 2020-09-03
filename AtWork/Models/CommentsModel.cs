using System;
using System.Collections.Generic;
using AtWork.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Models
{
    public class CommentsModel
    {
        public class NewsComment
        {
            public NewsComment()
            {
                //tbl_News_Comments_Likes = new HashSet<tbl_News_Comments_Likes>();
            }
            public int Id { get; set; }
            public string coUniqueID { get; set; }
            public string newsUniqueID { get; set; }
            public string comByID { get; set; }
            public DateTime? comDate { get; set; }
            public string comContent { get; set; }

            public News News { get; set; }
            public int Comments_Likes { get; set; }
            public string Day { get; set; }
            public Volunteers Volunteers { get; set; }
            public int LikeCount { get; set; }

            [JsonIgnore]
            public string UserName
            {
                get
                {
                    if (Volunteers?.volFirstName != string.Empty && Volunteers?.volLastName != string.Empty)
                    {
                        return Volunteers?.volFirstName + " " + Volunteers?.volLastName;
                    }
                    else if (Volunteers?.volFirstName != string.Empty)
                    {
                        return Volunteers?.volFirstName;
                    }
                    return string.Empty;
                }
            }

            [JsonIgnore]
            public string CommentsLikes
            {
                get
                {
                    if (Comments_Likes != null)
                    {
                        return Comments_Likes.ToString();
                    }
                    return string.Empty;
                }
            }
            //public string comUserName { get; set; }
            //public string comLineCount { get; set; }
            //public string ComUserProfileImage { get; set; }

            [JsonIgnore]
            public ImageSource NewsCommentUserProfileImage
            {
                get
                {
                    if (Volunteers?.volPicture == string.Empty)
                        return string.Empty;
                    return ImageSource.FromUri(new Uri(string.Format("http://app.atwork.ai/{0}", Volunteers?.volPicture)));
                }
            }

            ////public virtual ICollection<tbl_News_Comments_Likes> tbl_News_Comments_Likes { get; set; }
            [JsonIgnore]
            public bool CommentByLoggedInUser
            {
                get
                {
                    if (comByID != null && SettingsService.VolunteersUserData != null && SettingsService.VolunteersUserData.volUniqueID != null)
                        return comByID == SettingsService.VolunteersUserData.volUniqueID;
                    return false;
                }
            }
        }
        public class NewsCommentResponce
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<NewsComment> Data { get; set; }
            public object Data1 { get; set; }
        }
        
    }
}
