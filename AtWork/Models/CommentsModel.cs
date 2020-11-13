using System;
using System.Collections.Generic;
using AtWork.Services;
using Newtonsoft.Json;
using Prism.Mvvm;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;
using static AtWork.Models.NewsModel;

namespace AtWork.Models
{
    public class CommentsModel
    {
        public class NewsComment : BindableBase
        {
            public NewsComment()
            {
                //News_Comments_Likes = new List<News_Comments_Likes>();
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
            public int LikeId { get; set; }
            //public List<News_Comments_Likes> News_Comments_Likes { get; set; }
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
            private int _likeCount;
            public int LikeCount
            {
                get
                {
                    return _likeCount;
                }
                set
                {
                    _likeCount = value;
                    RaisePropertyChanged(nameof(CommentsLikes));
                }
            }
            [JsonIgnore]
            public string CommentsLikes
            {
                get
                {
                    return LikeCount.ToString();
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
                    return ImageSource.FromUri(new Uri(ConfigService.BaseProfileImageURL + Volunteers?.volPicture));
                }
            }

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
            private bool _likeByLoggedInUser;
            public bool LikeByLoginUser
            {
                get
                {
                    return _likeByLoggedInUser;
                }
                set
                {
                    _likeByLoggedInUser = value;
                    RaisePropertyChanged(nameof(CommentLikeImage));
                    RaisePropertyChanged(nameof(CommentsLikes));
                }
            }
            [JsonIgnore]
            public ImageSource CommentLikeImage
            {
                get
                {
                    if (LikeByLoginUser)
                        return "heartfill";
                    else
                        return "heartoutline";
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
        public class News_Comments_Likes
        {
            public int Id { get; set; }
            public int newsCommentId { get; set; }
            public string likeByID { get; set; }
            public DateTime? likeDate { get; set; }
        }
    }
}
