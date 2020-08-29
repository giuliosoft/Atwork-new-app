using System;
using AtWork.Services;

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
