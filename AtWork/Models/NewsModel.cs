using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AtWork.Services;
using AtWork.ViewModels;
using Prism.Mvvm;
using Xamarin.Forms;
using static AtWork.Models.CommentsModel;
using static AtWork.Models.LoginModel;

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
            public bool LikeByLoginUser { get; set; }
            public int LikeCount { get; set; }
            public int LikeId { get; set; }
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
            public string NewsImage { get; set; }
            public List<NewsCarouselListModel> NewsImageCarouselList { get; set; }
        }

        public class NewsListResponse
        {
            public bool Flag { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public List<NewsListModel> Data { get; set; }
        }

        public class NewsListModel
        {
            public int CommentsCount { get; set; }
            public int LikeCount { get; set; }
            public News news { get; set; }
            public Volunteers Volunteers { get; set; }
            public string Day { get; set; }
        }

        #region Bind View Models
        public class NewsCarouselListModel
        {
            public ImageSource NewsImage { get; set; }
            public string NewsImageUrl { get; set; }
        }

        public class NewsListData_Model : BindableBase
        {
            public News news { get; set; }
            public Volunteers Volunteers { get; set; }
            public ObservableCollection<NewsCarouselListModel> NewsCarouselList { get; set; }
            public ImageSource newsPostUserProfilePic { get; set; }
            public ImageSource newsPostPublishType { get; set; }
            public string userName { get; set; }
            public string newsTitle { get; set; }
            public string newsDescription { get; set; }
            public string NewsCreatedTime { get; set; }

            public int _LikeCount;
            public int LikeCount
            {
                get { return _LikeCount; }
                set { SetProperty(ref _LikeCount, value); }
            }

            public int _CommentsCount;
            public int CommentsCount
            {
                get { return _CommentsCount; }
                set { SetProperty(ref _CommentsCount, value); }
            }
            public bool NewsCreatedByLoggedInUser
            {
                get
                {
                    if (Volunteers != null && SettingsService.VolunteersUserData != null)
                        return Volunteers.volUniqueID == SettingsService.VolunteersUserData.volUniqueID;
                    return false;
                }
            }

            public bool NewsPostUserIsVisible
            {
                get
                {
                    bool retVal = true;
                    if (news != null)
                    {
                        retVal = news.newsOrigin.Equals("Coordinator", StringComparison.InvariantCultureIgnoreCase) ? false : true;
                    }
                    return retVal;
                }
            }
        }
        #endregion

        public class NewsDetailModel_Input
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

        //public partial class NewsComment
        //{
        //    public NewsComment()
        //    {
        //        //tbl_News_Comments_Likes = new HashSet<tbl_News_Comments_Likes>();
        //    }
        //    public int Id { get; set; }
        //    public string coUniqueID { get; set; }
        //    public string newsUniqueID { get; set; }
        //    public string comByID { get; set; }
        //    public DateTime? comDate { get; set; }
        //    public string comContent { get; set; }
        //    //public virtual ICollection<tbl_News_Comments_Likes> tbl_News_Comments_Likes { get; set; }

        //}

        public class NewsLikes
        {
            public int Id { get; set; }
            public int newsId { get; set; }
            public string likeByID { get; set; }
            public DateTime? likeDate { get; set; }
            public bool LikeByLoginUser { get; set; }
        }

        public class NewsLikeRespnce
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public int Data { get; set; }
            public int Data1 { get; set; }
        }

        public class NewUnLikeRespnce
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
            public int Data1 { get; set; }
        }



        public class CommentLikeResponce
        {
            public bool Flag { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public int Data { get; set; }
        }
    }
}

