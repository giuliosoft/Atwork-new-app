using System;
namespace AtWork.Services
{
    public class ConfigService
    {

        //*****Local URL*****//
        //public static string BaseServerURL = "http://d3a247eeba49.ngrok.io";
        //public static string BaseServiceURL = "http://d3a247eeba49.ngrok.io/api";
        //

        //*****LIVE URL******//
        public static string BaseServerURL = "http://app.atwork.ai";
        public static string BaseServiceURL = "http://app.atwork.ai/api";
        public static string BaseNewsImageURL = BaseServerURL + "/newsposts/";
        public static string BaseProfileImageURL = BaseServerURL + "/volunteerpics/";
        public static string BaseNewsAttachFileURL = BaseServerURL + "/newspostsfile/";
        public static string BaseActivityImageURL = BaseServerURL + "/activities/";
        public static string BaseCompanyLogoURL = BaseServerURL + "/companylogos/";
        //

        public static string LoginServiceURL = "/Login";
        public static string AuthorizationTokenKey = "Basic ";
        public static string NewsDetailsServiceURL = "/news/getrow/";
        public static string NewsAddPostServiceURL = "/news/addrow";
        public static string NewsPostEditServiceURL = "/news/editrow";
        public static string NewsDetailsAddCommentServiceURL = "/commentslikes/addComment";
        public static string NewsDetailsEditCommentServiceURL = "/commentslikes/EditComment";
        public static string NewsDetailsDeleteCommentServiceURL = "/commentslikes/deleteComment/";
        public static string NewsDetailsGetCommentListURL = "/commentslikes/getcommentlist/";
        public static string NewsPostDeleteServiceURL = "/news/deleterow/";
        public static string NewsListServiceURL = "/news/getlist/";
        public static string CommentsLikesServiceURL = "/commentslikes";
        public static string AddNewsLikeServiceURL = "/AddNewsLike";
        public static string DeleteNewsLikeServiceURL = "/DeleteNewsLike";
        public static string ActivitiesGetRowServiceURL = "/activities/getrow/";
        public static string ActivityListServiceURL = "/activities/getlist/";
        public static string ActivityJoinServiceURL = "/activities/joinActitvity/";
        public static string MyActivityListServiceURL = "/activities/MyActivity/";
    }
}
