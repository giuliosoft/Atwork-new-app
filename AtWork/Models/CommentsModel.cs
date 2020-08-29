using System;
using AtWork.Services;
using Xamarin.Forms;

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
            public string comUserName { get; set; }
            public string comLineCount { get; set; }
            public string ComUserProfileImage { get; set; }

            public ImageSource NewsCommentUserProfileImage
            {
                get
                {
                    if (ComUserProfileImage == string.Empty)
                        return string.Empty;
                    return ImageSource.FromUri(new Uri(string.Format("http://app.atwork.ai/{0}", ComUserProfileImage)));
                }
            }

            //public virtual ICollection<tbl_News_Comments_Likes> tbl_News_Comments_Likes { get; set; }
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
    }
}
