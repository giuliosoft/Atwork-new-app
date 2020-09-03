using System;
using System.Collections.Generic;

namespace AtWork.Models
{
    public class ActivityModel
    {
        public class ActivityListModel
        {
            public int id { get; set; }
            public string proUniqueID { get; set; }
            public string proTitle { get; set; }
            public string coUniqueID { get; set; }
            public string proDescription { get; set; }
            public string proLocation { get; set; }
            public DateTime proAddActivityDate { get; set; }
            public string proAddActivity_StartTime { get; set; }
            public string proAddActivity_EndTime { get; set; }
            public string proAddress1 { get; set; }
            public object proAddress2 { get; set; }
            public string proPostalCode { get; set; }
            public string proCity { get; set; }
            public string proCategoryName { get; set; }
            public string proSubCategoryName { get; set; }
            public string proStatus { get; set; }
            public string proPartner { get; set; }
            public object proAddActivity_HoursCommitted { get; set; }
            public string proAddActivity_ParticipantsMinNumber { get; set; }
            public string proAddActivity_ParticipantsMaxNumber { get; set; }
            public object proBackgroundImage { get; set; }
            public string proDeliveryMethod { get; set; }
        }

        public class ActivityResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<ActivityListModel> Data { get; set; }
            public object Data1 { get; set; }
        }
    }
}
