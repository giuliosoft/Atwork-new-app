using System;
using System.Collections.Generic;

namespace AtWork.Models
{
    public class NotificationModel
    {
        public class NotificationResponseModel
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<Notification> Data { get; set; }
            public object Data1 { get; set; }
        }
        public class NotificationSaveResponseModel
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public Notification Data { get; set; }
            public object Data1 { get; set; }
        }
        public class NotificationConnectResponseModel
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<Connect_Notification_Setting> Data { get; set; }
            public object Data1 { get; set; }
        }
        public class NotificationActivityResponseModel
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<Activity_Notification_Setting> Data { get; set; }
            public object Data1 { get; set; }
        }
        public class Notification
        {
            public string volUniqueId { get; set; } //
            public bool IsPaused { get; set; }  // toggle
            public string PauseTime { get; set; } // selected value in popup
            public bool IsForever { get; set; } // 
            public int PauseTimeMinute { get; set; } // calculated min
            public DateTime PauseNotificationStarttime { get; set; } 
            public DateTime PauseNotificationEndtime { get; set; }
            public DateTime PauseNotificationCurrentTime { get; set; }
            public string FormattedDate { get; set; }

            //public Connect_Notification_Setting connect_Notification_Setting { get; set; }
            //public Activity_Notification_Setting activity_Notification_Setting { get; set; }
        }
        public class Connect_Notification_Setting
        {
            public string volUniqueId { get; set; }
            public bool Connect_IsPostFromCompany { get; set; }
            public bool Connect_IsPostFromGroup { get; set; }
            public bool Connect_IsPostFromEveryone { get; set; }
            public bool Connect_IsLikesOnPosts { get; set; }
            public bool Connect_IsLikesOnComments { get; set; }
            public bool Connect_IsCommentsOnPosts { get; set; }
            public bool Connect_IsPostsYouComment { get; set; }

        }
        public class Activity_Notification_Setting
        {
            public string volUniqueId { get; set; }
            public bool Active_IsYGTSomeoneRegister { get; set; }
            public bool Active_IsYGTSomeoneCancelled { get; set; }
            public bool Active_IsAllActActivityCancelled { get; set; }
            public bool Active_IsAllActActivityReminder { get; set; }
            public bool Active_IsAllActFeedbackReminder { get; set; }
            public bool Active_IsPetitionsStatusUpdate { get; set; }

        }
    }
}
