using System;
namespace AtWork.Services
{
    public class ConfigService
    {

        //*****Local URL*****//
        //public static string BaseServerURL = "http://a3b41c661f73.ngrok.io";
        //public static string BaseServiceURL = "http://a3b41c661f73.ngrok.io/api";
        //

        //*****LIVE URL******//
        public static string BaseServerURL = "http://app.atwork.ai";
        public static string BaseServiceURL = "http://app.atwork.ai/api";
        public static string BaseNewsImageURL = BaseServerURL + "/newsposts/";
        public static string BaseProfileImageURL = BaseServerURL + "/volunteers/";
        public static string BaseNewsAttachFileURL = BaseServerURL + "/newspostsfile/";
        public static string BaseActivityImageURL = BaseServerURL + "/activities/";
        public static string BaseCompanyLogoURL = BaseServerURL + "/companylogos/";
        //

        public static string LoginServiceURL = "/Login";
        public static string AuthorizationTokenKey = "Basic ";
        public static string NewsDetailsServiceURL = "/news/getrow/";
        public static string NewsDetails_V1ServiceURL = "/news/getrow_v1/";
        public static string NewsAddPostServiceURL = "/news/addrow";
        public static string NewsPostEditServiceURL = "/news/editrow";
        public static string NewsDetailsAddCommentServiceURL = "/commentslikes/addComment";
        public static string NewsDetailsEditCommentServiceURL = "/commentslikes/EditComment";
        public static string NewsDetailsDeleteCommentServiceURL = "/commentslikes/deleteComment/";
        public static string NewsDetailsGetCommentListURL = "/commentslikes/getcommentlist_v1/";
        public static string NewsPostDeleteServiceURL = "/news/deleterow/";
        public static string NewsListServiceURL = "/news/getlist/";//"/news/getlist_v1/";
        public static string CommentsLikesServiceURL = "/commentslikes";
        public static string AddNewsLikeServiceURL = "/AddNewsLike";
        public static string DeleteNewsLikeServiceURL = "/DeleteNewsLike";
        public static string ActivitiesGetRowServiceURL = "/activities/getrow/";
        public static string ActivityListServiceURL = "/activities/getlist_v1/";
        public static string ActivityJoinServiceURL = "/activities/joinActitvity/";
        public static string MyActivityListServiceURL = "/activities/MyActivity/";
        public static string CreateActivityServiceURL = "/activities/insertrow";
        public static string ActivitiesJoinedMembersServiceURL = "/activities/getActivitiesEmp/";
        public static string ActivityUnsubscribeServiceURL = "/activities/deletejoinActitvity/";
        public static string NewsCommentLinkServiceURL = "/news/getlist/";
        public static string NewsAddCommentLikeServiceURL = "/commentslikes/AddNewsCommentLike";
        public static string NewsDeleteCommentLikeServiceURL = "/commentslikes/DeleteNewsCommentLike";
    }
}
