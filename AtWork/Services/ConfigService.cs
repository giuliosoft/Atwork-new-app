using System;
namespace AtWork.Services
{
    public class ConfigService
    {
        public static string BaseServiceURL = "http://app.atwork.ai/api";
        public static string LoginServiceURL = "/Login";
        public static string AuthorizationTokenKey = "Basic ";
        public static string NewsDetailsServiceURL = "/news/getrow";
        public static string NewsAddPostServiceURL = "/news/addrow";
        public static string NewsDetailsAddCommentServiceURL = "/commentslikes/addComment";
        public static string NewsDetailsEditCommentServiceURL = "/commentslikes/EditComment";
        public static string NewsDetailsDeleteCommentServiceURL = "/commentslikes/deleteComment/";
    }
}
