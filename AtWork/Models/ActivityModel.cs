using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AtWork.Services;
using Prism.Mvvm;
using Xamarin.Forms;

namespace AtWork.Models
{

    public class ActivityModel
    {
        public class ActivityListModel : BindableBase

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
            public string proCompany { get; set; }
            public string proCountry { get; set; }
            public string proContinent { get; set; }
            public string proTimeCommitment { get; set; }
            public string proTimeCommitmentDecimal { get; set; }
            public string proDatesConfirmed { get; set; }
            public string proType { get; set; }
            public string proCategory { get; set; }
            public string proSubCategory { get; set; }
            public string proPartnerEmail { get; set; }
            public string proActivityLanguage { get; set; }
            public string proActivityLanguageID { get; set; }
            public string proAudience { get; set; }
            public string proSpecialRequirements { get; set; }
            public string proCostCoveredEmployee { get; set; }
            public string proCostCoveredCompany { get; set; }
            public string proAddActivity_OrgName { get; set; }
            public string proAddActivity_Website { get; set; }
            public string proAddActivity_AdditionalInfo { get; set; }
            public string proAddActivity_CoordinatorEmail { get; set; }
            public DateTime? proPublishedDate { get; set; }

            //New Fields:
            public ObservableCollection<ActivityCarouselListModel> ActivityCarouselList { get; set; }

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
            public bool _ShowPastActivity;
            public bool ShowPastActivity
            {
                get { return _ShowPastActivity; }
                set { SetProperty(ref _ShowPastActivity, value); }
            }
            public bool IsPastActivity {get;set;}
            /*
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
            }*/
        }

        public class ActivityResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<ActivityListModel> Data { get; set; }
            public List<ActivityListModel> Data1 { get; set; }
        }

        public class ActivityCarouselListModel
        {
            public ImageSource ActivityImage { get; set; }
            public string ActivityImageUrl { get; set; }
        }

        public class JoinActivityInputModel
        {
            public int Id { get; set; }
            public string coUniqueID { get; set; }
            public string proUniqueID { get; set; }
            public string volUniqueID { get; set; }
            public int ActivityID { get; set; }
        }
    }
}
