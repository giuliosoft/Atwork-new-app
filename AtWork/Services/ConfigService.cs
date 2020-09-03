﻿using System;
namespace AtWork.Services
{
    public class ConfigService
    {
        //*****Local URL*****//
        //public static string BaseServiceURL = "http://6e3e3b0e32ce.ngrok.io/api";
        //public static string BaseImageURL = "http://6e3e3b0e32ce.ngrok.io/";
        //

        //*****LIVE URL******//
        public static string BaseServiceURL = "http://app.atwork.ai/api";
        public static string BaseImageURL = "http://app.atwork.ai/newsposts/";
        public static string BaseNewsImageURL = "http://app.atwork.ai/newsposts/";
        public static string BaseProfileImageURL = "http://app.atwork.ai/volunteerpics/";
        public static string BaseNewsAttachFileURL = "http://app.atwork.ai/newspostsfile/";
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
        public static string ActivityListServiceURL = "/activities/getlist/";        
    }
}
