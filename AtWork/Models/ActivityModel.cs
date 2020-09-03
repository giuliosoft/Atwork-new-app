using System;
using System.Collections.Generic;
using System.Linq;
using AtWork.Services;
using Xamarin.Forms;

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
            public string proBackgroundImage { get; set; }
            public string proDeliveryMethod { get; set; }

            //New fields:
            public string activityLocation
            {
                get
                {
                    if (string.IsNullOrEmpty(proLocation))
                        return string.Empty;
                    return proLocation;
                }
            }

            public string activityDate
            {
                get
                {
                    var retDate = string.Empty;
                    try
                    {
                        if (proAddActivityDate == null)
                            retDate = string.Empty;
                        var date = proAddActivityDate.Date;
                        retDate = date.ToString("d MMM yyyy");
                    }
                    catch (Exception ex) { }
                    return retDate;
                }
            }

            public string activityDuration
            {
                get
                {
                    if (string.IsNullOrEmpty(proAddActivity_StartTime) || string.IsNullOrEmpty(proAddActivity_EndTime))
                        return string.Empty;
                    return proAddActivity_StartTime + " - " + proAddActivity_EndTime;
                }
            }

            public ImageSource activityImage
            {
                get
                {
                    ImageSource retVal = string.Empty;
                    try
                    {
                        if (string.IsNullOrEmpty(proBackgroundImage))
                            return string.Empty;
                        List<string> aimgUrlList = new List<string>();
                        if (proBackgroundImage.Contains(","))
                        {
                            aimgUrlList = proBackgroundImage.Split(',').ToList();
                            if (aimgUrlList != null && aimgUrlList.Count > 0)
                            {
                                retVal = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + aimgUrlList[0]));
                            }
                        }
                        else
                        {
                            retVal = ImageSource.FromUri(new Uri(ConfigService.BaseActivityImageURL + proBackgroundImage));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    return retVal;
                }
            }
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
